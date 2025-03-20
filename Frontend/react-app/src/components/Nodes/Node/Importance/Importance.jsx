import React, { useEffect, useState } from "react";
import { CiEdit } from "react-icons/ci";

import IconButton from "../../../../core/components/buttons/IconButton/IconButton";
import IntegerModal from "../../../../core/components/modals/IntegerModal/IntegerModal";
import { LOCAL_STORAGE_KEY } from "../../../../data/constants";

import "./Importance.css";

const Importance = ({ timelineId, nodeId, setModalActive }) => {
    const [importance, setImportance] = useState(0);
    const [isModalOpen, setModalOpen] = useState(false);

    // todo: connect to backend
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

    // todo: connect to backend
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
            <IconButton onClick={() => setModalState(true)} icon={<CiEdit />} title="Edit" />
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
