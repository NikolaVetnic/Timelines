import React, { useEffect, useState } from "react";
import { toast } from "react-toastify";
import TimelineService from "../../../../services/TimelineService";
import Button from "../../buttons/Button/Button";
import FormField from "../../forms/FormField/FormField";
import "./CreateTimelineModal.css";

const CreateTimelineModal = ({ onClose, onTimelineCreated }) => {
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [error, setError] = useState("");
  const [useTemplate, setUseTemplate] = useState(false);
  const [timelines, setTimelines] = useState([]);
  const [selectedTemplate, setSelectedTemplate] = useState("");
  const [loadingTemplates, setLoadingTemplates] = useState(false);

  useEffect(() => {
    if (useTemplate) {
      fetchTimelines();
    }
  }, [useTemplate]);

  const fetchTimelines = async () => {
    try {
      setLoadingTemplates(true);
      const response = await TimelineService.getAllTimelines(0, 100);
      setTimelines(response.items);
    } catch (error) {
      toast.error("Failed to load timelines");
    } finally {
      setLoadingTemplates(false);
    }
  };

  const handleTitleChange = (e) => {
    setTitle(e.target.value);
    if (error) setError("");
  };

  const handleDescriptionChange = (e) => {
    setDescription(e.target.value);
  };

  const handleCreateTimeline = async () => {
      if (useTemplate && selectedTemplate) {
        await TimelineService.cloneTimeline(selectedTemplate, title, description);
      } else {
        await TimelineService.createTimeline(title, description);
      }
      onClose();
      if (onTimelineCreated) onTimelineCreated();
  };

  return (
    <div className="create-timeline-modal">
      <div className="create-timeline-modal-content">
        <div className="create-timeline-modal-header">
          {useTemplate ? "Clone Timeline" : "Create New Timeline"}
        </div>

        <div className="create-timeline-template-toggle">
          <label>Use existing timeline as template:</label>
          <label className="react-toggle-switch">
            <input
              type="checkbox"
              checked={useTemplate}
              onChange={() => setUseTemplate(!useTemplate)}
            />
            <span className="react-toggle-slider"></span>
          </label>
        </div>

        {useTemplate && (
          <div className="create-timeline-modal-input">
            <FormField
              label="Select Timeline as an Template"
              type="select"
              name="template"
              value={selectedTemplate}
              onChange={(e) => setSelectedTemplate(e.target.value)}
              options={timelines.map(t => ({
                value: t.id,
                label: t.title
              }))}
              placeholder={loadingTemplates ? "Loading timelines..." : "Select a timeline"}
              disabled={loadingTemplates}
            />
          </div>
        )}

        <div className="create-timeline-modal-input">
          <FormField
            label="Title"
            type="text"
            name="title"
            value={title}
            onChange={handleTitleChange}
            placeholder="Enter timeline title"
            required
            error={error}
          />
        </div>

        <div className="create-timeline-modal-input">
          <FormField
            label="Description"
            type="textarea"
            name="description"
            value={description}
            onChange={handleDescriptionChange}
            placeholder="Enter timeline description (optional)"
          />
        </div>

        {error && <div className="create-timeline-error-message">{error}</div>}

        <div className="create-timeline-modal-buttons">
          <Button 
            text="Cancel" 
            onClick={onClose} 
            size="small" 
            variant="secondary"
          />
          <Button
            text={useTemplate ? "Clone Timeline" : "Create Timeline"}
            variant="success"
            size="small"
            onClick={handleCreateTimeline}
            disabled={!title.trim() || (useTemplate && !selectedTemplate)}
          />
        </div>
      </div>
    </div>
  );
};

export default CreateTimelineModal;
