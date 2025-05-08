import React, { useCallback, useEffect, useState } from "react";
import RemindersList from "../../../../core/components/lists/RemindersList/RemindersList";
import CreateReminderModal from "../../../../core/components/modals/CreateReminderModal/CreateReminderModal";
import DeleteModal from "../../../../core/components/modals/DeleteModal/DeleteModal";
import Pagination from "../../../../core/components/pagination/Pagination";
import ReminderService from "../../../../services/ReminderService";
import "./Reminder.css";

const Reminder = ({ node, onToggle }) => {
  const root = "reminder";
  const [isRemindersExpanded, setIsRemindersExpanded] = useState(false);
  const [isCreateModalOpen, setCreateModalOpen] = useState(false);
  const [reminders, setReminders] = useState([]);
  const [reminderToDelete, setReminderToDelete] = useState(null);
  const [isDeleteModalOpen, setIsDeleteModalOpen] = useState(false);
  const [isLoading, setIsLoading] = useState(false);
  
  const [currentPage, setCurrentPage] = useState(0);
  const [itemsPerPage, setItemsPerPage] = useState(2);
  const [totalCount, setTotalCount] = useState(0);
  const itemsPerPageOptions = [2, 4, 6, 8];

  const fetchReminders = useCallback(async () => {
    if (isRemindersExpanded && node.id) {
      setIsLoading(true);
      try {
        const response = await ReminderService.getRemindersByNode(
          node.id,
          currentPage,
          itemsPerPage
        );
        setReminders(response.items || []);
        setTotalCount(response.totalCount || 0);
      } finally {
        setIsLoading(false);
      }
    }
  }, [isRemindersExpanded, node.id, currentPage, itemsPerPage]);

  useEffect(() => {
    fetchReminders();
  }, [fetchReminders]);

  const totalPages = Math.ceil(totalCount / itemsPerPage) || 1;

  const handlePageChange = (page) => {
    setCurrentPage(page - 1);
  };

  const handleItemsPerPageChange = (size) => {
    setItemsPerPage(size);
    setCurrentPage(0);
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
  try {
    setIsLoading(true);
    const reminderWithNodeId = { ...newReminder, nodeId: node.id };
   await ReminderService.createReminder(reminderWithNodeId);

  } finally {
    await fetchReminders();
    setIsLoading(false);
    closeCreateModal();
  }
};

  const confirmDelete = async () => {
    if (!reminderToDelete) return;
    try {
      setIsLoading(true);
      await ReminderService.deleteReminder(reminderToDelete.id);
      await fetchReminders();
    } finally {
      setIsLoading(false);
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
                reminders={reminders}
                openCreateModal={openCreateModal}
                setReminderToDelete={setReminderToDelete}
                setIsDeleteModalOpen={setIsDeleteModalOpen}
              />
              {totalCount > 0 && (
                <Pagination
                  currentPage={currentPage + 1}
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
