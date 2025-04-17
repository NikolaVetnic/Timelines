import React, { useEffect, useState } from "react";
import ReactDOM from "react-dom";
import Button from "../../buttons/Button/Button";
import FormField from "../../forms/FormField/FormField";
import "./CreateReminderModal.css";

const CreateReminderModal = ({ isOpen, closeModal, saveReminder, nodeId }) => {
  const root = "create-reminder-modal";
  const [formData, setFormData] = useState({
    title: "",
    description: "",
    dueDateTime: "",
    priority: 1,
    notificationTime: "",
    status: "Pending"
  });
  const [errors, setErrors] = useState({});

   useEffect(() => {
    if (!isOpen) {
      setFormData({
        title: "",
        description: "",
        dueDateTime: "",
        priority: 1,
        notificationTime: "",
        status: "Pending"
      });
      setErrors({});
    }
  }, [isOpen]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: name === "priority" ? Number(value) : value
    }));
  };

  const validate = () => {
    const newErrors = {};
    if (!formData.title.trim()) newErrors.title = "Title is required";
    if (!formData.dueDateTime) newErrors.dueDateTime = "Due date is required";
    if (!formData.notificationTime) newErrors.notificationTime = "Notification time is required";
    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSave = () => {
  if (!validate()) return;

  const dueDateTimeUTC = formData.dueDateTime ? new Date(formData.dueDateTime).toISOString() : new Date().toISOString();
  const notificationTimeUTC = formData.notificationTime ? new Date(formData.notificationTime).toISOString() : new Date().toISOString();

  const newReminder = {
    title: formData.title,
    description: formData.description || "",
    dueDateTime: dueDateTimeUTC,
    priority: formData.priority || 0,
    notificationTime: notificationTimeUTC,
    status: formData.status || "Pending",
    nodeId: nodeId
  };

  saveReminder(newReminder);
  closeModal();
};

  if (!isOpen) return null;

  return ReactDOM.createPortal(
    <div className={`${root}-overlay`}>
      <div className={`${root}-content`}>
        <div className={`${root}-header`}>Create Reminder</div>
        
        <FormField
          label="Title"
          type="text"
          name="title"
          value={formData.title}
          onChange={handleChange}
          placeholder="Reminder Title"
          required
          error={errors.title}
        />
        
        <FormField
          label="Description"
          type="textarea"
          name="description"
          value={formData.description}
          onChange={handleChange}
          placeholder="Description (Optional)"
        />
        
        <FormField
          label="Due Date & Time"
          type="datetime-local"
          name="dueDateTime"
          value={formData.dueDateTime}
          onChange={handleChange}
          required
          error={errors.dueDateTime}
        />
        
        <FormField
          label="Notification Time"
          type="datetime-local"
          name="notificationTime"
          value={formData.notificationTime}
          onChange={handleChange}
          required
          error={errors.notificationTime}
        />
        
        <FormField
          label="Priority"
          type="select"
          name="priority"
          value={formData.priority}
          onChange={handleChange}
        >
          <option value={1}>Low</option>
          <option value={2}>Medium</option>
          <option value={3}>High</option>
        </FormField>

        <div className={`${root}-actions`}>
          <Button 
            text="Cancel"
            variant="secondary"
            onClick={closeModal}
            />
          <Button 
            text="Create" 
            variant="success" 
            onClick={handleSave}
          />
        </div>
      </div>
    </div>,
    document.body
  );
};

export default CreateReminderModal;
