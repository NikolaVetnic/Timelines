import React, { useEffect, useState } from "react";
import { CiEdit } from "react-icons/ci";
import Button from "../../../../core/components/buttons/Button/Button";
import NodeService from "../../../../services/NodeService";
import "./EditableTitle.css";

const EditableTitle = ({
  node,
  className,
  title: propTitle,
  onUpdateTitle,
}) => {
  const root = "editable-title";
  const [isEditing, setIsEditing] = useState(false);
  const [localTitle, setLocalTitle] = useState(propTitle || "");
  const [isHovered, setIsHovered] = useState(false);

  useEffect(() => {
    setLocalTitle(propTitle || "");
  }, [propTitle]);

  const setEditing = (isActive) => {
    setIsEditing(isActive);
    if (!isActive) {
      setIsHovered(false);
    }
  };

  const handleChange = (e) => {
    setLocalTitle(e.target.value);
  };

  const handleSaveTitle = async () => {
    await NodeService.updateNode(node, { title: localTitle });

    if (onUpdateTitle) {
      onUpdateTitle(localTitle);
    }

    setEditing(false);
  };

  const handleKeyDown = (e) => {
    if (e.key === "Enter") {
      handleSaveTitle();
    }
  };

  const handleBlur = () => {
    handleSaveTitle();
  };

  return isEditing ? (
    <input
      type="text"
      value={localTitle}
      onChange={handleChange}
      onKeyDown={handleKeyDown}
      onBlur={handleBlur}
      className={`${root} ${className}`}
      autoFocus
    />
  ) : (
    <div
      className={`${root}-container ${className}`}
      onMouseEnter={() => setIsHovered(true)}
      onMouseLeave={() => setIsHovered(false)}
    >
      <h3 className={`${root}`}>{localTitle || "Untitled"}</h3>
      {isHovered && (
        <Button
          icon={<CiEdit />}
          iconOnly
          variant="info"
          shape="square"
          size="little"
          onClick={(e) => {
            e.stopPropagation();
            setEditing(true);
          }}
        />
      )}
    </div>
  );
};

export default EditableTitle;
