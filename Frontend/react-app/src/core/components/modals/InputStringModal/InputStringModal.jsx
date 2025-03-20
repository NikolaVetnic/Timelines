import React, { useEffect, useState } from "react";

import TextButton from "../../buttons/TextButton/TextButton";

import "./InputStringModal.css";

const InputStringModal = ({
    isOpen,
    onClose,
    onSave,
    initialValue = "",
    title = "Input String",
    placeholder = "Input String...",
}) => {
    const root = "input-string-modal";
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
        <div className={`${root}-overlay`} onClick={onClose}>
            <div className={`${root}`} onClick={(e) => e.stopPropagation()}>
                <h3>{title}</h3>
                <textarea
                    rows="4"
                    value={value}
                    onChange={handleInputChange}
                    placeholder={placeholder}
                    autoFocus
                ></textarea>
                <div className={`${root}-actions`}>
                    <TextButton onClick={onClose} text="Cancel" color="default" />
                    <TextButton onClick={handleSave} text="Save" color="green" disabled={!isChanged}/>
                </div>
            </div>  
        </div>
    );
};

export default InputStringModal;
