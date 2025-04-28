import React, { useEffect, useState } from "react";
import Button from "../../buttons/Button/Button";
import { FaMinus, FaPlus } from "react-icons/fa";
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
          <Button
            icon={<FaMinus />}
            iconOnly
            variant="danger"
            shape="circle"
            onClick={handleDecrement}
            disabled={value <= minValue}
          />
          <span className={`${root}-value`}>{value}</span>
          <Button
            icon={<FaPlus />}
            iconOnly
            variant="success"
            shape="circle"
            onClick={handleIncrement}
            disabled={value >= maxValue}
          />
        </div>

        <div className={`${root}-actions`}>
          <Button text="Cancel" onClick={closeModal} size="small" />
          <Button
            text="Save"
            variant="success"
            size="small"
            onClick={handleSave}
            disabled={!isChanged}
          />
        </div>
      </div>
    </div>
  );
};

export default IntegerModal;
