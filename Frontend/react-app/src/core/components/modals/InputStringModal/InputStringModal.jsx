import React, { useEffect, useState } from "react";
import FormField from "../../forms/FormField/FormField";
import "./InputStringModal.css";
import Button from "../../buttons/Button/Button";

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
  const [isModalOpen, setModalOpen] = useState(isOpen);

  useEffect(() => {
    setModalOpen(isOpen);
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
          <Button text="Cancel" onClick={closeModal} />
          <Button
            text="Save"
            variant="success"
            onClick={handleSave}
            disabled={!isChanged}
          />
        </div>
      </div>
    </div>
  );
};

export default InputStringModal;
