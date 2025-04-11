import React, { useState } from "react";
import { toast } from "react-toastify";
import TimelineService from "../../../../services/TimelineService";
import Button from "../../buttons/Button/Button";
import "./CreateTimelineModal.css";

const CreateTimelineModal = ({ onClose, onTimelineCreated }) => {
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [error, setError] = useState("");

  const handleTitleChange = (e) => {
    setTitle(e.target.value);
    if (error) setError("");
  };

  const handleDescriptionChange = (e) => {
    setDescription(e.target.value);
  };

  const handleCreateTimelineData = async () => {
    try {
      await TimelineService.createTimeline(title, description);
      onClose();
      if (onTimelineCreated) onTimelineCreated();
    } catch (error) {
      setError(error.message);
      toast.error(error.message);
    }
  };

  return (
    <div className="create-timeline-modal">
      <div className="create-timeline-modal-content">
        <div className="create-timeline-modal-header">Create New Timeline</div>

        <div className="create-timeline-modal-input">
          <label htmlFor="title">Title*:</label>
          <input
            id="title"
            type="text"
            value={title}
            onChange={handleTitleChange}
            placeholder="Enter timeline title"
            className={error ? "create-timeline-input-error" : ""}
          />
        </div>

        <div className="create-timeline-modal-input">
          <label htmlFor="description">Description:</label>
          <textarea
            id="description"
            value={description}
            onChange={handleDescriptionChange}
            placeholder="Enter timeline description (optional)"
            rows="4"
          />
        </div>

        {error && <div className="create-timeline-error-message">{error}</div>}

        <div className="create-timeline-modal-buttons">
          <Button text="Cancel" onClick={onClose} />
          <Button
            text="Create"
            variant="success"
            onClick={handleCreateTimelineData}
            disabled={!title.trim()}
          />
        </div>
      </div>
    </div>
  );
};

export default CreateTimelineModal;
