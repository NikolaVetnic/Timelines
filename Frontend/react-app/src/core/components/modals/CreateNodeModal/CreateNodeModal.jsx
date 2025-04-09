import React, { useEffect, useState } from "react";
import { toast } from "react-toastify";
import NodeService from "../../../../services/NodeService";
import FormField from "../../forms/FormField/FormField";
import "./CreateNodeModal.css";

const INITIAL_NODE_DATA = {
  title: "",
  description: "",
  phase: "",
  timestamp: new Date().toISOString().slice(0, 16),
  importance: 1,
  categories: "",
  tags: "",
};

const INITIAL_ERRORS = {
  title: "",
  description: "",
  tags: "",
  categories: "",
};

const CreateNodeModal = ({ isOpen, onClose, timelineId, onNodeCreated }) => {
  const root = "create-node-modal";
  const [isModalOpen, setModalOpen] = useState(isOpen);
  const [nodeData, setNodeData] = useState(INITIAL_NODE_DATA);
  const [errors, setErrors] = useState(INITIAL_ERRORS);
  const [isCreating, setIsCreating] = useState(false);

  useEffect(() => {
    setModalOpen(isOpen);
    if (!isOpen) {
      setNodeData(INITIAL_NODE_DATA);
      setErrors(INITIAL_ERRORS);
    }
  }, [isOpen]);

  const closeModal = () => {
    setModalOpen(false);
    onClose();
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setNodeData((prevData) => ({
      ...prevData,
      [name]: value,
    }));

    if (errors[name]) {
      setErrors((prev) => ({ ...prev, [name]: "" }));
    }
  };

  const validateForm = () => {
    const newErrors = { ...INITIAL_ERRORS };
    let isValid = true;

    if (!nodeData.title.trim()) {
      newErrors.title = "Title is required";
      isValid = false;
    }

    if (!nodeData.description.trim()) {
      newErrors.description = "Description is required";
      isValid = false;
    }

    const formattedTags = formatTags(nodeData.tags);
    if (formattedTags.length === 0) {
      newErrors.tags = "There should be at least one tag";
      isValid = false;
    }

    const formattedCategories = formatCategories(nodeData.categories);
    if (formattedCategories.length === 0) {
      newErrors.categories = "There should be at least one category";
      isValid = false;
    }

    setErrors(newErrors);
    return isValid;
  };

  const formatTags = (tags) =>
    tags
      .split(",")
      .map((tag) => tag.trim().toLowerCase().replace(/\s+/g, "-"))
      .filter((tag) => tag.length > 0);

  const formatCategories = (categories) =>
    categories
      .split(",")
      .map((category) =>
        category
          .trim()
          .toLowerCase()
          .replace(/\b\w/g, (char) => char.toUpperCase())
      )
      .filter((category) => category.length > 0);

  const handleSave = async () => {
    if (!validateForm()) {
      return;
    }

    try {
      setIsCreating(true);

      await NodeService.createNode({
        ...nodeData,
        tags: formatTags(nodeData.tags),
        categories: formatCategories(nodeData.categories),
        timestamp: new Date(nodeData.timestamp).toISOString(),
        timelineId: timelineId,
      });

      toast.success("Node created successfully!");
      onNodeCreated();
      closeModal();
    } catch (error) {
      toast.error(error.message || "Failed to create node");
    } finally {
      setIsCreating(false);
    }
  };

  if (!isModalOpen) return null;

  return (
    <div className="create-node-modal-overlay">
      <div className="create-node-modal-content">
        <div className="create-node-modal-header">
          <h3>Add New Node</h3>
        </div>

        <FormField
          label="Title"
          type="text"
          name="title"
          value={nodeData.title}
          onChange={handleChange}
          required
          error={errors.title}
        />
        <FormField
          label="Description"
          type="textarea"
          name="description"
          value={nodeData.description}
          onChange={handleChange}
          required
          error={errors.description}
        />
        <FormField
          label="Phase"
          type="text"
          name="phase"
          value={nodeData.phase}
          onChange={handleChange}
        />
        <FormField
          label="Timestamp"
          type="datetime-local"
          name="timestamp"
          value={nodeData.timestamp}
          onChange={handleChange}
        />
        <FormField
          label="Importance"
          type="number"
          name="importance"
          value={nodeData.importance}
          onChange={handleChange}
          min="1"
          max="10"
        />
        <FormField
          label="Tags"
          type="text"
          name="tags"
          value={nodeData.tags}
          onChange={handleChange}
          required
          placeholder="tag1, tag2..."
          error={errors.tags}
        />
        <FormField
          label="Categories"
          type="text"
          name="categories"
          value={nodeData.categories}
          onChange={handleChange}
          required
          placeholder="Category1, Category2..."
          error={errors.categories}
        />

        <div className={`${root}-actions`}>
          <button
            className={`${root}-save-btn`}
            onClick={handleSave}
            disabled={isCreating}
          >
            {isCreating ? "Creating..." : "Save"}
          </button>
          <button
            className={`${root}-cancel-btn`}
            onClick={closeModal}
            disabled={isCreating}
          >
            Cancel
          </button>
        </div>
      </div>
    </div>
  );
};

export default CreateNodeModal;
