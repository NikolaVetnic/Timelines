import React, { useEffect, useState } from "react";

import { LOCAL_STORAGE_KEY } from "../../../../data/constants";
import EditButton from "../../../../core/components/buttons/EditButton/EditButton";
import DatePickerModal from "../../../../core/components/modals/DatePickerModal/DatePickerModal";

import "./Timestamp.css";

const Timestamp = ({ timelineId, nodeId, setModalActive }) => {
    const root = "timestamp";
    const [isModalOpen, setModalOpen] = useState(false);
    const [localTimestamp, setLocalTimestamp] = useState(null);

    // todo: connect to backend
    useEffect(() => {
        try {
            const storedData = JSON.parse(localStorage.getItem(LOCAL_STORAGE_KEY)) || [];
            const timeline = storedData.find(t => t.id === timelineId);
            const node = timeline?.nodes.find(n => n.id === nodeId);
            if (node?.timestamp) {
                setLocalTimestamp(new Date(node.timestamp));
            }
        } catch (error) {
            console.error("Error loading timestamp:", error);
        }
    }, [timelineId, nodeId]);

    const setModalState = (isActive) => {
        setModalOpen(isActive);
        setModalActive(isActive);
    };

    // todo: connect to backend
    const updateLocalStorage = (newTimestamp) => {
        try {
            const storedData = JSON.parse(localStorage.getItem(LOCAL_STORAGE_KEY)) || [];
            const timelineIndex = storedData.findIndex(t => t.id === timelineId);
            
            if (timelineIndex !== -1) {
                const nodeIndex = storedData[timelineIndex].nodes.findIndex(n => n.id === nodeId);
                if (nodeIndex !== -1) {
                    storedData[timelineIndex].nodes[nodeIndex].timestamp = newTimestamp.toISOString();
                    localStorage.setItem(LOCAL_STORAGE_KEY, JSON.stringify(storedData));
                }
            }
        } catch (error) {
            console.error("Error saving timestamp:", error);
        }
    };

    const handleSaveTimestamp = (newTimestamp) => {
        setLocalTimestamp(newTimestamp);
        updateLocalStorage(newTimestamp);
        setModalActive(false);
    };

    return (
        <div className={`${root}-container`}>
            <div>
                <strong>Timestamp:</strong> {localTimestamp ? localTimestamp.toLocaleDateString() : "Not Set"}
            </div>
            <EditButton onClick={() => setModalState(true)} />
            <DatePickerModal
                isOpen={isModalOpen}
                onClose={() => setModalState(false)}
                onSave={handleSaveTimestamp}
                initialValue={localTimestamp || new Date()}
                title="Edit Timestamp"
            />
        </div>
    );
};

export default Timestamp;
