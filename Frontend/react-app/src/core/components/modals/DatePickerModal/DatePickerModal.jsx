import React, { useEffect, useState } from "react";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";

import TextButton from "../../buttons/TextButton/TextButton";

import "./DatePickerModal.css";

const DatePickerModal = ({ isOpen, onClose, onSave, initialValue, title }) => {
    const root = "datepicker-modal";
    const [selectedDate, setSelectedDate] = useState(new Date(initialValue));
    const [isChanged, setIsChanged] = useState(false);

    useEffect(() => {
        if (isOpen) {
            setSelectedDate(new Date(initialValue));
            setIsChanged(false);
        }
    }, [isOpen, initialValue]);

    const handleDateChange = (date) => {
        setSelectedDate(date);
        setIsChanged(
            date.toISOString().slice(0, 10) !== new Date(initialValue).toISOString().slice(0, 10)
        );
    };

    const handleSave = () => {
        onSave(selectedDate);
        onClose();
    };

    if (!isOpen) return null;

    return (
        <div className={`${root}-overlay ${isOpen ? "show" : ""}`}>
            <div className={`${root}`}>
                <h3>{title}</h3>
                <div>
                    <DatePicker
                        selected={selectedDate}
                        onChange={handleDateChange}
                        dateFormat="yyyy-MM-dd"
                        inline
                    />
                </div>
                <div className={`${root}-actions`}>
                    <TextButton onClick={onClose} text="Cancel" color="default" />
                    <TextButton onClick={handleSave} text="Save" color="green" disabled={!isChanged}/>
                </div>
            </div>
        </div>
    );
};

export default DatePickerModal;
