import React, { useState } from "react";
import ReactDOM from "react-dom";

import "./CreateReminderModal.css";

const CreateReminderModal = ({ isOpen, closeModal, saveReminder }) => {
  const root = "create-reminder-modal";
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [notifyAt, setNotifyAt] = useState("");
  const [priority, setPriority] = useState(1);
  const [notified, setNotified] = useState(false);

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
      notified,
      priority,
    };

    saveReminder(newReminder);
    closeModal();
    setTitle("");
    setDescription("");
    setNotified(false);
    setNotifyAt("");
    setPriority(1);
  };

  if (!isOpen) return null;

  return ReactDOM.createPortal(
    <div className={`${root}-overlay`}>
      <div className={`${root}-content`}>
        <h3>Create Reminder</h3>
        <input
          type="text"
          placeholder="Reminder Title"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          className={`${root}-title-input`}
        />
        <textarea
          placeholder="Description (Optional)"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          className={`${root}-description-input`}
        />
        <label className={`${root}-label`}>Notify At:</label>
        <input
          type="datetime-local"
          value={notifyAt}
          onChange={(e) => setNotifyAt(e.target.value)}
          className={`${root}-datetime-input`}
        />
        <label className={`${root}-label`}>Priority:</label>
        <select
          value={priority}
          onChange={(e) => setPriority(Number(e.target.value))}
          className={`${root}-priority-input`}
        >
          <option value={1}>Low</option>
          <option value={2}>Medium</option>
          <option value={3}>High</option>
        </select>

        <div className={`${root}-buttons`}>
          <button className={`${root}-save-button`} onClick={handleSave}>Save</button>
          <button className={`${root}-close-button`} onClick={closeModal}>Cancel</button>
        </div>
      </div>
    </div>,
    document.body
  );
};

export default CreateReminderModal;
