import React, { useEffect, useState } from "react";
import { toast } from "react-toastify";

import RemindersList from "../../../../core/components/lists/RemindersList/RemindersList";
import CreateReminderModal from "../../../../core/components/modals/CreateReminderModal/CreateReminderModal";
import DeleteModal from "../../../../core/components/modals/DeleteModal/DeleteModal";
import ReminderService from "../../../../services/ReminderService";

import "react-toastify/dist/ReactToastify.css";
import "./Reminder.css";

const Reminder = ({ nodeId, onToggle }) => {
  const root = "reminder";
  const [isRemindersExpanded, setIsRemindersExpanded] = useState(false);
  const [isCreateModalOpen, setCreateModalOpen] = useState(false);
  const [reminders, setReminders] = useState([]);
  const [reminderToDelete, setReminderToDelete] = useState(null);
  const [isDeleteModalOpen, setIsDeleteModalOpen] = useState(false);
  const [isLoading, setIsLoading] = useState(false);

  useEffect(() => {
    const fetchReminders = async () => {
      if (isRemindersExpanded && nodeId) {
        setIsLoading(true);
        try {
          const remindersData = await ReminderService.getRemindersByNode(nodeId);
          setReminders(remindersData);
        } catch (error) {
          toast.error("Failed to load reminders");
        } finally {
          setIsLoading(false);
        }
      }
    };

    fetchReminders();
  }, [isRemindersExpanded, nodeId]);

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
      const reminderWithNodeId = { ...newReminder, nodeId };
      await ReminderService.createReminder(reminderWithNodeId);
      const updatedReminders = await ReminderService.getRemindersByNode(nodeId);
      setReminders(updatedReminders);
      toast.success("Reminder created successfully!");
      closeCreateModal();
    } catch (error) {
      toast.error(error.message || "Failed to create reminder");
    }
  };

  const confirmDelete = async () => {
    if (!reminderToDelete) return;
    
    try {
      await ReminderService.deleteReminder(reminderToDelete.id);
      setReminders(prev => prev.filter(r => r.id !== reminderToDelete.id));
      toast.success("Reminder deleted successfully!");
    } catch (error) {
      toast.error(error.message || "Failed to delete reminder");
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
            <RemindersList
              root={root}
              reminders={reminders}
              openCreateModal={openCreateModal}
              setReminderToDelete={setReminderToDelete}
              setIsDeleteModalOpen={setIsDeleteModalOpen}
            />
          )}
        </div>
      )}

      <DeleteModal
        isOpen={isDeleteModalOpen}
        onClose={cancelDelete}
        onConfirm={confirmDelete}
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
