import React from "react";
import { FaTimes } from "react-icons/fa";
import Button from "../../buttons/Button/Button";
import "./DeleteTimelineModal.css";

const DeleteTimelineModal = ({ isOpen, onClose, onConfirm, count }) => {
  if (!isOpen) return null;

  return (
    <div className="delete-confirmation-modal">
      <div className="delete-confirmation-content">
        <div className="delete-confirmation-buttons">
          <Button
            icon={<FaTimes />}
            iconOnly
            noBackground
            variant="danger"
            onClick={onClose}
          />
        </div>
        <h3>Confirm Deletion</h3>
        <p>
          Are you sure you want to delete {count}
          {count === 1 ? " timeline" : " timelines"}? This action cannot be
          undone.
        </p>
        <div className="confirmation-buttons">
          <Button text="Delete" variant="danger" onClick={onConfirm} />
        </div>
      </div>
    </div>
  );
};

export default DeleteTimelineModal;
