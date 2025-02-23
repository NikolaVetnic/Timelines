import React, { useEffect, useState } from "react";
import EditButton from "../../../../core/components/buttons/EditButton/EditButton";
import TextAreaModal from "../../../../core/components/modals/TextAreaModal/TextAreaModal";

import "./Description.css";

const LOCAL_STORAGE_KEY = "timelineData";

const Description = ({ timelineId, nodeId, setModalActive }) => {
    const [isModalOpen, setModalOpen] = useState(false);
    const [localDescription, setLocalDescription] = useState("");

    useEffect(() => {
        try {
            const storedData = JSON.parse(localStorage.getItem(LOCAL_STORAGE_KEY)) || [];
            const timeline = storedData.find(t => t.id === timelineId);
            const node = timeline?.nodes.find(n => n.id === nodeId);
            if (node?.description) {
                setLocalDescription(node.description);
            }
        } catch (error) {
            console.error("Error loading description:", error);
        }
    }, [timelineId, nodeId]);

    const setModalState = (isActive) => {
        setModalOpen(isActive);
        setModalActive(isActive);
    };

    // todo: connect to backend
    const updateLocalStorage = (newDescription) => {
        try {
            const storedData = JSON.parse(localStorage.getItem(LOCAL_STORAGE_KEY)) || [];
            const timelineIndex = storedData.findIndex(t => t.id === timelineId);

            if (timelineIndex !== -1) {
                const nodeIndex = storedData[timelineIndex].nodes.findIndex(n => n.id === nodeId);
                if (nodeIndex !== -1) {
                    storedData[timelineIndex].nodes[nodeIndex].description = newDescription;
                    localStorage.setItem(LOCAL_STORAGE_KEY, JSON.stringify(storedData));
                }
            }
        } catch (error) {
            console.error("Error saving description:", error);
        }
    };

    const handleSaveDescription = (newDescription) => {
        setLocalDescription(newDescription);
        updateLocalStorage(newDescription);
        setModalActive(false);
    };

    return (
        <div className="description-container">
            <div>
                <strong>Description:</strong> {localDescription || "No Description Set"}
            </div>
            <EditButton onClick={() => setModalState(true)} />
            <TextAreaModal
                isOpen={isModalOpen}
                onClose={() => setModalState(false)}
                onSave={handleSaveDescription}
                initialValue={localDescription}
                title="Edit Description"
            />
        </div>
    );
};

export default Description;
