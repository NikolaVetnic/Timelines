import React, { useEffect, useState } from "react";
import { toast } from "react-toastify";
import TimelineService from "../../../../services/TimelineService";
import Button from "../../buttons/Button/Button";
import "./EditTimelineModal.css";

const EditTimelineModal = ({ timeline, onClose, onTimelineUpdated }) => {
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [error, setError] = useState("");
  const [isLoading, setIsLoading] = useState(false);

  useEffect(() => {
    if (timeline) {
      setTitle(timeline.title || "");
      setDescription(timeline.description || "");
    }
  }, [timeline]);

  const handleTitleChange = (e) => {
    setTitle(e.target.value);
    if (error) setError("");
  };

  const handleDescriptionChange = (e) => {
    setDescription(e.target.value);
  };

  const handleUpdateTimeline = async () => {
    if (!title.trim()) {
      setError("Title is required");
      return;
    }

    setIsLoading(true);
    try {
      await TimelineService.updateTimeline(timeline.id, title, description);
      toast.success("Timeline updated successfully");
      onClose();
      if (onTimelineUpdated) onTimelineUpdated();
    } catch (error) {
      setError(error.message);
      toast.error(error.message);
    } finally {
      setIsLoading(false);
    }
  };

  if (!timeline) return null;

  return (
    <div className="edit-timeline-modal">
      <div className="edit-timeline-modal-content">
        <div className="edit-timeline-modal-header">Edit Timeline</div>

        <div className="edit-timeline-modal-input">
          <label htmlFor="title">Title*:</label>
          <input
            id="title"
            type="text"
            value={title}
            onChange={handleTitleChange}
            placeholder="Enter timeline title"
            className={error ? "edit-timeline-input-error" : ""}
          />
        </div>

        <div className="edit-timeline-modal-input">
          <label htmlFor="description">Description:</label>
          <textarea
            id="description"
            value={description}
            onChange={handleDescriptionChange}
            placeholder="Enter timeline description (optional)"
            rows="4"
          />
        </div>

        {error && <div className="edit-timeline-error-message">{error}</div>}

        <div className="edit-timeline-modal-buttons">
          <Button 
            text="Cancel" 
            onClick={onClose} 
            size="small" 
            disabled={isLoading}
          />
          <Button
            text={isLoading ? "Updating..." : "Update"}
            variant="success"
            size="small"
            onClick={handleUpdateTimeline}
            disabled={!title.trim() || isLoading}
          />
        </div>
      </div>
    </div>
  );
};

export default EditTimelineModal;
