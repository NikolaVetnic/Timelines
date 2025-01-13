import React, { useState } from "react";
import EditButton from "../../core/components/buttons/EditButton/EditButton";
import ImportanceModal from "../../core/components/modals/ImportanceModal/ImportanceModal";

import "./Importance.css";

const Importance = ({ initialValue, onSave }) => {
    const [importance, setImportance] = useState(initialValue);
    const [isModalOpen, setModalOpen] = useState(false);

    const handleOpenModal = () => {
        setModalOpen(true);
    };

    const handleCloseModal = () => {
        setModalOpen(false);
    };

    const handleSaveImportance = (newImportance) => {
        setImportance(newImportance);
        onSave(newImportance);
        handleCloseModal();
    };

    return (
        <div className="importance-container">
            <div className="importance-content">
                <strong>Importance:</strong> {importance}
            </div>
            <EditButton onClick={handleOpenModal} />
            <ImportanceModal
                isOpen={isModalOpen}
                onClose={handleCloseModal}
                onSave={handleSaveImportance}
                initialValue={importance}
            />
        </div>
    );
};

export default Importance;
