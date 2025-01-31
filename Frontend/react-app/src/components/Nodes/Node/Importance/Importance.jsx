import React, { useEffect, useState } from "react";
import EditButton from "../../../../core/components/buttons/EditButton/EditButton";
import IntegerModal from "../../../../core/components/modals/IntegerModal/IntegerModal";

import "./Importance.css";

const LOCAL_STORAGE_KEY = "timelineData";

const Importance = ({ timelineId, nodeId, setModalActive }) => {
    const [importance, setImportance] = useState(0);
    const [isModalOpen, setModalOpen] = useState(false);

    useEffect(() => {
        try {
            const storedData = JSON.parse(localStorage.getItem(LOCAL_STORAGE_KEY)) || [];
            const timeline = storedData.find(t => t.id === timelineId);
            const node = timeline?.nodes.find(n => n.id === nodeId);
            if (node?.importance !== undefined) {
                setImportance(node.importance);
            }
        } catch (error) {
            console.error("Error loading importance:", error);
        }
    }, [timelineId, nodeId]);

    const setModalState = (isActive) => {
        setModalOpen(isActive);
        setModalActive(isActive);
    };

    const updateLocalStorage = (newImportance) => {
        try {
            const storedData = JSON.parse(localStorage.getItem(LOCAL_STORAGE_KEY)) || [];
            const timelineIndex = storedData.findIndex(t => t.id === timelineId);

            if (timelineIndex !== -1) {
                const nodeIndex = storedData[timelineIndex].nodes.findIndex(n => n.id === nodeId);
                if (nodeIndex !== -1) {
                    storedData[timelineIndex].nodes[nodeIndex].importance = newImportance;
                    localStorage.setItem(LOCAL_STORAGE_KEY, JSON.stringify(storedData));
                }
            }
        } catch (error) {
            console.error("Error saving importance:", error);
        }
    };

    const handleSaveImportance = (newImportance) => {
        setImportance(newImportance);
        updateLocalStorage(newImportance);
        setModalState(false);
    };

    return (
        <div className="importance-container">
            <div className="importance-content">
                <strong>Importance:</strong> {importance}
            </div>
            <EditButton onClick={() => setModalState(true)} />
            <IntegerModal
                isOpen={isModalOpen}
                onClose={() => setModalState(false)}
                onSave={handleSaveImportance}
                initialValue={importance}
            />
        </div>
    );
};

export default Importance;
