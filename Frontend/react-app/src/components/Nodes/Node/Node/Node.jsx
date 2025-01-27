import React, { forwardRef, useState } from "react";
import Categories from "../Categories/Categories";
import Description from "../Description/Description";
import EditableTitle from "../EditableTitle/EditableTitle";
import Importance from "../Importance/Importance";
import Tags from "../Tags/Tags";
import Timestamp from "../Timestamp/Timestamp";

import "./Node.css";

const Node = forwardRef(({ item, onToggle, isModalActive, setModalActive, openNodeId, setOpenNodeId }, ref) => {
    const isOpen = openNodeId === item.id;
    const [categories, setCategories] = useState(item.categories);
    const [tags, setTags] = useState(item.tags);
    const [description, setDescription] = useState(item.description);
    const [timestamp, setTimestamp] = useState(item.timestamp);
    const [title, setTitle] = useState(item.title);

    const toggleCard = () => {
        if (isModalActive) return;

        if (isOpen) {
            setOpenNodeId(null);
        } else {
            setOpenNodeId(item.id);
        }
        onToggle();
    };

    return (
        <div className={`timeline-node ${isOpen ? "open" : ""}`} ref={ref}>
            <div className="node-header" onClick={toggleCard}>
                <EditableTitle
                    title={title}
                    onUpdateTitle={(newTitle) => setTitle(newTitle)}
                    className="timeline-title"
                />
                <span>{isOpen ? "-" : "+"}</span>
            </div>
            {isOpen && (
                <div className="node-content">
                    <Description
                        description={description}
                        onUpdateDescription={setDescription}
                        setModalActive={setModalActive}
                    />
                    <Timestamp
                        initialValue={timestamp}
                        onSave={(newTimestamp) => setTimestamp(newTimestamp)}
                        setModalActive={setModalActive}
                    />
                    <Importance
                        initialValue={item.importance}
                        onSave={(newImportance) =>
                            console.log("Saved Importance:", newImportance)
                        }
                        setModalActive={setModalActive}
                    />
                    <p>
                        <strong>Phase:</strong> {item.phase.title}
                    </p>
                    <Categories
                        categories={categories}
                        onUpdateCategories={setCategories}
                        setModalActive={setModalActive}
                    />
                    <Tags
                        tags={tags}
                        onUpdateTags={setTags}
                        setModalActive={setModalActive}
                    />
                </div>
            )}
        </div>
    );
});

export default Node;
