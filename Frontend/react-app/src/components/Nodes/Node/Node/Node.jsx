import React, { forwardRef, useState } from "react";

import Categories from "../Categories/Categories";
import Description from "../Description/Description";
import EditableTitle from "../EditableTitle/EditableTitle";
import Importance from "../Importance/Importance";
import Tags from "../Tags/Tags";
import Timestamp from "../Timestamp/Timestamp";

import "./Node.css";

const Node = forwardRef(({ item, onToggle }, ref) => {
    const [isOpen, setIsOpen] = useState(false);
    const [categories, setCategories] = useState(item.categories);
    const [tags, setTags] = useState(item.tags);
    const [description, setDescription] = useState(item.description);
    const [timestamp, setTimestamp] = useState(item.timestamp);
    const [title, setTitle] = useState(item.title);

    const toggleCard = () => {
        setIsOpen(!isOpen);
        onToggle();
    };

    return (
        <div className={`timeline-card ${isOpen ? "open" : ""}`} ref={ref}>
            <div className="card-header" onClick={toggleCard}>
                <EditableTitle
                    title={title}
                    onUpdateTitle={(newTitle) => setTitle(newTitle)}
                    className="timeline-title"
                />
                <span>{isOpen ? "-" : "+"}</span>
            </div>
            {isOpen && (
                <div className="card-content">
                    <Description
                        description={description}
                        onUpdateDescription={setDescription}
                    />
                    <Timestamp
                        initialValue={timestamp}
                        onSave={(newTimestamp) => setTimestamp(newTimestamp)}
                    />
                    <Importance
                        initialValue={item.importance}
                        onSave={(newImportance) =>
                            console.log("Saved Importance:", newImportance)
                        }
                    />
                    <p>
                        <strong>Phase:</strong> {item.phase}
                    </p>
                    <Categories
                        categories={categories}
                        onUpdateCategories={setCategories}
                    />
                    <Tags
                        tags={tags}
                        onUpdateTags={setTags}
                    />
                    {item.image && <img src={item.image} alt={title} />}
                </div>
            )}
        </div>
    );
});

export default Node;
