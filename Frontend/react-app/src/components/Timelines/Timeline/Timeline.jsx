import React, { useEffect, useRef, useState } from "react";

import TimelineHeader from "../../../core/components/headers/TimelineHeader.jsx/TimelineHeader";
import CreateNodeModal from "../../../core/components/modals/CreateNodeModal/CreateNodeModal";
import recalculateStrip from "../../../core/utils/RecalculateStrip";
import Node from "../../Nodes/Node/Node/Node";

import "./Timeline.css";

const Timeline = ({ selectedTimeline, setSelectedTimeline, setTimelineData, timelineData, updateSelectedTimeline }) => {
    const root = "timeline";
    const stripRef = useRef(null);
    const nodesRef = useRef([]);
    const [stripStyle, setStripStyle] = useState({});
    const [updateStrip, setUpdateStrip] = useState(false);
    const [nodesRendered, setNodesRendered] = useState(false);
    const [isModalActive, setModalActive] = useState(false);
    const [openNodeId, setOpenNodeId] = useState(null);

    const [isCreateModalActive, setIsCreateModalActive] = useState(false);

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
        <div className={`${root}-container`} key={selectedTimeline.id}>
            <TimelineHeader root={root} selectedTimeline={selectedTimeline} setIsCreateModalActive={setIsCreateModalActive} />
    
            <div className={`${root}-strip`} ref={stripRef} style={stripStyle}></div>
    
            <div className={`${root}-nodes`}>
                {selectedTimeline.nodes.map((node, index) => (
                    <Node
                        timelineId={selectedTimeline.id}
                        key={node.id}
                        node={node}
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
    
            {isCreateModalActive && (
                <CreateNodeModal
                    isOpen={isCreateModalActive}
                    onClose={() => setIsCreateModalActive(false)}
                    selectedTimeline={selectedTimeline}
                    setTimelineData={setTimelineData}
                    timelineData={timelineData}
                    updateSelectedTimeline={updateSelectedTimeline}
                />
            )}
        </div>
    );
};

export default Timeline;
