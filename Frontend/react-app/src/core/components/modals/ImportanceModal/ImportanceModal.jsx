import React, { useEffect, useState } from "react";
import "./ImportanceModal.css";

import CancelButton from "../../buttons/CancelButton/CancelButton";
import SaveButton from "../../buttons/SaveButton/SaveButton";

const ImportanceModal = ({ isOpen, onClose, onSave, initialValue }) => {
    const [value, setValue] = useState(initialValue);

    useEffect(() => {
        setValue(initialValue);
    }, [initialValue]);

    const handleIncrement = () => {
        setValue((prevValue) => prevValue + 1);
    };

    const handleDecrement = () => {
        setValue((prevValue) => (prevValue > 0 ? prevValue - 1 : 0));
    };

    const handleSave = () => {
        onSave(value);
        onClose();
    };

    if (!isOpen) return null;

    return (
        <div className="importance-modal-overlay">
            <div className="importance-modal">
                <h3>Edit Importance</h3>
                <div className="importance-controls">
                    <button className="importance-button" onClick={handleDecrement}>
                        -
                    </button>
                    <span className="importance-value">{value}</span>
                    <button className="importance-button" onClick={handleIncrement}>
                        +
                    </button>
                </div>
                <div className="importance-modal-actions">
                    <CancelButton onClick={onClose} />
                    <SaveButton onClick={handleSave} />
                </div>
            </div>
        </div>
    );
};

export default ImportanceModal;
