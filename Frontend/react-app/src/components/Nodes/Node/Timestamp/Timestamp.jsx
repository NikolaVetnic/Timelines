import React, { useEffect, useState } from "react";
import { CiEdit } from "react-icons/ci";
import Button from "../../../../core/components/buttons/Button/Button";
import DatePickerModal from "../../../../core/components/modals/DatePickerModal/DatePickerModal";
import NodeService from "../../../../services/NodeService";
import "./Timestamp.css";

const Timestamp = ({ nodeId, setModalActive, initialValue, onSave }) => {
  const root = "timestamp";
  const [isModalOpen, setModalOpen] = useState(false);
  const [localTimestamp, setLocalTimestamp] = useState(
    initialValue ? new Date(initialValue) : null
  );
  const [isLoading, setIsLoading] = useState(false);

  useEffect(() => {
    setLocalTimestamp(initialValue ? new Date(initialValue) : null);
  }, [initialValue]);

  const setModalState = (isActive) => {
    setModalOpen(isActive);
    setModalActive(isActive);
  };

  const handleSaveTimestamp = async (newTimestamp) => {
    setIsLoading(true);
    try {
      const isoString = newTimestamp.toISOString();

      await NodeService.updateNode(nodeId, { timestamp: isoString });

      setLocalTimestamp(newTimestamp);

      if (onSave) {
        onSave(isoString);
      }
    } finally {
      setIsLoading(false);
      setModalState(false);
    }
  };

  return (
    <div className={`${root}-container`}>
      <div>
        <strong>Timestamp:</strong>{" "}
        {localTimestamp ? localTimestamp.toLocaleDateString() : "Not Set"}
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
      <DatePickerModal
        isOpen={isModalOpen}
        onClose={() => setModalState(false)}
        onSave={handleSaveTimestamp}
        initialValue={localTimestamp || new Date()}
        title="Edit Timestamp"
        isLoading={isLoading}
      />
    </div>
  );
};

export default Timestamp;
