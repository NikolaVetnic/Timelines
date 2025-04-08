import React, { useEffect, useState } from "react";
import { CiEdit } from "react-icons/ci";
import NodeService from "../../../../services/NodeService";
import IconButton from "../../../../core/components/buttons/IconButton/IconButton";
import DatePickerModal from "../../../../core/components/modals/DatePickerModal/DatePickerModal";
import { toast } from "react-toastify";
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

      toast.success("Timestamp updated successfully!");
    } catch (error) {
      console.error("Error saving timestamp:", error);
      toast.error("Failed to update timestamp");
      setLocalTimestamp(initialValue ? new Date(initialValue) : null);
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
      <IconButton
        onClick={() => setModalState(true)}
        icon={<CiEdit />}
        title="Edit"
        disabled={isLoading}
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
