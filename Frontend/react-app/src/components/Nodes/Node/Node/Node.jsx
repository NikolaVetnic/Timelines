import React, { forwardRef, useState } from "react";

import { FaMinus, FaPlus } from "react-icons/fa";
import File from "../../Files/File/File";
import Note from "../../Notes/Note/Note";
import Reminder from "../../Reminders/Reminder/Reminder";
import Categories from "../Categories/Categories";
import Description from "../Description/Description";
import EditableTitle from "../EditableTitle/EditableTitle";
import Importance from "../Importance/Importance";
import Tags from "../Tags/Tags";
import Timestamp from "../Timestamp/Timestamp";

import "./Node.css";

const Node = forwardRef(
  (
    {
      node,
      onToggle,
      isModalActive,
      setModalActive,
      openNodeId,
      setOpenNodeId,
      timelineId,
      isSelected,
      onSelect,
      onDeselect,
      onToggleSelectAll,
    },
    ref
  ) => {
    const root = "node";
    const isOpen = openNodeId === node.id;
    const [categories, setCategories] = useState(node.categories || []);
    const [tags, setTags] = useState(node.tags || []);
    const [description, setDescription] = useState(node.description || "");
    const [timestamp, setTimestamp] = useState(node.timestamp || "");
    const [title, setTitle] = useState(node.title || "Untitled");
    const [notes, setNotes] = useState(node.notes || []);

    const toggleCard = () => {
      if (isModalActive) return;
      if (isOpen) {
        setOpenNodeId(null);
      } else {
        setOpenNodeId(node.id);
      }
      onToggle();
    };

    const toggleSelection = () => {
      if (isSelected) {
        onDeselect(node.id);
      } else {
        onSelect(node.id);
      }
    };

    return (
      <div className={`${root}-timeline ${isOpen ? "open" : ""}`} ref={ref}>
        <div className="node-header" onClick={toggleCard}>
          <EditableTitle
            node={node}
            title={title}
            onUpdateTitle={(newTitle) => setTitle(newTitle)}
          />
          <div className="node-header-actions">
            <input
              type="checkbox"
              className="node-checkbox"
              checked={isSelected}
              onChange={toggleSelection}
              onClick={(e) => e.stopPropagation()}
            />
            <span className={`${root}-toggle`}>
              {isOpen ? <FaMinus size={15} /> : <FaPlus size={15} />}
            </span>
          </div>
        </div>
        {isOpen && (
          <div className={`${root}-content`}>
            <Description
              node={node}
              description={description}
              onUpdateDescription={setDescription}
              setModalActive={setModalActive}
            />
            <Timestamp
              node={node}
              initialValue={timestamp}
              onSave={(newTimestamp) => setTimestamp(newTimestamp)}
              setModalActive={setModalActive}
            />
            <Importance
              node={node}
              initialValue={node.importance}
              onSave={(newImportance) =>
                console.log("Saved Importance:", newImportance)
              }
              setModalActive={setModalActive}
            />
            <p className="node-phase">
              <strong>Phase:</strong> {node.phase}
            </p>
            <Categories
              node={node}
              categories={categories}
              onUpdateCategories={setCategories}
              setModalActive={setModalActive}
            />
            <Tags
              node={node}
              tags={tags}
              onUpdateTags={setTags}
              setModalActive={setModalActive}
            />
            <Note
              node={node}
              timelineId={timelineId}
              notes={notes}
              setNotes={setNotes}
              onToggle={onToggle}
            />
            <Reminder
              node={node}
              timelineId={timelineId}
              onToggle={onToggle}
            />
            <File
              node={node}
              timelineId={timelineId}
              onToggle={onToggle}
            />
          </div>
        )}
      </div>
    );
  }
);

export default Node;
