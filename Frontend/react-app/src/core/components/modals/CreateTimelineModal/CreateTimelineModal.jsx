import React, { useEffect, useState } from "react";
import TimelineService from "../../../../services/TimelineService";
import Button from "../../buttons/Button/Button";
import FormField from "../../forms/FormField/FormField";
import "./CreateTimelineModal.css";

const CreateTimelineModal = ({ isOpen, onClose, onTimelineCreated, initialTemplate }) => {
  const [title, setTitle] = useState("");
  const [isModalOpen, setModalOpen] = useState(isOpen);
  const [description, setDescription] = useState("");
  const [error, setError] = useState("");
  const [useTemplate, setUseTemplate] = useState(!!initialTemplate);
  const [timelines, setTimelines] = useState([]);
  const [selectedTemplate, setSelectedTemplate] = useState(initialTemplate || "");
  const [loadingTemplates, setLoadingTemplates] = useState(false);
  const [isFetchingTemplate, setIsFetchingTemplate] = useState(false);
  
  useEffect(() => {
    setModalOpen(isOpen);
  }, [isOpen]);

  useEffect(() => {
    if (useTemplate) {
      fetchTimelines();
    } else {
      setTitle("");
      setDescription("");
      setSelectedTemplate("");
    }
  }, [useTemplate]);

  useEffect(() => {
    if (initialTemplate) {
      setUseTemplate(true);
      setSelectedTemplate(initialTemplate);
      fetchTemplateData(initialTemplate);
    }
  }, [initialTemplate]);

  const fetchTimelines = async () => {
    try {
      setLoadingTemplates(true);
      const response = await TimelineService.getAllTimelines(0, 100);
      setTimelines(response.items);
    } finally {
      setLoadingTemplates(false);
    }
  };

  const fetchTemplateData = async (timelineId) => {
    setIsFetchingTemplate(true);
    let timeline;
    if(timelineId instanceof Object) {
      timeline = await TimelineService.getTimelineById(timelineId.target.value);
    } else {
      timeline = await TimelineService.getTimelineById(timelineId);
    }
    setTitle(timeline.title || "");
    setDescription(timeline.description || "");
    setIsFetchingTemplate(false);
  };

  const handleTitleChange = (e) => {
    setTitle(e.target.value);
    if (error) setError("");
  };

  const handleDescriptionChange = (e) => {
    setDescription(e.target.value);
  };

  const handleTemplateSelect = async (selectedValue) => {
    if (!selectedValue) return;
    
    setSelectedTemplate(selectedValue);
    await fetchTemplateData(selectedValue);
  };

  const handleCreateTimeline = async () => {
    if (useTemplate && selectedTemplate) {
      if(selectedTemplate instanceof Object) {
        await TimelineService.cloneTimeline(selectedTemplate.target.value, title, description);
      } else {
        await TimelineService.cloneTimeline(selectedTemplate, title, description);
      }
    } else {
      await TimelineService.createTimeline(title, description);
    }
    onClose();
    if (onTimelineCreated) onTimelineCreated();
  };

  const handleTemplateToggle = () => {
    if (initialTemplate) {
      return;
    }
    setUseTemplate(!useTemplate);
    if (!useTemplate) {
      setTitle("");
      setDescription("");
      setSelectedTemplate("");
    }
  };

  if (!isModalOpen) return null;

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
              onChange={handleTemplateToggle}
              disabled={!!initialTemplate}
            />
            <span className="react-toggle-slider"></span>
          </label>
        </div>

        {useTemplate && (
          <div className="create-timeline-modal-input">
            <FormField
              label="Select Timeline as a Template"
              type="search-select"
              name="template"
              value={selectedTemplate}
              onChange={handleTemplateSelect}
              options={timelines.map((t) => ({
                value: t.id,
                label: t.title,
              }))}
              placeholder={
                loadingTemplates ? "Loading timelines..." : "Search timelines..."
              }
              disabled={loadingTemplates || isFetchingTemplate || initialTemplate}
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
            disabled={isFetchingTemplate}
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
            disabled={isFetchingTemplate}
          />
        </div>

        {error && <div className="create-timeline-error-message">{error}</div>}

        <div className="create-timeline-modal-buttons">
          <Button 
            text="Cancel" 
            onClick={onClose} 
            size="small" 
            variant="secondary"
            disabled={isFetchingTemplate}
          />
          <Button
            text={useTemplate ? "Clone Timeline" : "Create Timeline"}
            variant="success"
            size="small"
            onClick={handleCreateTimeline}
            disabled={!title.trim() || (useTemplate && !selectedTemplate) || isFetchingTemplate}
            loading={isFetchingTemplate ? "true" : undefined}  // Fix for boolean attribute warning
          />
        </div>
      </div>
    </div>
  );
};

export default CreateTimelineModal;
