import React, { useState } from "react";

import EditButton from "../../../../core/components/buttons/EditButton/EditButton";
import InputStringModal from "../../../../core/components/modals/InputStringModal/InputStringModal";
import convertStringToColor from "../../../../core/utils/ConvertStringToColor";

import "./Categories.css";

const Categories = ({ categories, onUpdateCategories, setModalActive }) => {
    const [isModalOpen, setModalOpen] = useState(false);
    const [localCategories, setLocalCategories] = useState(categories);

    const setModalState = (isActive) => {
        setModalOpen(isActive);
        setModalActive(isActive);
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
            <EditButton onClick={() => setModalState(true)} />
            <InputStringModal
                isOpen={isModalOpen}
                onClose={() => setModalState(false)}
                onSave={handleSaveCategories}
                initialValue={localCategories.join(", ")}
                title="Edit Categories"
            />
        </div>
    );
};

export default Categories;
