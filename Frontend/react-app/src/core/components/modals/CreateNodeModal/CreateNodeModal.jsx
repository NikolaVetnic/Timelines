import React, { useEffect, useState } from "react";

import FormField from "../../forms/FormField/FormField";

import "./CreateNodeModal.css";

const CreateNodeModal = ({ isOpen, onClose, selectedTimeline, setTimelineData, timelineData, updateSelectedTimeline }) => {
    const root = "create-node-modal";
    const [isModalOpen, setModalOpen] = useState(isOpen);
    const [nodeData, setNodeData] = useState({
        id: Date.now().toString(),
        title: "",
        description: "",
        phase: "",
        timestamp: new Date().toISOString().slice(0, 16),
        importance: 1,
        files: [],
        categories: "",
        tags: "",
    });

    useEffect(() => {
        setModalOpen(isOpen);
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
    };

    const formatTags = (tags) => tags.split(",").map(tag => tag.trim().toLowerCase().replace(/\s+/g, "-")).filter(tag => tag.length > 0);
    const formatCategories = (categories) => categories.split(",").map(category => category.trim().toLowerCase().replace(/\b\w/g, char => char.toUpperCase())).filter(category => category.length > 0);

    const handleSave = () => {
        if (!selectedTimeline) return;

        const formattedNodeData = {
            ...nodeData,
            tags: formatTags(nodeData.tags),
            categories: formatCategories(nodeData.categories),
        };

        const updatedTimelines = timelineData.map((timeline) => (
            timeline.id === selectedTimeline.id ? { ...timeline, nodes: [...timeline.nodes, formattedNodeData] } : timeline
        ));

        localStorage.setItem("timelineData", JSON.stringify(updatedTimelines));
        setTimelineData(updatedTimelines);
        updateSelectedTimeline();
        closeModal();
    };

    if (!isModalOpen) return null;

    return (
        <div className="create-node-modal-overlay">
            <div className="create-node-modal-content">
                <div className="create-node-modal-header">
                    <h3>Add New Node</h3>
                </div>

                <FormField label="Title" type="text" name="title" value={nodeData.title} onChange={handleChange} />
                <FormField label="Description" type="textarea" name="description" value={nodeData.description} onChange={handleChange} />
                <FormField label="Phase" type="text" name="phase" value={nodeData.phase} onChange={handleChange} />
                <FormField label="Timestamp" type="datetime-local" name="timestamp" value={nodeData.timestamp} onChange={handleChange} />
                <FormField label="Importance" type="number" name="importance" value={nodeData.importance} onChange={handleChange} min="1" max="10" />
                <FormField label="Tags" type="text" name="tags" value={nodeData.tags} onChange={handleChange} placeholder="tag1, tag2..." />
                <FormField label="Categories" type="text" name="categories" value={nodeData.categories} onChange={handleChange} placeholder="Category1, Category2..." />

                <div className={`${root}-actions`}>
                    <button className={`${root}-save-btn`} onClick={handleSave}>Save</button>
                    <button className={`${root}-cancel-btn`} onClick={closeModal}>Cancel</button>
                </div>
            </div>
        </div>
    );
};

export default CreateNodeModal;
