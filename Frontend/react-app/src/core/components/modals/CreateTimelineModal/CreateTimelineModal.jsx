import React, { useState } from "react";
import { toast } from "react-toastify";
import TimelineService from "../../../../services/TimelineService";
import "./CreateTimelineModal.css";

const CreateTimelineModal = ({ onClose, onTimelineCreated }) => {
  const [title, setTitle] = useState("");
  const [error, setError] = useState("");

  const handleTitleChange = (e) => {
    setTitle(e.target.value);
    if (error) setError("");
  };

  const handleCreateTimelineData = async () => {
    try {
      await TimelineService.createTimeline(title);
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
        <div className="create-timeline-modal-input">
          <label htmlFor="title">Title:</label>
          <input
            id="title"
            type="text"
            value={title}
            onChange={handleTitleChange}
            placeholder="Enter title"
            className={error ? "create-timeline-input-error" : ""}
          />
          {error && <div className="create-timeline-error-message">{error}</div>}
        </div>

        <div className="create-timeline-modal-buttons">
          <button
            className="create-timeline-modal-button-close"
            onClick={onClose}
          >
            Close
          </button>
          <button
            className="create-timeline-modal-button"
            onClick={handleCreateTimelineData}
            disabled={!title.trim()}
          >
            Save
          </button>
        </div>
      </div>
    </div>
  );
};

export default CreateTimelineModal;
