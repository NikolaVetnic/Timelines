import React, { useEffect, useState } from "react";
import { CiEdit } from "react-icons/ci";
import { toast } from "react-toastify";
import Button from "../../../../core/components/buttons/Button/Button";
import InputStringModal from "../../../../core/components/modals/InputStringModal/InputStringModal";
import convertStringToColor from "../../../../core/utils/ConvertStringToColor";
import NodeService from "../../../../services/NodeService";
import "./Tags.css";

const Tags = ({ nodeId, setModalActive, tags: propTags, onUpdateTags }) => {
  const root = "tags";
  const [isModalOpen, setModalOpen] = useState(false);
  const [localTags, setLocalTags] = useState(propTags || []);
  const [isLoading, setIsLoading] = useState(false);

  useEffect(() => {
    setLocalTags(propTags || []);
  }, [propTags]);

  const setModalState = (isActive) => {
    setModalOpen(isActive);
    setModalActive(isActive);
  };

  const formatTags = (input) => {
    return input
      .split(",")
      .map((tag) => tag.trim().toLowerCase().replace(/\s+/g, "-"))
      .filter((tag) => tag.length > 0);
  };

  const handleSaveTags = async (tagsInput) => {
    setIsLoading(true);
    const formattedTags = formatTags(tagsInput);

    try {
      await NodeService.updateNode(nodeId, { tags: formattedTags });

      setLocalTags(formattedTags);

      if (onUpdateTags) {
        onUpdateTags(formattedTags);
      }

      toast.success("Tags updated successfully!");
    } catch (error) {
      console.error("Error saving tags:", error);
      toast.error("Failed to update tags");
      setLocalTags(propTags || []);
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
        initialValue={localTags.join(", ")}
        title="Edit Tags"
        isLoading={isLoading}
        placeholder="Enter tags, separated by commas (e.g., 'important, ui-fix')"
      />
    </div>
  );
};

export default Tags;
