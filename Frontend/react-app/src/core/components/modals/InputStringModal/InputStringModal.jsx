import React, { useEffect, useState } from "react";
import "./InputStringModal.css";

import CancelButton from "../../buttons/CancelButton/CancelButton";
import SaveButton from "../../buttons/SaveButton/SaveButton";

const InputStringModal = ({ 
    isOpen, 
    onClose, 
    onSave, 
    initialValue = "", 
    title = "Edit Values", 
    placeholder = "Enter values here..." 
}) => {
    const [value, setValue] = useState(initialValue);
    const [isChanged, setIsChanged] = useState(false);

    useEffect(() => {
        if (isOpen) {
            setValue(initialValue);
            setIsChanged(false);
        }
    }, [initialValue, isOpen]);

    const handleInputChange = (e) => {
        const newValue = e.target.value;
        setValue(newValue);
        setIsChanged(newValue.trim() !== initialValue.trim());
    };

    const handleSave = () => {
        onSave(value.split(",").map((item) => item.trim())); 
        onClose();
    };

    if (!isOpen) return null;

    return (
        <div className="modal-overlay" onClick={onClose}>
            <div className="modal" onClick={(e) => e.stopPropagation()}>
                <h3>{title}</h3>
                <textarea
                    rows="4"
                    value={value}
                    onChange={handleInputChange}
                    placeholder={placeholder}
                    autoFocus
                ></textarea>
                <div className="modal-actions">
                    <CancelButton onClick={onClose} />
                    <SaveButton onClick={handleSave} disabled={!isChanged} />
                </div>
            </div>
        </div>
    );
};

export default InputStringModal;
