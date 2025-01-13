import React, { useState } from "react";

import EditButton from "../../core/components/buttons/EditButton/EditButton";
import InputCSVModal from "../../core/components/modals/InputCSVModal/InputCSVModal";
import convertStringToColor from "../../core/utils/ConvertStringToColor";

import "./Tags.css";

const Tags = ({ tags, onUpdateTags }) => {
    const [isModalOpen, setModalOpen] = useState(false);
    const [localTags, setLocalTags] = useState(tags);

    const handleOpenModal = () => {
        setModalOpen(true);
    };

    const handleCloseModal = () => {
        setModalOpen(false);
    };

    const handleSaveTags = (newTags) => {
        setLocalTags(newTags);
        onUpdateTags(newTags);
        handleCloseModal();
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
            <EditButton onClick={handleOpenModal} />
            <InputCSVModal
                isOpen={isModalOpen}
                onClose={handleCloseModal}
                onSave={handleSaveTags}
                initialValue={localTags.join(", ")}
                title="Edit Tags"
            />
        </div>
    );
};

export default Tags;
