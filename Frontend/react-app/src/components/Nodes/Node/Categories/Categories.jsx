import React, { useEffect, useState } from "react";
import { CiEdit } from "react-icons/ci";
import Button from "../../../../core/components/buttons/Button/Button";
import InputStringModal from "../../../../core/components/modals/InputStringModal/InputStringModal";
import convertStringToColor from "../../../../core/utils/ConvertStringToColor";
import NodeService from "../../../../services/NodeService";
import "./Categories.css";

const Categories = ({
  node,
  setModalActive,
  categories: propCategories,
  onUpdateCategories,
}) => {
  const root = "categories";
  const [isModalOpen, setModalOpen] = useState(false);
  const [localCategories, setLocalCategories] = useState([]);
  const [isLoading, setIsLoading] = useState(false);

  useEffect(() => {
    setLocalCategories(Array.isArray(propCategories) ? propCategories : []);
  }, [propCategories]);

  const setModalState = (isActive) => {
    setModalOpen(isActive);
    setModalActive(isActive);
  };

  const parseCategoriesInput = (input) => {
    if (typeof input !== 'string') {
      if (Array.isArray(input)) {
        return input.map(cat => String(cat).trim()).filter(cat => cat.length > 0);
      }
      return [];
    }
    
    return input
      .split(",")
      .map((cat) => cat.trim())
      .filter((cat) => cat.length > 0);
  };

  const handleSaveCategories = async (categoriesInput) => {
    setIsLoading(true);
    try {
      const newCategories = parseCategoriesInput(categoriesInput);
      
      await NodeService.updateNode(node, { 
        categories: newCategories 
      });

      setLocalCategories(newCategories);
      
      if (onUpdateCategories) {
        onUpdateCategories(newCategories);
      }
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
      <Button
        icon={<CiEdit />}
        iconOnly
        variant="info"
        shape="square"
        size="little"
        onClick={() => setModalState(true)}
      />
      <InputStringModal
        isOpen={isModalOpen}
        onClose={() => setModalState(false)}
        onSave={handleSaveCategories}
        initialValue={Array.isArray(localCategories) ? localCategories.join(", ") : ""}
        title="Edit Categories"
        isLoading={isLoading}
        dataType="array"
        placeholder="Enter categories, separated by commas"
      />
    </div>
  );
};

export default Categories;
