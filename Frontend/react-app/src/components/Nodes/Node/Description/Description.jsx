import React, { useState } from "react";
import EditButton from "../../../../core/components/buttons/EditButton/EditButton";
import TextAreaModal from "../../../../core/components/modals/TextAreaModal/TextAreaModal";

import "./Description.css";

const Description = ({ description, onUpdateDescription }) => {
    const [isModalOpen, setModalOpen] = useState(false);
    const [localDescription, setLocalDescription] = useState(description);

    const setModalActive = (isActive) => {
        setModalOpen(isActive);
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
            <EditButton onClick={() => setModalActive(true)} />
            <TextAreaModal
                isOpen={isModalOpen}
                onClose={() => setModalActive(false)}
                onSave={handleSaveDescription}
                initialValue={localDescription}
                title="Edit Description"
            />
        </div>
    );
};

export default Description;
