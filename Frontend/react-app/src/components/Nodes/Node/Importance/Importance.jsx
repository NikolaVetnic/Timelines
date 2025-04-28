import React, { useEffect, useState } from "react";
import { CiEdit } from "react-icons/ci";
import Button from "../../../../core/components/buttons/Button/Button";
import IntegerModal from "../../../../core/components/modals/IntegerModal/IntegerModal";
import NodeService from "../../../../services/NodeService";
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
    await NodeService.updateNode(nodeId, { importance: newImportance });

    setImportance(newImportance);

    if (onSave) {
      onSave(newImportance);
    }

    setIsLoading(false);
    setModalState(false);
  };

  return (
    <div className={`${root}-container`}>
      <div className={`${root}-content`}>
        <strong>Importance:</strong> {importance}
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
