import React, { useEffect, useState } from "react";
import "./IntegerModal.css";

const IntegerModal = ({
  isOpen,
  onClose,
  onSave,
  initialValue = 0,
  title = "Edit Value",
  minValue = 0,
  maxValue = 100,
}) => {
  const root = "integer-modal";
  const [value, setValue] = useState(initialValue);
  const [isChanged, setIsChanged] = useState(false);
  const [isModalOpen, setModalOpen] = useState(isOpen);

  useEffect(() => {
    setModalOpen(isOpen);
    if (isOpen) {
      setValue(initialValue);
      setIsChanged(false);
    }
  }, [initialValue, isOpen]);

  const handleIncrement = () => {
    setValue((prevValue) => {
      const newValue = prevValue < maxValue ? prevValue + 1 : maxValue;
      setIsChanged(newValue !== initialValue);
      return newValue;
    });
  };

  const handleDecrement = () => {
    setValue((prevValue) => {
      const newValue = prevValue > minValue ? prevValue - 1 : minValue;
      setIsChanged(newValue !== initialValue);
      return newValue;
    });
  };

  const handleSave = () => {
    onSave(value);
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

        <div className={`${root}-controls`}>
          <button
            className={`${root}-decrement-btn`}
            onClick={handleDecrement}
            disabled={value <= minValue}
          >
            -
          </button>
          <span className={`${root}-value`}>{value}</span>
          <button
            className={`${root}-increment-btn`}
            onClick={handleIncrement}
            disabled={value >= maxValue}
          >
            +
          </button>
        </div>

        <div className={`${root}-actions`}>
          <button className={`${root}-cancel-btn`} onClick={closeModal}>
            Cancel
          </button>
          <button
            className={`${root}-save-btn`}
            onClick={handleSave}
            disabled={!isChanged}
          >
            Save
          </button>
        </div>
      </div>
    </div>
  );
};

export default IntegerModal;
