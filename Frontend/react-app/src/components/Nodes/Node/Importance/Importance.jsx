import React, { useState } from "react";

import EditButton from "../../../../core/components/buttons/EditButton/EditButton";
import IntegerModal from "../../../../core/components/modals/IntegerModal/IntegerModal";

import "./Importance.css";

const Importance = ({ initialValue, onSave }) => {
    const [importance, setImportance] = useState(initialValue);
    const [isModalOpen, setModalOpen] = useState(false);

    const setModalActive = (isActive) => {
        setModalOpen(isActive);
    };

    const handleSaveImportance = (newImportance) => {
        setImportance(newImportance);
        onSave(newImportance);
        setModalActive(false);
    };

    return (
        <div className="importance-container">
            <div className="importance-content">
                <strong>Importance:</strong> {importance}
            </div>
            <EditButton onClick={() => setModalActive(true)} />
            <IntegerModal
                isOpen={isModalOpen}
                onClose={() => setModalActive(false)}
                onSave={handleSaveImportance}
                initialValue={importance}
            />
        </div>
    );
};

export default Importance;
