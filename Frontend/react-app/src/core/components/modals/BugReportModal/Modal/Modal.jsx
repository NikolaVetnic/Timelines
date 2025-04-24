import React from "react";
import { createPortal } from "react-dom";
import "./Modal.css"; // Create this file for modal styles

const Modal = ({ children, onClose }) => {
  return createPortal(
    <div className="modal-overlay">
      <div className="modal-backdrop" onClick={onClose} />
      <div className="modal-content">
        <button className="modal-close" onClick={onClose}>
          &times;
        </button>
        {children}
      </div>
    </div>,
    document.body
  );
};

export default Modal;
