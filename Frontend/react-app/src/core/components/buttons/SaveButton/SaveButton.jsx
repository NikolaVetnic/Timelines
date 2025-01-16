import React from "react";
import "./SaveButton.css";

const SaveButton = ({ onClick, disabled }) => {
    return (
        <button
            className={`modal-save-button ${disabled ? "disabled" : ""}`}
            onClick={onClick}
            disabled={disabled}
        >
            Save
        </button>
    );
};

export default SaveButton;
