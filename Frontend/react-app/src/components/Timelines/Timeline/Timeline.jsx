import React, { useEffect, useRef, useState } from "react";
import recalculateStrip from "../../../core/utils/RecalculateStrip";
import Node from "../../Nodes/Node/Node/Node";
import "./Timeline.css";

const Timeline = ({ selectedTimeline }) => {
    const stripRef = useRef(null);
    const cardsRef = useRef([]);
    const [stripStyle, setStripStyle] = useState({});
    const [updateStrip, setUpdateStrip] = useState(false);
    const [nodesRendered, setNodesRendered] = useState(false);

    useEffect(() => {
        cardsRef.current = [];
        setNodesRendered(false);
        setStripStyle({});
    }, [selectedTimeline]);

    useEffect(() => {
        if (nodesRendered && cardsRef.current.length > 0) {
            const newStyle = recalculateStrip(cardsRef);
            setStripStyle(newStyle);
        }
    }, [nodesRendered, selectedTimeline, updateStrip]); 

    if (!selectedTimeline) {
        return <p>Please select a timeline.</p>;
    }

    return (
        <div className="timeline-container">
            <h2>{selectedTimeline.title}</h2>
            <div className="timeline-strip" ref={stripRef} style={stripStyle}></div>
            <div className="timeline-cards">
                {selectedTimeline.nodes.map((item, index) => (
                    <Node
                        key={item.id}
                        item={item}
                        ref={(el) => {
                            if (el) {
                                cardsRef.current[index] = el;
                            }
                            if (index === selectedTimeline.nodes.length - 1) {
                                setTimeout(() => setNodesRendered(true), 0);
                            }
                        }}
                        onToggle={() => setUpdateStrip(prev => !prev)}
                    />
                ))}
            </div>
        </div>
    );
};

export default Timeline;
