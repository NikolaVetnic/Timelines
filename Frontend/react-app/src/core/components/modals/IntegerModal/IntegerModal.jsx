import React, { useEffect, useState } from "react";
import "./IntegerModal.css";

import CancelButton from "../../buttons/CancelButton/CancelButton";
import SaveButton from "../../buttons/SaveButton/SaveButton";

const IntegerModal = ({ isOpen, onClose, onSave, initialValue }) => {
    const [value, setValue] = useState(initialValue);
    const [isChanged, setIsChanged] = useState(false);

    useEffect(() => {
        setValue(initialValue);
        setIsChanged(false);
    }, [initialValue]);

    const handleIncrement = () => {
        setValue((prevValue) => {
            const newValue = prevValue + 1;
            setIsChanged(newValue !== initialValue);
            return newValue;
        });
    };

    const handleDecrement = () => {
        setValue((prevValue) => {
            const newValue = prevValue > 0 ? prevValue - 1 : 0;
            setIsChanged(newValue !== initialValue);
            return newValue;
        });
    };

    const handleSave = () => {
        onSave(value);
        onClose();
    };

    if (!isOpen) return null;

    return (
        <div className="integer-modal-overlay">
            <div className="integer-modal">
                <h3>Edit Importance</h3>
                <div className="integer-controls">
                    <button className="integer-button" onClick={handleDecrement}>
                        -
                    </button>
                    <span className="integer-value">{value}</span>
                    <button className="importance-button" onClick={handleIncrement}>
                        +
                    </button>
                </div>
                <div className="integer-modal-actions">
                    <CancelButton onClick={onClose} />
                    <SaveButton onClick={handleSave} disabled={!isChanged} />
                </div>
            </div>
        </div>
    );
};

export default IntegerModal;
