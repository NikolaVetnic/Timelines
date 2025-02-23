import React, { useEffect, useState } from "react";
import { toast } from "react-toastify";

import { LOCAL_STORAGE_KEY } from "../../../../data/constants";
import RemoveButton from "../../../../core/components/buttons/RemoveButton/RemoveButton";
import CreateReminderModal from "../../../../core/components/modals/CreateReminderModal/CreateReminderModal";

import "./Reminder.css";
import "react-toastify/dist/ReactToastify.css";

const Reminder = ({ nodeId, timelineId, onToggle }) => {
  const [isRemindersExpanded, setIsRemindersExpanded] = useState(false);
  const [isCreateModalOpen, setCreateModalOpen] = useState(false);
  const [reminders, setReminders] = useState([]);

  useEffect(() => {
    const storedData = localStorage.getItem(LOCAL_STORAGE_KEY);
    if (storedData) {
      const parsedData = JSON.parse(storedData);
      const timeline = parsedData.find(t => t.id === timelineId);
      const node = timeline?.nodes.find(n => n.id === nodeId);
      if (node) {
        setReminders(node.reminders || []);
      }
    }
  }, [timelineId, nodeId]);

  // todo: connect to backend
  const updateLocalStorage = (updatedReminders) => {
    const storedData = localStorage.getItem(LOCAL_STORAGE_KEY);
    if (storedData) {
      const parsedData = JSON.parse(storedData);
      const timelineIndex = parsedData.findIndex(t => t.id === timelineId);
      if (timelineIndex !== -1) {
        const nodeIndex = parsedData[timelineIndex].nodes.findIndex(n => n.id === nodeId);
        if (nodeIndex !== -1) {
          parsedData[timelineIndex].nodes[nodeIndex].reminders = updatedReminders;
          localStorage.setItem(LOCAL_STORAGE_KEY, JSON.stringify(parsedData));
        }
      }
    }
  };

  const toggleRemindersSection = () => {
    setIsRemindersExpanded(prev => !prev);
    onToggle();
  };

  const handleRemoveReminder = (id) => {
    const updatedReminders = reminders.filter(reminder => reminder.id !== id);
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
    <div className="reminders-section">
      <button
        className={`reminders-header ${
          isRemindersExpanded ? "reminder-headers-opened" : "reminder-headers-closed"
        }`}
        onClick={toggleRemindersSection}
      >
        <h4>Reminders</h4>
        <span>{isRemindersExpanded ? "-" : "+"}</span>
      </button>

      {isRemindersExpanded && (
        <div className="reminders-container">
          <button className="add-reminder-button" onClick={openCreateModal}>+</button>
          {reminders.length > 0 ?reminders.map((reminder) => (
            <div key={reminder.id} className="reminder-item">
              <div className="reminder-content">
                <p>{reminder.title}</p>
                <p><strong>Notify At:</strong> {new Date(reminder.notifyAt).toLocaleString()}</p>
                <p><strong>Priority:</strong> {reminder.priority}</p>
              </div>
              <RemoveButton id={reminder.id} onRemove={handleRemoveReminder} message="Reminder removed."/>
            </div>
          )) : <p>There are no current reminders.</p>}
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
