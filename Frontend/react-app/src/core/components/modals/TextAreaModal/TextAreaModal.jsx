import React, { useEffect, useState } from "react";

import CancelButton from "../../buttons/CancelButton/CancelButton";
import SaveButton from "../../buttons/SaveButton/SaveButton";

import "./TextAreaModal.css";

const TextAreaModal = ({ isOpen, onClose, onSave, initialValue, title }) => {
    const [value, setValue] = useState(initialValue);
    const [isChanged, setIsChanged] = useState(false);

    useEffect(() => {
        setValue(initialValue);
        setIsChanged(false);
    }, [initialValue]);

    const handleInputChange = (e) => {
        const newValue = e.target.value;
        setValue(newValue);
        setIsChanged(newValue.trim() !== initialValue.trim());
    };

    const handleSave = () => {
        onSave(value);
        onClose();
    };

    if (!isOpen) return null;

    return (
        <div className="text-area-modal-overlay">
            <div className="text-area-modal">
                <h3>{title}</h3>
                <textarea
                    rows="4"
                    value={value}
                    onChange={handleInputChange}
                    placeholder="Input text"
                ></textarea>
                <div className="text-area-modal-actions">
                    <CancelButton onClick={onClose} />
                    <SaveButton onClick={handleSave} disabled={!isChanged} />
                </div>
            </div>
        </div>
    );
};

export default TextAreaModal;
