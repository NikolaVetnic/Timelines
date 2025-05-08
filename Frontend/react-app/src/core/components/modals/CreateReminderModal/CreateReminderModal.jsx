import React, { useEffect, useState } from "react";
import ReactDOM from "react-dom";
import { PRIORITY_OPTIONS } from "../../../../data/constants";
import Button from "../../buttons/Button/Button";
import FormField from "../../forms/FormField/FormField";
import "./CreateReminderModal.css";

const CreateReminderModal = ({ isOpen, closeModal, saveReminder, nodeId }) => {
  const root = "create-reminder-modal";
  const [formData, setFormData] = useState({
    title: "",
    description: "",
    notifyAt: "",
    priority: 1,
  });
  const [errors, setErrors] = useState({});

  useEffect(() => {
    if (!isOpen) {
      setFormData({
        title: "",
        description: "",
        priority: 1,
        notifyAt: "",
      });
      setErrors({});
    }
  }, [isOpen]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: name === "priority" ? Number(value) : value,
    }));
  };

  const validate = () => {
    const newErrors = {};
    if (!formData.title.trim()) newErrors.title = "Title is required";
    if (!formData.description.trim())
      newErrors.description = "Description is required.";
    if (!formData.notifyAt) newErrors.notifyAt = "Notify at is required";
    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSave = () => {
    if (!validate()) return;

    const notifyAtUTC = formData.notifyAt
      ? new Date(formData.notifyAt).toISOString()
      : new Date().toISOString();

    const newReminder = {
      title: formData.title,
      description: formData.description || "",
      notifyAt: notifyAtUTC,
      priority: formData.priority || 0,
      nodeId: nodeId,
    };

    saveReminder(newReminder);
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
          required
          error={errors.description}
          placeholder="Description (Optional)"
        />

        <FormField
          label="Notify At"
          type="datetime-local"
          name="notifyAt"
          value={formData.notifyAt}
          onChange={(e) => {
            const value = e.target.value;
            setFormData(prev => ({
              ...prev,
              notifyAt: value ? value.replace("Z", "") : value
            }));
          }}
          required
          error={errors.notifyAt}
        />
        <FormField
          label="Priority Level"
          type="select"
          name="priority"
          value={formData.priority}
          onChange={handleChange}
          options={PRIORITY_OPTIONS}
          required
          error={errors.priority}
        />

        <div className={`${root}-actions`}>
          <Button
            text="Cancel"
            variant="secondary"
            onClick={closeModal}
            size="small"
          />
          <Button
            text="Create"
            variant="success"
            onClick={handleSave}
            size="small"
          />
        </div>
      </div>
    </div>,
    document.body
  );
};

export default CreateReminderModal;
