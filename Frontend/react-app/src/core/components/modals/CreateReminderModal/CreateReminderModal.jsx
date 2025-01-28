import React, { useState } from "react";
import ReactDOM from "react-dom";
import "./CreateReminderModal.css";

const CreateReminderModal = ({ isOpen, closeModal, saveReminder }) => {
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [notifyAt, setNotifyAt] = useState("");
  const [priority, setPriority] = useState(1);

  const handleSave = () => {
    if (!title.trim() || !notifyAt) {
      alert("Title and Notification time are required!");
      return;
    }

    const newReminder = {
      id: Date.now().toString(),
      title,
      description,
      notifyAt,
      priority,
    };

    saveReminder(newReminder);
    closeModal();
    setTitle("");
    setDescription("");
    setNotifyAt("");
    setPriority(1);
  };

  if (!isOpen) return null;

  return ReactDOM.createPortal(
    <div className="create-reminder-modal-overlay">
      <div className="create-reminder-modal-content">
        <h3>Create Reminder</h3>
        <input
          type="text"
          placeholder="Reminder Title"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          className="reminder-title-input"
        />
        <textarea
          placeholder="Description (Optional)"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          className="reminder-description-input"
        />
        <label className="create-reminder-modal-label">Notify At:</label>
        <input
          type="datetime-local"
          value={notifyAt}
          onChange={(e) => setNotifyAt(e.target.value)}
          className="reminder-datetime-input"
        />
        <label className="create-reminder-modal-label">Priority:</label>
        <select
          value={priority}
          onChange={(e) => setPriority(Number(e.target.value))}
          className="reminder-priority-input"
        >
          <option value={1}>Low</option>
          <option value={2}>Medium</option>
          <option value={3}>High</option>
        </select>

        <div className="modal-buttons">
          <button className="save-button" onClick={handleSave}>Save</button>
          <button className="close-button" onClick={closeModal}>Cancel</button>
        </div>
      </div>
    </div>,
    document.body
  );
};

export default CreateReminderModal;
