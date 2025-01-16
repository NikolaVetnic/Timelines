import React, { useState } from "react";
import EditButton from "../../core/components/buttons/EditButton/EditButton";
import DescriptionModal from "../../core/components/modals/DescriptionModal/DescriptionModal";

import "./Description.css";

const Description = ({ description, onUpdateDescription }) => {
    const [isModalOpen, setModalOpen] = useState(false);
    const [localDescription, setLocalDescription] = useState(description);

    const handleOpenModal = () => {
        setModalOpen(true);
    };

    const handleCloseModal = () => {
        setModalOpen(false);
    };

    const handleSaveDescription = (newDescription) => {
        setLocalDescription(newDescription);
        onUpdateDescription(newDescription);
        handleCloseModal();
    };

    return (
        <div className="description-container">
            <div>
                <strong>Description:</strong> {localDescription}
            </div>
            <EditButton onClick={handleOpenModal} />
            <DescriptionModal
                isOpen={isModalOpen}
                onClose={handleCloseModal}
                onSave={handleSaveDescription}
                initialValue={localDescription}
                title="Edit Description"
            />
        </div>
    );
};

export default Description;
