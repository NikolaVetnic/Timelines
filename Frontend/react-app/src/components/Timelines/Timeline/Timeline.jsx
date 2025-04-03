import React, { useEffect, useRef, useState } from "react";
import { useParams } from "react-router";
import recalculateStrip from "../../../core/utils/RecalculateStrip";
import TimelineService from "../../../services/TimelineService";
import Node from "../../Nodes/Node/Node/Node";
import "./Timeline.css";

const Timeline = () => {
    const { id } = useParams();
    const [timeline, setTimeline] = useState(null);
    const stripRef = useRef(null);
    const nodesRef = useRef([]);
    const [stripStyle, setStripStyle] = useState({});
    const [updateStrip, setUpdateStrip] = useState(false);
    const [nodesRendered, setNodesRendered] = useState(false);
    const [isModalActive, setModalActive] = useState(false);
    const [openNodeId, setOpenNodeId] = useState(null);
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
        const fetchTimeline = async () => {
            try {
                const response = await TimelineService.getTimelineById(id);
                setTimeline(response);
            } catch (error) {
                console.error("Error fetching timeline:", error);
            } finally {
                setIsLoading(false);
            }
        };

        fetchTimeline();
    }, [id]);

    useEffect(() => {
        if (!timeline) return;
        
        nodesRef.current = [];
        setNodesRendered(false);
        setStripStyle({});
        setUpdateStrip(prev => !prev);
    }, [timeline]);

    useEffect(() => {
        if (nodesRendered && nodesRef.current.length > 0) {
            const newStyle = recalculateStrip(nodesRef);
            setStripStyle(newStyle);
        }
    }, [nodesRendered, timeline, updateStrip]);

    if (isLoading) {
        return <div className="loading-message">Loading timeline...</div>;
    }

    if (!timeline) {
        return <div className="error-message">Timeline not found</div>;
    }

    return (
        <div className="timeline-container" key={timeline.id}>
            <div className="timeline-header">
                <h2 className="timeline-title">{timeline.title}</h2>
                <div className="timeline-header-corner">
                    <span className="timeline-corner-title">{timeline.title}</span>
                </div>
            </div>
            
            {timeline.nodes && timeline.nodes.length > 0 ? (
                <>
                    <div className="timeline-strip" ref={stripRef} style={stripStyle}></div>
                    <div className="timeline-nodes">
                        {timeline.nodes.map((item, index) => (
                            <Node
                                key={item.id}
                                item={item}
                                ref={(el) => {
                                    if (el) {
                                        nodesRef.current[index] = el;
                                    }
                                    if (index === timeline.nodes.length - 1) {
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
                </>
            ) : (
                <div className="empty-nodes-message">
                    <p>This timeline doesn't have any nodes yet.</p>
                    <button className="add-node-button">Add Node</button>
                </div>
            )}
        </div>
    );  
};

export default Timeline;
