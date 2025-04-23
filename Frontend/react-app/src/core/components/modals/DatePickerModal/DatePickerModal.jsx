import React, { useEffect, useState } from "react";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import "./DatePickerModal.css";
import Button from "../../buttons/Button/Button";

const DatePickerModal = ({
  isOpen,
  onClose,
  onSave,
  initialValue = new Date(),
  title = "Select Date",
  minDate,
  maxDate,
}) => {
  const root = "datepicker-modal";
  const [selectedDate, setSelectedDate] = useState(new Date(initialValue));
  const [isChanged, setIsChanged] = useState(false);
  const [isModalOpen, setModalOpen] = useState(isOpen);

  useEffect(() => {
    setModalOpen(isOpen);
    if (isOpen) {
      setSelectedDate(new Date(initialValue));
      setIsChanged(false);
    }
  }, [isOpen, initialValue]);

  const handleDateChange = (date) => {
    setSelectedDate(date);
    setIsChanged(
      date.toISOString().slice(0, 10) !==
        new Date(initialValue).toISOString().slice(0, 10)
    );
  };

  const handleSave = () => {
    onSave(selectedDate);
    onClose();
  };

  const closeModal = () => {
    setModalOpen(false);
    onClose();
  };

  if (!isModalOpen) return null;

  return (
    <div className={`${root}-overlay`} onClick={closeModal}>
      <div className={`${root}-content`} onClick={(e) => e.stopPropagation()}>
        <div className={`${root}-header`}>
          <h3>{title}</h3>
        </div>

        <div className={`${root}-picker-container`}>
          <DatePicker
            selected={selectedDate}
            onChange={handleDateChange}
            minDate={minDate}
            maxDate={maxDate}
            inline
            highlightDates={Date.now()}
            calendarClassName={`${root}-calendar`}
            dayClassName={() => `${root}-day`}
          />
        </div>

        <div className={`${root}-actions`}>
          <Button text="Cancel" onClick={closeModal} size="small" />
          <Button
            text="Save"
            size="small"
            variant="success"
            onClick={handleSave}
            disabled={!isChanged}
          />
        </div>
      </div>
    </div>
  );
};

export default DatePickerModal;
