import React, { useState } from "react";

import EditButton from "../../../../core/components/buttons/EditButton/EditButton";
import InputStringModal from "../../../../core/components/modals/InputStringModal/InputStringModal";
import convertStringToColor from "../../../../core/utils/ConvertStringToColor";

import "./Tags.css";

const Tags = ({ tags, onUpdateTags, setModalActive }) => {
    const [isModalOpen, setModalOpen] = useState(false);
    const [localTags, setLocalTags] = useState(tags);

    const setModalState = (isActive) => {
        setModalOpen(isActive);
        setModalActive(isActive);
    };

    const handleSaveTags = (newTags) => {
        setLocalTags(newTags);
        onUpdateTags(newTags);
        setModalActive(false);
    };

    return (
        <div className="tags-container">
            <div>
                <strong>Tags:</strong>{" "}
                {localTags.map((tag, index) => (
                    <span
                        key={index}
                        className="tag-badge"
                        style={{ backgroundColor: convertStringToColor(tag) }}
                    >
                        {tag}
                    </span>
                ))}
            </div>
            <EditButton onClick={() => setModalState(true)} />
            <InputStringModal
                isOpen={isModalOpen}
                onClose={() => setModalState(false)}
                onSave={handleSaveTags}
                initialValue={localTags.join(", ")}
                title="Edit Tags"
            />
        </div>
    );
};

export default Tags;
