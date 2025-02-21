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

const Node = forwardRef(({ node, onToggle, isModalActive, setModalActive, openNodeId, setOpenNodeId, timelineId }, ref) => {
    const isOpen = openNodeId === node.id;
    const [categories, setCategories] = useState(node.categories);
    const [tags, setTags] = useState(node.tags);
    const [description, setDescription] = useState(node.description);
    const [timestamp, setTimestamp] = useState(node.timestamp);
    const [title, setTitle] = useState(node.title);
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

    return (
        <div className={`timeline-node ${isOpen ? "open" : ""}`} ref={ref}>
            <div className="node-header" onClick={toggleCard}>
                <EditableTitle
                    nodeId={node.id}
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
                        nodeId={node.id}
                        timelineId={timelineId}
                        description={description}
                        onUpdateDescription={setDescription}
                        setModalActive={setModalActive}
                    />
                    <Timestamp
                        nodeId={node.id}
                        timelineId={timelineId}
                        initialValue={timestamp}
                        onSave={(newTimestamp) => setTimestamp(newTimestamp)}
                        setModalActive={setModalActive}
                    />
                    <Importance
                        nodeId={node.id}
                        timelineId={timelineId} 
                        initialValue={node.importance}
                        onSave={(newImportance) =>
                            console.log("Saved Importance:", newImportance)
                        }
                        setModalActive={setModalActive}
                    />
                    <p>
                        <strong>Phase:</strong> {node.phase.title}
                    </p>
                    <Categories
                        nodeId={node.id}
                        timelineId={timelineId}
                        categories={categories}
                        onUpdateCategories={setCategories}
                        setModalActive={setModalActive}
                    />
                    <Tags
                        nodeId={node.id}
                        timelineId={timelineId}
                        tags={tags}
                        onUpdateTags={setTags}
                        setModalActive={setModalActive}
                    />
                    <Note
                        nodeId={node.id}
                        timelineId={timelineId}
                        notes={notes} 
                        setNotes={setNotes}
                        onToggle={onToggle}
                    />
                    <Reminder
                        nodeId={node.id}
                        timelineId={timelineId}
                        onToggle={onToggle}
                    />
                    <File
                        nodeId={node.id}
                        timelineId={timelineId}
                        onToggle={onToggle}
                    />
                </div>
            )}
        </div>
    );
});

export default Node;
