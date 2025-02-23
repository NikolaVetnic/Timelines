import React, { useEffect, useState } from "react";

import EditButton from "../../../../core/components/buttons/EditButton/EditButton";
import InputStringModal from "../../../../core/components/modals/InputStringModal/InputStringModal";
import convertStringToColor from "../../../../core/utils/ConvertStringToColor";

import "./Categories.css";

const LOCAL_STORAGE_KEY = "timelineData";

const Categories = ({ timelineId, nodeId, setModalActive }) => {
    const [isModalOpen, setModalOpen] = useState(false);
    const [localCategories, setLocalCategories] = useState([]);

    useEffect(() => {
        try {
            const storedData = JSON.parse(localStorage.getItem(LOCAL_STORAGE_KEY)) || [];
            const timeline = storedData.find(t => t.id === timelineId);
            const node = timeline?.nodes.find(n => n.id === nodeId);
            if (node?.categories) {
                setLocalCategories(node.categories);
            }
        } catch (error) {
            console.error("Error loading categories:", error);
        }
    }, [timelineId, nodeId]);

    const setModalState = (isActive) => {
        setModalOpen(isActive);
        setModalActive(isActive);
    };

    // todo: connect to backend
    const updateLocalStorage = (newCategories) => {
        try {
            const storedData = JSON.parse(localStorage.getItem(LOCAL_STORAGE_KEY)) || [];
            const timelineIndex = storedData.findIndex(t => t.id === timelineId);

            if (timelineIndex !== -1) {
                const nodeIndex = storedData[timelineIndex].nodes.findIndex(n => n.id === nodeId);
                if (nodeIndex !== -1) {
                    storedData[timelineIndex].nodes[nodeIndex].categories = newCategories;
                    localStorage.setItem(LOCAL_STORAGE_KEY, JSON.stringify(storedData));
                }
            }
        } catch (error) {
            console.error("Error saving categories:", error);
        }
    };

    const handleSaveCategories = (newCategories) => {
        setLocalCategories(newCategories);
        updateLocalStorage(newCategories);
        setModalActive(false);
    };

    return (
        <div className="categories-container">
            <div>
                <strong>Categories:</strong>{" "}
                {localCategories.length > 0 ? (
                    localCategories.map((category, index) => (
                        <span
                            key={index}
                            className="category-badge"
                            style={{ backgroundColor: convertStringToColor(category) }}
                        >
                            {category}
                        </span>
                    ))
                ) : (
                    <span>No Categories Set</span>
                )}
            </div>
            <EditButton onClick={() => setModalState(true)} />
            <InputStringModal
                isOpen={isModalOpen}
                onClose={() => setModalState(false)}
                onSave={handleSaveCategories}
                initialValue={localCategories.join(", ")}
                title="Edit Categories"
            />
        </div>
    );
};

export default Categories;
