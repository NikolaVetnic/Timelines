import React, { useState } from "react";

import EditButton from "../../../../core/components/buttons/EditButton/EditButton";
import InputStringModal from "../../../../core/components/modals/InputStringModal/InputStringModal";
import convertStringToColor from "../../../../core/utils/ConvertStringToColor";

import "./Categories.css";

const Categories = ({ categories, onUpdateCategories }) => {
    const [isModalOpen, setModalOpen] = useState(false);
    const [localCategories, setLocalCategories] = useState(categories);

    const setModalActive = (isActive) => {
        setModalOpen(isActive);
    };

    const handleSaveCategories = (newCategories) => {
        setLocalCategories(newCategories);
        onUpdateCategories(newCategories);
        setModalActive(false);
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
            <EditButton onClick={() => setModalActive(true)} />
            <InputStringModal
                isOpen={isModalOpen}
                onClose={() => setModalActive(false)}
                onSave={handleSaveCategories}
                initialValue={localCategories.join(", ")}
                title="Edit Categories"
            />
        </div>
    );
};

export default Categories;
