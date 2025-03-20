import React from "react";
import "./TextButton.css";

const TextButton = ({ onClick, disabled, text, color = "default", className = "" }) => {
    return (
        <button
            className={`text-button ${color} ${disabled ? "disabled" : ""} ${className}`}
            onClick={onClick}
            disabled={disabled}
        >
            {text}
        </button>
    );
};

export default TextButton;
