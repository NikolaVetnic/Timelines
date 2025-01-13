import React, { useEffect, useState } from "react";

import "./InputCSVModal.css";

import CancelButton from "../../buttons/CancelButton/CancelButton";
import SaveButton from "../../buttons/SaveButton/SaveButton";

const InputCSVModal = ({ isOpen, onClose, onSave, initialValue, title }) => {
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
        onSave(value.split(",").map((item) => item.trim()));
        onClose();
    };

    if (!isOpen) return null;

    return (
        <div className="modal-overlay">
            <div className="modal">
                <h3>{title}</h3>
                <textarea
                    rows="4"
                    value={value}
                    onChange={handleInputChange}
                    placeholder="Enter values as a comma-separated list"
                ></textarea>
                <div className="modal-actions">
                    <CancelButton onClick={onClose} />
                    <SaveButton onClick={handleSave} disabled={!isChanged} />
                </div>
            </div>
        </div>
    );
};

export default InputCSVModal;
