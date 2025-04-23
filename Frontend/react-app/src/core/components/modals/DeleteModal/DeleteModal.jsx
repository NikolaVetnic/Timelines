import React from "react";
import Button from "../../buttons/Button/Button";
import "./DeleteModal.css";

const deleteMessageOne = "Are you sure you want to delete";
const deleteMessageTwo = "This action cannot be undone.";

const DeleteModal = ({
  isOpen,
  onClose,
  onConfirm,
  itemType = "note",
  itemTitle,
  count = 1,
}) => {
  if (!isOpen) return null;

  // todo: find better solution
  const getMessage = () => {
    if (itemType === "timeline") {
      return `${deleteMessageOne} ${count} ${
        count === 1 ? "timeline" : "timelines"
      }? ${deleteMessageTwo}`;
    } else if (itemType === "node") {
      return `${deleteMessageOne} ${count} ${
        count === 1 ? "node" : "nodes"
      }? ${deleteMessageTwo}`;
    } else if (itemType === "reminder") {
      return `${deleteMessageOne} ${count} ${
        count === 1 ? "reminder" : "reminders"
      }? ${deleteMessageTwo}`;
    }
    return `${deleteMessageOne} the ${itemType} "${itemTitle}"? ${deleteMessageTwo}`;
  };

  const getTitle = () => {
    if (count > 1) {
      return `Delete ${itemType.charAt(0).toUpperCase() + itemType.slice(1)}s`;
    }
    return `Delete ${itemType.charAt(0).toUpperCase() + itemType.slice(1)}`;
  };

  return (
    <div className="delete-modal-overlay">
      <div className="delete-modal-content">
        <h3 className="delete-modal-header">{getTitle()}</h3>
        <p className="delete-modal-message">{getMessage()}</p>

        <div className="delete-modal-actions">
          <Button text="Cancel" variant="secondary" onClick={onClose} />
          <Button text="Delete" variant="danger" onClick={onConfirm} />
        </div>
      </div>
    </div>
  );
};

export default DeleteModal;
