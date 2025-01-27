import React, { useEffect, useRef, useState } from "react";
import recalculateStrip from "../../../core/utils/RecalculateStrip";
import Node from "../../Nodes/Node/Node/Node";
import "./Timeline.css";

const Timeline = ({ selectedTimeline }) => {
    const stripRef = useRef(null);
    const nodesRef = useRef([]);
    const [stripStyle, setStripStyle] = useState({});
    const [updateStrip, setUpdateStrip] = useState(false);
    const [nodesRendered, setNodesRendered] = useState(false);
    const [isModalActive, setModalActive] = useState(false);
    const [openNodeId, setOpenNodeId] = useState(null);

    useEffect(() => {
        nodesRef.current = [];
        setNodesRendered(false);
        setStripStyle({});
        setUpdateStrip(prev => !prev);
    }, [selectedTimeline]);

    useEffect(() => {
        if (nodesRendered && nodesRef.current.length > 0) {
            const newStyle = recalculateStrip(nodesRef);
            setStripStyle(newStyle);
        }
    }, [nodesRendered, selectedTimeline, updateStrip]);

    if (!selectedTimeline) {
        return <p>Please select a timeline.</p>;
    }

    return (
        <div className="timeline-container" key={selectedTimeline.id}>
            <h2>{selectedTimeline.title}</h2>
            <div className="timeline-strip" ref={stripRef} style={stripStyle}></div>
            <div className="timeline-nodes">
                {selectedTimeline.nodes.map((item, index) => (
                    <Node
                        key={item.id}
                        item={item}
                        ref={(el) => {
                            if (el) {
                                nodesRef.current[index] = el;
                            }
                            if (index === selectedTimeline.nodes.length - 1) {
                                setTimeout(() => setNodesRendered(true), 0);
                            }
                        }}
                        isModalActive={isModalActive}
                        setModalActive={setModalActive}
                        openNodeId={openNodeId}
                        setOpenNodeId={setOpenNodeId}
                        onToggle={() => setUpdateStrip(prev => !prev)}
                    />
                ))}
            </div>
        </div>
    );  
};

export default Timeline;
