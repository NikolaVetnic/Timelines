import React, { useState } from "react";
import EditButton from "../../../../core/components/buttons/EditButton/EditButton";
import IntegerModal from "../../../../core/components/modals/IntegerModal/IntegerModal";

import "./Importance.css";

const Importance = ({ initialValue, onSave, setModalActive }) => {
    const [importance, setImportance] = useState(initialValue);
    const [isModalOpen, setModalOpen] = useState(false);

    const setModalState = (isActive) => {
        setModalOpen(isActive);
        setModalActive(isActive);
    };

    const handleSaveImportance = (newImportance) => {
        setImportance(newImportance);
        onSave(newImportance);
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
