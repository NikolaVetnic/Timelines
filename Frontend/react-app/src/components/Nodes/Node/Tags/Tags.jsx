import React, { useEffect, useState } from "react";
import { CiEdit } from "react-icons/ci";
import Button from "../../../../core/components/buttons/Button/Button";
import InputStringModal from "../../../../core/components/modals/InputStringModal/InputStringModal";
import convertStringToColor from "../../../../core/utils/ConvertStringToColor";
import NodeService from "../../../../services/NodeService";
import "./Tags.css";

const Tags = ({ node, setModalActive, tags: propTags, onUpdateTags }) => {
  const root = "tags";
  const [isModalOpen, setModalOpen] = useState(false);
  const [localTags, setLocalTags] = useState([]);
  const [isLoading, setIsLoading] = useState(false);

  useEffect(() => {
    setLocalTags(Array.isArray(propTags) ? propTags : []);
  }, [propTags]);

  const setModalState = (isActive) => {
    setModalOpen(isActive);
    setModalActive(isActive);
  };

  const formatTags = (input) => {
    if (typeof input !== 'string') {
      if (Array.isArray(input)) {
        return input
          .map(tag => String(tag).trim().toLowerCase().replace(/\s+/g, "-"))
          .filter(tag => tag.length > 0);
      }
      return [];
    }
    
    return input
      .split(",")
      .map((tag) => tag.trim().toLowerCase().replace(/\s+/g, "-"))
      .filter((tag) => tag.length > 0);
  };

  const handleSaveTags = async (tagsInput) => {
    setIsLoading(true);
    try {
      const formattedTags = formatTags(tagsInput);
      
      await NodeService.updateNode(node, { 
        tags: formattedTags 
      });

      setLocalTags(formattedTags);
      
      if (onUpdateTags) {
        onUpdateTags(formattedTags);
      }
    } finally {
      setIsLoading(false);
      setModalState(false);
    }
  };

  return (
    <div className={`${root}-container`}>
      <div>
        <strong>Tags:</strong>{" "}
        {localTags.length > 0 ? (
          localTags.map((tag, index) => (
            <span
              key={index}
              className={`${root}-badge`}
              style={{ backgroundColor: convertStringToColor(tag) }}
            >
              {tag}
            </span>
          ))
        ) : (
          <span>No Tags Set</span>
        )}
      </div>
      <Button
        icon={<CiEdit />}
        iconOnly
        variant="info"
        shape="square"
        size="little"
        disabled={isLoading}
        onClick={() => setModalState(true)}
      />
      <InputStringModal
        isOpen={isModalOpen}
        onClose={() => setModalState(false)}
        onSave={handleSaveTags}
        initialValue={Array.isArray(localTags) ? localTags.join(", ") : ""}
        title="Edit Tags"
        isLoading={isLoading}
        dataType="array"
        placeholder="Enter tags, separated by commas (e.g., 'important, ui-fix')"
      />
    </div>
  );
};

export default Tags;
