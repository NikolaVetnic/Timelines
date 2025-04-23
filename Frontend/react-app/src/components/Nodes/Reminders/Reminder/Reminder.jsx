import React, { useEffect, useState } from "react";
import RemindersList from "../../../../core/components/lists/RemindersList/RemindersList";
import CreateReminderModal from "../../../../core/components/modals/CreateReminderModal/CreateReminderModal";
import DeleteModal from "../../../../core/components/modals/DeleteModal/DeleteModal";
import ReminderService from "../../../../services/ReminderService";
import Pagination from "../../../../core/components/pagination/Pagination";
import "./Reminder.css";

const Reminder = ({ nodeId, onToggle }) => {
  const root = "reminder";
  const [isRemindersExpanded, setIsRemindersExpanded] = useState(false);
  const [isCreateModalOpen, setCreateModalOpen] = useState(false);
  const [reminders, setReminders] = useState([]);
  const [reminderToDelete, setReminderToDelete] = useState(null);
  const [isDeleteModalOpen, setIsDeleteModalOpen] = useState(false);
  const [isLoading, setIsLoading] = useState(false);

  const [currentPage, setCurrentPage] = useState(1);
  const [itemsPerPage, setItemsPerPage] = useState(2);
  const itemsPerPageOptions = [2, 4, 6, 8];

  useEffect(() => {
    const fetchReminders = async () => {
      if (isRemindersExpanded && nodeId) {
        setIsLoading(true);
        try {
          const remindersData = await ReminderService.getRemindersByNode(
            nodeId
          );
          setReminders(remindersData);
        } finally {
          setIsLoading(false);
        }
      }
    };

    fetchReminders();
  }, [isRemindersExpanded, nodeId]);

  const indexOfLastItem = currentPage * itemsPerPage;
  const indexOfFirstItem = indexOfLastItem - itemsPerPage;
  const currentReminders = reminders.slice(indexOfFirstItem, indexOfLastItem);
  const totalPages = Math.ceil(reminders.length / itemsPerPage);

  const handlePageChange = (page) => {
    setCurrentPage(page);
  };

  const handleItemsPerPageChange = (size) => {
    setItemsPerPage(size);
    setCurrentPage(1);
  };

  const toggleRemindersSection = () => {
    setIsRemindersExpanded((prev) => !prev);
    onToggle();
  };

  const openCreateModal = (e) => {
    e.stopPropagation();
    setCreateModalOpen(true);
  };

  const closeCreateModal = () => setCreateModalOpen(false);

  const saveNewReminder = async (newReminder) => {
    const reminderWithNodeId = { ...newReminder, nodeId };
    await ReminderService.createReminder(reminderWithNodeId);
    const updatedReminders = await ReminderService.getRemindersByNode(nodeId);
    setReminders(updatedReminders);
    closeCreateModal();
  };

  const confirmDelete = async () => {
    if (!reminderToDelete) return;

    try {
      await ReminderService.deleteReminder(reminderToDelete.id);
      setReminders((prev) => prev.filter((r) => r.id !== reminderToDelete.id));
    } finally {
      setIsDeleteModalOpen(false);
      setReminderToDelete(null);
    }
  };

  const cancelDelete = () => {
    setIsDeleteModalOpen(false);
    setReminderToDelete(null);
  };

  return (
    <div className={`${root}-section`}>
      <button
        className={`${root}-header ${root}-${
          isRemindersExpanded ? "header-open" : "header-closed"
        }`}
        onClick={toggleRemindersSection}
      >
        <h4>Reminders</h4>
        <span>{isRemindersExpanded ? "-" : "+"}</span>
      </button>

      {isRemindersExpanded && (
        <div className={`${root}-content`}>
          {isLoading ? (
            <div className={`${root}-loading`}>Loading reminders...</div>
          ) : (
            <>
              <RemindersList
                root={root}
                reminders={currentReminders}
                openCreateModal={openCreateModal}
                setReminderToDelete={setReminderToDelete}
                setIsDeleteModalOpen={setIsDeleteModalOpen}
              />
              {reminders.length > 0 && (
                <Pagination
                  currentPage={currentPage}
                  totalPages={totalPages}
                  itemsPerPage={itemsPerPage}
                  onPageChange={handlePageChange}
                  onItemsPerPageChange={handleItemsPerPageChange}
                  itemsPerPageOptions={itemsPerPageOptions}
                />
              )}
            </>
          )}
        </div>
      )}

      <DeleteModal
        isOpen={isDeleteModalOpen}
        onClose={cancelDelete}
        onConfirm={confirmDelete}
        itemType="reminder"
        itemTitle={reminderToDelete?.title || "Untitled Reminder"}
      />

      <CreateReminderModal
        isOpen={isCreateModalOpen}
        closeModal={closeCreateModal}
        saveReminder={saveNewReminder}
      />
    </div>
  );
};

export default Reminder;
