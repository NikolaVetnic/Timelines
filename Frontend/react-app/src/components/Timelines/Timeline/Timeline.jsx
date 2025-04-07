import React, { useEffect, useRef, useState, useCallback } from "react";
import { useParams, useNavigate } from "react-router";
import CreateNodeModal from "../../../core/components/modals/CreateNodeModal/CreateNodeModal";
import recalculateStrip from "../../../core/utils/RecalculateStrip";
import TimelineService from "../../../services/TimelineService";
import Node from "../../Nodes/Node/Node/Node";
import "./Timeline.css";

const Timeline = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [timeline, setTimeline] = useState(null);
  const stripRef = useRef(null);
  const nodesRef = useRef([]);
  const [stripStyle, setStripStyle] = useState({});
  const [updateStrip, setUpdateStrip] = useState(false);
  const [nodesRendered, setNodesRendered] = useState(false);
  const [isModalActive, setModalActive] = useState(false);
  const [openNodeId, setOpenNodeId] = useState(null);
  const [isLoading, setIsLoading] = useState(true);
  const [showCreateModal, setShowCreateModal] = useState(false);

  const fetchTimeline = useCallback(async () => {
    try {
      const response = await TimelineService.getTimelineById(id);
      setTimeline(response.timeline);
    } catch (error) {
      console.error("Error fetching timeline:", error);
    } finally {
      setIsLoading(false);
    }
  }, [id]);

  useEffect(() => {
    const handleResize = () => setUpdateStrip((prev) => !prev);
    window.addEventListener("resize", handleResize);
    return () => window.removeEventListener("resize", handleResize);
  }, []);

  useEffect(() => {
    fetchTimeline();
  }, [fetchTimeline]);

  useEffect(() => {
    if (!timeline) return;
    nodesRef.current = [];
    setNodesRendered(false);
    setStripStyle({});
    setUpdateStrip((prev) => !prev);
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
      <button className="back-button" onClick={() => navigate(-1)}>
        &larr; Back
      </button>

      <div className="timeline-header">
        <h2 className="timeline-title">{timeline.title}</h2>
      </div>

      <div className="timeline-actions">
        <button
          className="add-node-button"
          onClick={() => setShowCreateModal(true)}
        >
          + Add Node
        </button>
      </div>

      <CreateNodeModal
        isOpen={showCreateModal}
        onClose={() => setShowCreateModal(false)}
        timelineId={id}
        onNodeCreated={fetchTimeline}
      />

      {timeline.nodes && timeline.nodes.length > 0 ? (
        <div className="timeline-nodes-container">
          {timeline.nodes.length > 1 && (
            <div className="timeline-strip" ref={stripRef} style={stripStyle} />
          )}
          <div className="timeline-nodes">
            {timeline.nodes.map((node, index) => (
              <Node
                key={node.id}
                node={node}
                ref={(el) => {
                  if (el) nodesRef.current[index] = el;
                  if (index === timeline.nodes.length - 1) {
                    setTimeout(() => setNodesRendered(true), 0);
                  }
                }}
                isModalActive={isModalActive}
                setModalActive={setModalActive}
                openNodeId={openNodeId}
                setOpenNodeId={setOpenNodeId}
                onToggle={() => setUpdateStrip((prev) => !prev)}
              />
            ))}
          </div>
        </div>
      ) : (
        <div className="empty-nodes-message">
          <p>This timeline doesn't have any nodes yet.</p>
        </div>
      )}
    </div>
  );
};

export default Timeline;
