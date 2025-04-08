import React, { useEffect, useState } from "react";
import { CiEdit } from "react-icons/ci";
import NodeService from "../../../../services/NodeService";
import IconButton from "../../../../core/components/buttons/IconButton/IconButton";
import IntegerModal from "../../../../core/components/modals/IntegerModal/IntegerModal";
import { toast } from "react-toastify";
import "./Importance.css";

const Importance = ({ nodeId, setModalActive, initialValue, onSave }) => {
  const root = "importance";
  const [importance, setImportance] = useState(initialValue || 0);
  const [isModalOpen, setModalOpen] = useState(false);
  const [isLoading, setIsLoading] = useState(false);

  useEffect(() => {
    setImportance(initialValue || 0);
  }, [initialValue]);

  const setModalState = (isActive) => {
    setModalOpen(isActive);
    setModalActive(isActive);
  };

  const handleSaveImportance = async (newImportance) => {
    setIsLoading(true);
    try {
      await NodeService.updateNode(nodeId, { importance: newImportance });

      setImportance(newImportance);

      if (onSave) {
        onSave(newImportance);
      }

      toast.success("Importance level updated successfully!");
    } catch (error) {
      console.error("Error saving importance:", error);
      toast.error("Failed to update importance level");
      setImportance(initialValue || 0);
    } finally {
      setIsLoading(false);
      setModalState(false);
    }
  };

  return (
    <div className={`${root}-container`}>
      <div className={`${root}-content`}>
        <strong>Importance:</strong> {importance}
      </div>
      <IconButton
        onClick={() => setModalState(true)}
        icon={<CiEdit />}
        title="Edit"
        disabled={isLoading}
      />
      <IntegerModal
        isOpen={isModalOpen}
        onClose={() => setModalState(false)}
        onSave={handleSaveImportance}
        initialValue={importance}
        isLoading={isLoading}
      />
    </div>
  );
};

export default Importance;
