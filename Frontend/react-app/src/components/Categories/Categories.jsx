import React, { useState } from "react";

import EditButton from "../../core/components/buttons/EditButton/EditButton";
import InputCSVModal from "../../core/components/modals/InputCSVModal/InputCSVModal";
import convertStringToColor from "../../core/utils/ConvertStringToColor";

import "./Categories.css";

const Categories = ({ categories, onUpdateCategories }) => {
    const [isModalOpen, setModalOpen] = useState(false);
    const [localCategories, setLocalCategories] = useState(categories);

    const handleOpenModal = () => {
        setModalOpen(true);
    };

    const handleCloseModal = () => {
        setModalOpen(false);
    };

    const handleSaveCategories = (newCategories) => {
        setLocalCategories(newCategories);
        onUpdateCategories(newCategories);
        handleCloseModal();
    };

    return (
        <div className="categories-container">
            <div>
                <strong>Categories:</strong>{" "}
                {localCategories.map((category, index) => (
                    <span
                        key={index}
                        className="category-badge"
                        style={{ backgroundColor: convertStringToColor(category) }}
                    >
                        {category}
                    </span>
                ))}
            </div>
            <EditButton onClick={handleOpenModal} />
            <InputCSVModal
                isOpen={isModalOpen}
                onClose={handleCloseModal}
                onSave={handleSaveCategories}
                initialValue={localCategories.join(", ")}
                title="Edit Categories"
            />
        </div>
    );
};

export default Categories;
