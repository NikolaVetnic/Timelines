import React, { useEffect, useState } from "react";
import EditButton from "../../../../core/components/buttons/EditButton/EditButton";
import "./EditableTitle.css";

const LOCAL_STORAGE_KEY = "timelineData";

const EditableTitle = ({ timelineId, nodeId, className }) => {
    const [isEditing, setIsEditing] = useState(false);
    const [localTitle, setLocalTitle] = useState("");
    const [isHovered, setIsHovered] = useState(false);

    useEffect(() => {
        try {
            const storedData = JSON.parse(localStorage.getItem(LOCAL_STORAGE_KEY)) || [];
            const timeline = storedData.find(t => t.id === timelineId);
            const node = timeline?.nodes.find(n => n.id === nodeId);
            if (node?.title) {
                setLocalTitle(node.title);
            }
        } catch (error) {
            console.error("Error loading title:", error);
        }
    }, [timelineId, nodeId]);

    // todo: connect to backend
    const updateLocalStorage = (newTitle) => {
        try {
            const storedData = JSON.parse(localStorage.getItem(LOCAL_STORAGE_KEY)) || [];
            const timelineIndex = storedData.findIndex(t => t.id === timelineId);

            if (timelineIndex !== -1) {
                const nodeIndex = storedData[timelineIndex].nodes.findIndex(n => n.id === nodeId);
                if (nodeIndex !== -1) {
                    storedData[timelineIndex].nodes[nodeIndex].title = newTitle;
                    localStorage.setItem(LOCAL_STORAGE_KEY, JSON.stringify(storedData));
                }
            }
        } catch (error) {
            console.error("Error saving title:", error);
        }
    };

    const setEditing = (isActive) => {
        setIsEditing(isActive);
        if (!isActive) {
            setIsHovered(false);
        }
    };

    const handleChange = (e) => {
        setLocalTitle(e.target.value);
    };

    const handleSaveTitle = () => {
        setEditing(false);
        updateLocalStorage(localTitle);
    };

    const handleKeyDown = (e) => {
        if (e.key === "Enter") {
            handleSaveTitle();
        }
    };

    const handleBlur = () => {
        handleSaveTitle();
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
            <h3 className="editable-title">{localTitle || "Untitled"}</h3>
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
