import React, { useState } from "react";
import EditButton from "../../../../core/components/buttons/EditButton/EditButton";
import "./EditableTitle.css";

const EditableTitle = ({ title, onUpdateTitle, className }) => {
    const [isEditing, setIsEditing] = useState(false);
    const [localTitle, setLocalTitle] = useState(title);
    const [isHovered, setIsHovered] = useState(false);

    const setEditing = (isActive) => {
        setIsEditing(isActive);
        if (!isActive) {
            setIsHovered(false);
        }
    };

    const handleChange = (e) => {
        setLocalTitle(e.target.value);
    };

    const handleKeyDown = (e) => {
        if (e.key === "Enter") {
            setEditing(false);
            onUpdateTitle(localTitle);
        }
    };

    const handleBlur = () => {
        setEditing(false);
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
        <div
            className={`editable-title-container ${className}`}
            onMouseEnter={() => setIsHovered(true)}
            onMouseLeave={() => setIsHovered(false)}
        >
            <h3 className="editable-title">{localTitle}</h3>
            {isHovered && (
                <EditButton
                    className="editable-title-edit-icon"
                    onClick={(e) => {
                        e.stopPropagation();
                        setEditing(true);
                    }}
                />
            )}
        </div>
    );
};

export default EditableTitle;
