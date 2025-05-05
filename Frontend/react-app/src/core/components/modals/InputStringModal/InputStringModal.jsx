import React, { useEffect, useState } from "react";
import Button from "../../buttons/Button/Button";
import FormField from "../../forms/FormField/FormField";
import "./InputStringModal.css";

const InputStringModal = ({
  isOpen,
  onClose,
  onSave,
  initialValue = "",
  title = "Input String",
  placeholder = "Input String...",
  dataType = "string",
}) => {
  const root = "input-string-modal";
  const [value, setValue] = useState(
    dataType === "array" && Array.isArray(initialValue) 
      ? initialValue.join(", ") 
      : initialValue
  );
  const [isChanged, setIsChanged] = useState(false);
  const [isModalOpen, setModalOpen] = useState(isOpen);

  useEffect(() => {
    setModalOpen(isOpen);
    if (isOpen) {
      setValue(
        dataType === "array" && Array.isArray(initialValue)
          ? initialValue.join(", ")
          : initialValue
      );
      setIsChanged(false);
    }
  }, [initialValue, isOpen, dataType]);

  const handleInputChange = (e) => {
    const newValue = e.target.value;
    setValue(newValue);
    if (dataType === "array") {
      const currentArray = initialValue || [];
      const newArray = newValue.split(",").map(item => item.trim());
      setIsChanged(JSON.stringify(newArray) !== JSON.stringify(currentArray));
    } else {
      setIsChanged(newValue.trim() !== (initialValue || "").trim());
    }
  };

  const handleSave = () => {
    if (dataType === "array") {
      onSave(value.split(",").map(item => item.trim()).filter(item => item));
    } else {
      onSave(value.trim());
    }
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

        <FormField
          type="textarea"
          name="inputString"
          value={value}
          onChange={handleInputChange}
          placeholder={placeholder}
          autoFocus
          rows={4}
        />

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

export default InputStringModal;
