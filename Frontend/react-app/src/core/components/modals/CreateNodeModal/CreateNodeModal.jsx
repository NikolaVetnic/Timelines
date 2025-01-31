import React, { useEffect, useState } from "react";
import InputStringModal from "../InputStringModal/InputStringModal";
import "./CreateNodeModal.css";

const CreateNodeModal = ({ isOpen, onClose, selectedTimeline, setTimelineData, timelineData, updateSelectedTimeline }) => {
    const [isModalOpen, setModalOpen] = useState(isOpen);
    const [nodeData, setNodeData] = useState({
        id: Date.now().toString(),
        title: "",
        description: "",
        phase: "",
        timestamp: new Date().toISOString().slice(0, 16),
        importance: 1,
        files: [],
        categories: [],
        tags: [],
    });

    const [isTagsModalOpen, setIsTagsModalOpen] = useState(false);
    const [isCategoriesModalOpen, setIsCategoriesModalOpen] = useState(false);

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

    const handleSaveTags = (tags) => {
        const formattedTags = formatTags(tags);
        setNodeData((prevData) => ({ ...prevData, tags: formattedTags }));
        setIsTagsModalOpen(false);
    };
    
    const handleSaveCategories = (categories) => {
        const formattedCategories = formatCategories(categories);
        setNodeData((prevData) => ({ ...prevData, categories: formattedCategories }));
        setIsCategoriesModalOpen(false);
    };
    
    const formatTags = (tags) => {
        return tags
            .map(tag => tag.trim().toLowerCase().replace(/\s+/g, "-"))
            .filter(tag => tag.length > 0);
    };

    const formatCategories = (categories) => {
        return categories
            .map(category => 
                category
                    .trim()
                    .toLowerCase()
                    .replace(/\b\w/g, char => char.toUpperCase())
            )
            .filter(category => category.length > 0);
    };


    const handleSave = () => {
        if (!selectedTimeline) return;

        const updatedTimelines = timelineData.map((timeline) => {
            if (timeline.id === selectedTimeline.id) {
                return {
                    ...timeline,
                    nodes: [...timeline.nodes, nodeData],
                };
            }
            return timeline;
        });

        localStorage.setItem("timelineData", JSON.stringify(updatedTimelines));

        setTimelineData(updatedTimelines);

        updateSelectedTimeline();

        closeModal();
    };

    if (!isModalOpen) return null;

    return (
        <div className="create-node-modal-overlay" onClick={closeModal}>
            <div className="create-node-modal-content" onClick={(e) => e.stopPropagation()}>
                <div className="create-node-modal-header">
                    <h3>Add New Node</h3>
                </div>

                <label>Title:</label>
                <input type="text" name="title" value={nodeData.title} onChange={handleChange} />

                <label>Description:</label>
                <textarea name="description" value={nodeData.description} onChange={handleChange} />

                <label>Phase:</label>
                <input type="text" name="phase" value={nodeData.phase} onChange={handleChange} />

                <label>Timestamp:</label>
                <input type="datetime-local" name="timestamp" value={nodeData.timestamp} onChange={handleChange} />

                <label>Importance (1-5):</label>
                <input type="number" name="importance" min="1" max="5" value={nodeData.importance} onChange={handleChange} />

                <label>Tags:</label>
                <div className="multi-input-container">
                    <input type="text" value={nodeData.tags.join(", ")} readOnly />
                    <button type="button" onClick={() => setIsTagsModalOpen(true)}>Add Tags</button>
                </div>

                <label>Categories:</label>
                <div className="multi-input-container">
                    <input type="text" value={nodeData.categories.join(", ")} readOnly />
                    <button type="button" onClick={() => setIsCategoriesModalOpen(true)}>Add Categories</button>
                </div>

                <div className="create-node-modal-actions">
                    <button className="create-node-modal-save-btn" onClick={handleSave}>Save</button>
                    <button className="create-node-modal-cancel-btn" onClick={closeModal}>Cancel</button>
                </div>
            </div>

            <InputStringModal
                isOpen={isTagsModalOpen}
                onClose={() => setIsTagsModalOpen(false)}
                onSave={handleSaveTags}
                initialValue={nodeData.tags.join(", ")}
                title="Add Tags"
                placeholder="Enter tags separated by commas..."
            />

            <InputStringModal
                isOpen={isCategoriesModalOpen}
                onClose={() => setIsCategoriesModalOpen(false)}
                onSave={handleSaveCategories}
                initialValue={nodeData.categories.join(", ")}
                title="Add Categories"
                placeholder="Enter categories separated by commas..."
            />
        </div>
    );
};

export default CreateNodeModal;
