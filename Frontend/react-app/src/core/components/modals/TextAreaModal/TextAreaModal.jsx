import React, { useEffect, useState } from "react";

import TextButton from "../../buttons/TextButton/TextButton";

import "./TextAreaModal.css";

const TextAreaModal = ({ isOpen, onClose, onSave, initialValue, title }) => {
    const root = "text-area-modal";
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
        <div className={`${root}-overlay`}>
            <div className={`${root}`}>
                <h3>{title}</h3>
                <textarea
                    rows="4"
                    value={value}
                    onChange={handleInputChange}
                    placeholder="Input text"
                ></textarea>
                <div className={`${root}-actions`}>
                    <TextButton onClick={onClose} text="Cancel" color="default" />
                    <TextButton onClick={handleSave} text="Save" color="green" disabled={!isChanged}/>
                </div>
            </div>
        </div>
    );
};

export default TextAreaModal;
