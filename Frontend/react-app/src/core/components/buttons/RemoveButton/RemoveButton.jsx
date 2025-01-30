import React from "react";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import "./RemoveButton.css";

const RemoveButton = ({ id, onRemove, message }) => {
  const handleClick = () => {
    onRemove(id);
    toast.error(message);
  };

  return (
    <button className="remove-button" onClick={handleClick}>
      ✖
    </button>
  );
};

export default RemoveButton;
