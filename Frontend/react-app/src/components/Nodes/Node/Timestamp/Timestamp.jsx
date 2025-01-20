import React, { useState } from "react";
import EditButton from "../../../../core/components/buttons/EditButton/EditButton";
import DatePickerModal from "../../../../core/components/modals/DatePickerModal/DatePickerModal";

import "./Timestamp.css";

const Timestamp = ({ initialValue, onUpdateTimestamp }) => {
    const [isModalOpen, setModalOpen] = useState(false);
    const [localTimestamp, setLocalTimestamp] = useState(new Date(initialValue));

    const handleOpenModal = () => {
        setModalOpen(true);
    };

    const handleCloseModal = () => {
        setModalOpen(false);
    };

    const handleSaveTimestamp = (newTimestamp) => {
        setLocalTimestamp(newTimestamp);
        onUpdateTimestamp(newTimestamp);
        handleCloseModal();
    };

    return (
        <div className="timestamp-container">
            <div>
                <strong>Timestamp:</strong> {localTimestamp.toLocaleDateString()}
            </div>
            <EditButton onClick={handleOpenModal} />
            <DatePickerModal
                isOpen={isModalOpen}
                onClose={handleCloseModal}
                onSave={handleSaveTimestamp}
                initialValue={localTimestamp}
                title="Edit Timestamp"
            />
        </div>
    );
};

export default Timestamp;
