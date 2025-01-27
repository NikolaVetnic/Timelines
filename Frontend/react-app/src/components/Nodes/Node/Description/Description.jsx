import React, { useState } from "react";
import EditButton from "../../../../core/components/buttons/EditButton/EditButton";
import TextAreaModal from "../../../../core/components/modals/TextAreaModal/TextAreaModal";

import "./Description.css";

const Description = ({ description, onUpdateDescription, setModalActive }) => {
    const [isModalOpen, setModalOpen] = useState(false);
    const [localDescription, setLocalDescription] = useState(description);

    const setModalState = (isActive) => {
        setModalOpen(isActive);
        setModalActive(isActive);
    };

    const handleSaveDescription = (newDescription) => {
        setLocalDescription(newDescription);
        onUpdateDescription(newDescription);
        setModalActive(false);
    };

    return (
        <div className="description-container">
            <div>
                <strong>Description:</strong> {localDescription}
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
