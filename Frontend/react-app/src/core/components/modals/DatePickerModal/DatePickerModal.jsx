import React, { useEffect, useState } from "react";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";

import CancelButton from "../../buttons/CancelButton/CancelButton";
import SaveButton from "../../buttons/SaveButton/SaveButton";

import "./DatePickerModal.css";

const DatePickerModal = ({ isOpen, onClose, onSave, initialValue, title }) => {
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
        <div className={`datepicker-modal-overlay ${isOpen ? "show" : ""}`}>
            <div className="datepicker-modal">
                <h3>{title}</h3>
                <DatePicker
                    selected={selectedDate}
                    onChange={handleDateChange}
                    dateFormat="yyyy-MM-dd"
                    inline
                />
                <div className="datepicker-modal-actions">
                    <CancelButton onClick={onClose} />
                    <SaveButton onClick={handleSave} disabled={!isChanged} />
                </div>
            </div>
        </div>
    );
};

export default DatePickerModal;
