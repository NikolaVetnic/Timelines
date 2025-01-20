import React, { useState } from "react";
import EditButton from "../../../../core/components/buttons/EditButton/EditButton";
import DatePickerModal from "../../../../core/components/modals/DatePickerModal/DatePickerModal";

import "./Timestamp.css";

const Timestamp = ({ initialValue, onUpdateTimestamp }) => {
    const [isModalOpen, setModalOpen] = useState(false);
    const [localTimestamp, setLocalTimestamp] = useState(new Date(initialValue));

    const setModalActive = (isActive) => {
        setModalOpen(isActive);
    };

    const handleSaveTimestamp = (newTimestamp) => {
        setLocalTimestamp(newTimestamp);
        onUpdateTimestamp(newTimestamp);
        setModalActive(false);
    };

    return (
        <div className="timestamp-container">
            <div>
                <strong>Timestamp:</strong> {localTimestamp.toLocaleDateString()}
            </div>
            <EditButton onClick={() => setModalActive(true)} />
            <DatePickerModal
                isOpen={isModalOpen}
                onClose={() => setModalActive(false)}
                onSave={handleSaveTimestamp}
                initialValue={localTimestamp}
                title="Edit Timestamp"
            />
        </div>
    );
};

export default Timestamp;
