import React, { useEffect, useState } from "react";

import TextButton from "../../buttons/TextButton/TextButton";

import "./IntegerModal.css";

const IntegerModal = ({ isOpen, onClose, onSave, initialValue }) => {
    const root = "integer-modal";
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
        <div className={`${root}-overlay`}>
            <div className={`${root}`}>
                <h3>Edit Value</h3>
                <div className={`${root}-controls`}>
                    <button className={`${root}-button`} onClick={handleDecrement}>-</button>
                    <span className={`${root}-value`}>{value}</span>
                    <button className={`${root}-button`} onClick={handleIncrement}>+</button>
                </div>
                <div className={`${root}-actions`}>
                    <TextButton onClick={onClose} text="Cancel" color="default" />
                    <TextButton onClick={handleSave} text="Save" color="green" disabled={!isChanged}/>
                </div>
            </div>
        </div>
    );
};

export default IntegerModal;
