import React from "react";
import "./CancelButton.css";

const CancelButton = ({ onClick }) => {
    return (
        <button className="modal-cancel-button" onClick={onClick}>
            Cancel
        </button>
    );
};

export default CancelButton;
