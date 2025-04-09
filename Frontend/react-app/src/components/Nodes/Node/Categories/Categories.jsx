import React, { useEffect, useState } from "react";
import { CiEdit } from "react-icons/ci";
import NodeService from "../../../../services/NodeService";
import IconButton from "../../../../core/components/buttons/IconButton/IconButton";
import InputStringModal from "../../../../core/components/modals/InputStringModal/InputStringModal";
import convertStringToColor from "../../../../core/utils/ConvertStringToColor";
import { toast } from "react-toastify";
import "./Categories.css";

const Categories = ({
  nodeId,
  setModalActive,
  categories: propCategories,
  onUpdateCategories,
}) => {
  const root = "categories";
  const [isModalOpen, setModalOpen] = useState(false);
  const [localCategories, setLocalCategories] = useState(propCategories || []);
  const [isLoading, setIsLoading] = useState(false);

  useEffect(() => {
    setLocalCategories(propCategories || []);
  }, [propCategories]);

  const setModalState = (isActive) => {
    setModalOpen(isActive);
    setModalActive(isActive);
  };

  const parseCategoriesInput = (input) => {
    return input
      .split(",")
      .map((cat) => cat.trim())
      .filter((cat) => cat.length > 0);
  };

  const handleSaveCategories = async (categoriesInput) => {
    setIsLoading(true);
    const newCategories = parseCategoriesInput(categoriesInput);

    try {
      await NodeService.updateNode(nodeId, { categories: newCategories });

      setLocalCategories(newCategories);

      if (onUpdateCategories) {
        onUpdateCategories(newCategories);
      }

      toast.success("Categories updated successfully!");
    } catch (error) {
      console.error("Error saving categories:", error);
      toast.error("Failed to update categories");
      setLocalCategories(propCategories || []);
    } finally {
      setIsLoading(false);
      setModalState(false);
    }
  };

  return (
    <div className={`${root}-container`}>
      <div>
        <strong>Categories:</strong>{" "}
        {localCategories.length > 0 ? (
          localCategories.map((category, index) => (
            <span
              key={index}
              className={`${root}-badge`}
              style={{ backgroundColor: convertStringToColor(category) }}
            >
              {category}
            </span>
          ))
        ) : (
          <span>No Categories Set</span>
        )}
      </div>
      <IconButton
        icon={<CiEdit />}
        title="Edit Categories"
        hoverColor="var(--enmBlueHover)"
        onClick={() => setModalState(true)}
        disabled={isLoading}
      />
      <InputStringModal
        isOpen={isModalOpen}
        onClose={() => setModalState(false)}
        onSave={handleSaveCategories}
        initialValue={localCategories.join(", ")}
        title="Edit Categories"
        isLoading={isLoading}
        placeholder="Enter categories, separated by commas"
      />
    </div>
  );
};

export default Categories;
