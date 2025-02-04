import React, { forwardRef, useState } from "react";
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

const Node = forwardRef(({ item, onToggle, isModalActive, setModalActive, openNodeId, setOpenNodeId, timelineId }, ref) => {
    const isOpen = openNodeId === item.id;
    const [categories, setCategories] = useState(item.categories);
    const [tags, setTags] = useState(item.tags);
    const [description, setDescription] = useState(item.description);
    const [timestamp, setTimestamp] = useState(item.timestamp);
    const [title, setTitle] = useState(item.title);
    const [notes, setNotes] = useState(item.notes || []);

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
                    nodeId={item.id}
                    timelineId={timelineId}
                    title={title}
                    onUpdateTitle={(newTitle) => setTitle(newTitle)}
                    className="timeline-title"
                />
                <span>{isOpen ? "-" : "+"}</span>
            </div>
            {isOpen && (
                <div className="node-content">
                    <Description
                        nodeId={item.id}
                        timelineId={timelineId}
                        description={description}
                        onUpdateDescription={setDescription}
                        setModalActive={setModalActive}
                    />
                    <Timestamp
                        nodeId={item.id}
                        timelineId={timelineId}
                        initialValue={timestamp}
                        onSave={(newTimestamp) => setTimestamp(newTimestamp)}
                        setModalActive={setModalActive}
                    />
                    <Importance
                        nodeId={item.id}
                        timelineId={timelineId} 
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
                        nodeId={item.id}
                        timelineId={timelineId}
                        categories={categories}
                        onUpdateCategories={setCategories}
                        setModalActive={setModalActive}
                    />
                    <Tags
                        nodeId={item.id}
                        timelineId={timelineId}
                        tags={tags}
                        onUpdateTags={setTags}
                        setModalActive={setModalActive}
                    />
                    <Note
                        nodeId={item.id}
                        timelineId={timelineId}
                        notes={notes} 
                        setNotes={setNotes}
                        onToggle={onToggle}
                    />
                    <Reminder
                        nodeId={item.id}
                        timelineId={timelineId}
                        onToggle={onToggle}
                    />
                    <File
                        nodeId={item.id}
                        timelineId={timelineId}
                        onToggle={onToggle}
                    />
                </div>
            )}
        </div>
    );
});

export default Node;
