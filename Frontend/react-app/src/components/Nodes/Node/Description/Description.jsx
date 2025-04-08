import React, { useEffect, useState } from "react";
import { CiEdit } from "react-icons/ci";
import NodeService from "../../../../services/NodeService";
import IconButton from "../../../../core/components/buttons/IconButton/IconButton";
import TextAreaModal from "../../../../core/components/modals/TextAreaModal/TextAreaModal";
import { toast } from "react-toastify";
import "./Description.css";

const Description = ({
  nodeId,
  setModalActive,
  description: propDescription,
  onUpdateDescription,
}) => {
  const root = "description";
  const [isModalOpen, setModalOpen] = useState(false);
  const [localDescription, setLocalDescription] = useState(
    propDescription || ""
  );
  const [isLoading, setIsLoading] = useState(false);

  useEffect(() => {
    setLocalDescription(propDescription || "");
  }, [propDescription]);

  const setModalState = (isActive) => {
    setModalOpen(isActive);
    setModalActive(isActive);
  };

  const handleSaveDescription = async (newDescription) => {
    setIsLoading(true);
    try {
      await NodeService.updateNode(nodeId, { description: newDescription });

      setLocalDescription(newDescription);

      if (onUpdateDescription) {
        onUpdateDescription(newDescription);
      }

      toast.success("Description updated successfully!");
    } catch (error) {
      console.error("Error saving description:", error);
      toast.error("Failed to update description");
      setLocalDescription(propDescription || "");
    } finally {
      setIsLoading(false);
      setModalState(false);
    }
  };

  return (
    <div className={`${root}-container`}>
      <div>
        <strong>Description:</strong> {localDescription || "No Description Set"}
      </div>
      <IconButton
        onClick={() => setModalState(true)}
        icon={<CiEdit />}
        title="Edit"
        disabled={isLoading}
      />
      <TextAreaModal
        isOpen={isModalOpen}
        onClose={() => setModalState(false)}
        onSave={handleSaveDescription}
        initialValue={localDescription}
        title="Edit Description"
        isLoading={isLoading}
      />
    </div>
  );
};

export default Description;
