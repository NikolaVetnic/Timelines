import React, { useEffect, useState } from "react";
import { CiEdit } from "react-icons/ci";

import IconButton from "../../../../core/components/buttons/IconButton/IconButton";
import InputStringModal from "../../../../core/components/modals/InputStringModal/InputStringModal";
import convertStringToColor from "../../../../core/utils/ConvertStringToColor";
import { LOCAL_STORAGE_KEY } from "../../../../data/constants";

import "./Tags.css";

const Tags = ({ timelineId, nodeId, setModalActive }) => {
    const root = "tags";
    const [isModalOpen, setModalOpen] = useState(false);
    const [localTags, setLocalTags] = useState([]);

    // todo: connect to backend
    useEffect(() => {
        try {
            const storedData = JSON.parse(localStorage.getItem(LOCAL_STORAGE_KEY)) || [];
            const timeline = storedData.find(t => t.id === timelineId);
            const node = timeline?.nodes.find(n => n.id === nodeId);
            if (node?.tags) {
                setLocalTags(node.tags);
            }
        } catch (error) {
            console.error("Error loading tags:", error);
        }
    }, [timelineId, nodeId]);

    const setModalState = (isActive) => {
        setModalOpen(isActive);
        setModalActive(isActive);
    };

    // todo: connect to backend
    const updateLocalStorage = (newTags) => {
        try {
            const storedData = JSON.parse(localStorage.getItem(LOCAL_STORAGE_KEY)) || [];
            const timelineIndex = storedData.findIndex(t => t.id === timelineId);

            if (timelineIndex !== -1) {
                const nodeIndex = storedData[timelineIndex].nodes.findIndex(n => n.id === nodeId);
                if (nodeIndex !== -1) {
                    storedData[timelineIndex].nodes[nodeIndex].tags = newTags;
                    localStorage.setItem(LOCAL_STORAGE_KEY, JSON.stringify(storedData));
                }
            }
        } catch (error) {
            console.error("Error saving tags:", error);
        }
    };

    const handleSaveTags = (newTags) => {
        const formattedTags = newTags.map(tag => tag.trim().toLowerCase().replace(/\s+/g, "-"));
    
        setLocalTags(formattedTags);
        updateLocalStorage(formattedTags);
        setModalActive(false);
    };
    
    return (
        <div className={`${root}-container`}>
            <div>
                <strong>Tags:</strong>{" "}
                {localTags.length > 0 ? (
                    localTags.map((tag, index) => (
                        <span
                            key={index}
                            className={`${root}-badge`}
                            style={{ backgroundColor: convertStringToColor(tag) }}
                        >
                            {tag}
                        </span>
                    ))
                ) : (
                    <span>No Tags Set</span>
                )}
            </div>
            <IconButton onClick={() => setModalState(true)} icon={<CiEdit />} title="Edit" />
            <InputStringModal
                isOpen={isModalOpen}
                onClose={() => setModalState(false)}
                onSave={handleSaveTags}
                initialValue={localTags.join(", ")}
                title="Edit Tags"
            />
        </div>
    );
};

export default Tags;
