import React from 'react';
import { FaTimes } from 'react-icons/fa';
import Button from '../../buttons/Button/Button';
import './DeleteNoteModal.css';

const DeleteNoteModal = ({ isOpen, closeModal, handleConfirm, noteTitle }) => {
  if (!isOpen) return null;

  return (
    <div className="delete-confirmation-modal">
      <div className="delete-confirmation-content">
        <Button
            icon={<FaTimes />}
            iconOnly
            noBackground
            variant="danger"
            onClick={closeModal}
        />
        <h3>Delete Note</h3>
        <p>Are you sure you want to delete the note "{noteTitle}"? This action cannot be undone.</p>
        
        <div className="confirmation-buttons">
          <Button
            text="Delete"
            variant="danger"
            onClick={handleConfirm}
          />
        </div>
      </div>
    </div>
  );
};

export default DeleteNoteModal;
