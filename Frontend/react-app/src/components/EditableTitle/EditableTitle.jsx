import React, { useState } from "react";
import "./EditableTitle.css";

const EditableTitle = ({ title, onUpdateTitle, className }) => {
    const [isEditing, setIsEditing] = useState(false);
    const [localTitle, setLocalTitle] = useState(title);

    const handleDoubleClick = (e) => {
        e.stopPropagation();
        setIsEditing(true);
    };

    const handleClick = (e) => {
        e.stopPropagation();
    };

    const handleChange = (e) => {
        setLocalTitle(e.target.value);
    };

    const handleKeyDown = (e) => {
        if (e.key === "Enter") {
            handleSave();
        }
    };

    const handleBlur = () => {
        handleSave();
    };

    const handleSave = () => {
        setIsEditing(false);
        onUpdateTitle(localTitle);
    };

    return isEditing ? (
        <input
            type="text"
            value={localTitle}
            onChange={handleChange}
            onKeyDown={handleKeyDown}
            onBlur={handleBlur}
            className={`editable-title ${className}`}
            autoFocus
        />
    ) : (
        <h3
            onDoubleClick={handleDoubleClick}
            onClick={handleClick}
            className={`editable-title ${className}`}
        >
            {localTitle}
        </h3>
    );
};

export default EditableTitle;
