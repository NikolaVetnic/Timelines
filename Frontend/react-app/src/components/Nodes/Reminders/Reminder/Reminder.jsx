import React, { useEffect, useState } from "react";
import { toast } from "react-toastify";

import RemindersList from "../../../../core/components/lists/RemindersList/RemindersList";
import CreateReminderModal from "../../../../core/components/modals/CreateReminderModal/CreateReminderModal";
import { LOCAL_STORAGE_KEY } from "../../../../data/constants";

import "react-toastify/dist/ReactToastify.css";
import "./Reminder.css";

const Reminder = ({ nodeId, timelineId, onToggle }) => {
  const root = "reminder";
  const [isRemindersExpanded, setIsRemindersExpanded] = useState(false);
  const [isCreateModalOpen, setCreateModalOpen] = useState(false);
  const [reminders, setReminders] = useState([]);

  useEffect(() => {
    const storedData = localStorage.getItem(LOCAL_STORAGE_KEY);
    if (storedData) {
      const parsedData = JSON.parse(storedData);
      const timeline = parsedData.find((t) => t.id === timelineId);
      const node = timeline?.nodes.find((n) => n.id === nodeId);
      if (node) {
        setReminders(node.reminders || []);
      }
    }
  }, [timelineId, nodeId]);

  const updateLocalStorage = (updatedReminders) => {
    const storedData = localStorage.getItem(LOCAL_STORAGE_KEY);
    if (storedData) {
      const parsedData = JSON.parse(storedData);
      const timelineIndex = parsedData.findIndex((t) => t.id === timelineId);
      if (timelineIndex !== -1) {
        const nodeIndex = parsedData[timelineIndex].nodes.findIndex(
          (n) => n.id === nodeId
        );
        if (nodeIndex !== -1) {
          parsedData[timelineIndex].nodes[nodeIndex].reminders = updatedReminders;
          localStorage.setItem(LOCAL_STORAGE_KEY, JSON.stringify(parsedData));
        }
      }
    }
  };

  const toggleRemindersSection = () => {
    setIsRemindersExpanded((prev) => !prev);
    onToggle();
  };

  const handleRemoveReminder = (id) => {
    const updatedReminders = reminders.filter((reminder) => reminder.id !== id);
    setReminders(updatedReminders);
    updateLocalStorage(updatedReminders);
    setTimeout(() => onToggle(), 0);
  };

  const openCreateModal = (e) => {
    e.stopPropagation();
    setCreateModalOpen(true);
  };

  const closeCreateModal = () => {
    setCreateModalOpen(false);
  };

  const saveNewReminder = (newReminder) => {
    const updatedReminders = [...reminders, newReminder];
    setReminders(updatedReminders);
    updateLocalStorage(updatedReminders);
    setTimeout(() => onToggle(), 0);
    toast.success("New reminder added!");
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
          <RemindersList
            root={root}
            reminders={reminders}
            openCreateModal={openCreateModal}
            handleRemoveReminder={handleRemoveReminder}
          />
        </div>
      )}

      <CreateReminderModal
        isOpen={isCreateModalOpen}
        closeModal={closeCreateModal}
        saveReminder={saveNewReminder}
      />
    </div>
  );
};

export default Reminder;
