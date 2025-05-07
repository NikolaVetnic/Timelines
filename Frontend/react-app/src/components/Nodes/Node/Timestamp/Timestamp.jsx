import React, { useEffect, useState } from "react";
import { CiEdit } from "react-icons/ci";
import { toast } from "react-toastify";
import Button from "../../../../core/components/buttons/Button/Button";
import DatePickerModal from "../../../../core/components/modals/DatePickerModal/DatePickerModal";
import NodeService from "../../../../services/NodeService";
import "./Timestamp.css";

const Timestamp = ({ node, setModalActive, initialValue, onSave }) => {
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
    const now = new Date();
    if (newTimestamp > now) {
      toast.error("Timestamp cannot be in the future");
      return;
    }

    setIsLoading(true);
    try {
      const isoString = newTimestamp.toISOString();
      await NodeService.updateNode(node, { timestamp: isoString });
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
        maxDate={new Date()}
      />
    </div>
  );
};

export default Timestamp;
