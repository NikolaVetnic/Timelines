import React, { useCallback, useEffect, useRef, useState } from "react";
import { FaArrowLeft } from "react-icons/fa6";
import { FaTrash } from "react-icons/fa";
import { PiSelectionAllFill, PiSelectionAll } from "react-icons/pi";
import { useNavigate, useParams } from "react-router";
import CreateNodeModal from "../../../core/components/modals/CreateNodeModal/CreateNodeModal";
import Button from "../../../core/components/buttons/Button/Button";
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
  const [selectedNodes, setSelectedNodes] = useState([]);

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

  const handleSelectNode = (nodeId) => {
    setSelectedNodes((prev) => [...prev, nodeId]);
  };

  const handleDeselectNode = (nodeId) => {
    setSelectedNodes((prev) => prev.filter((id) => id !== nodeId));
  };

  const toggleSelectAll = () => {
    if (selectedNodes.length === timeline?.nodes?.length) {
      setSelectedNodes([]);
    } else {
      setSelectedNodes(timeline?.nodes?.map((node) => node.id) || []);
    }
  };

  const handleDeleteSelected = async () => {
    // todo: connecto to a service
    console.log("Deleting nodes:", selectedNodes);
    setSelectedNodes([]);
  };

  if (isLoading) {
    return <div className="loading-message">Loading timeline...</div>;
  }

  if (!timeline) {
    return <div className="error-message">Timeline not found</div>;
  }

  return (
    <div className="timeline-container" key={timeline.id}>
      <button className="back-button" onClick={() => navigate(-1)}>
        <FaArrowLeft />
      </button>

      <div className="timeline-header">
        <h2 className="timeline-title">{timeline.title}</h2>
      </div>

      <div className="timeline-actions">
        {timeline?.nodes?.length > 0 && (
          <>
            {selectedNodes.length > 0 && (
              <Button
                icon={<FaTrash />}
                iconOnly
                onClick={handleDeleteSelected}
                variant="danger"
                size="small"
              />
            )}
            <Button
              icon={
                selectedNodes.length === timeline.nodes.length ? (
                  <PiSelectionAll />
                ) : (
                  <PiSelectionAllFill />
                )
              }
              iconOnly
              onClick={toggleSelectAll}
              variant="secondary"
              size="small"
            />
          </>
        )}
        <Button
          text="Add Node"
          onClick={() => setShowCreateModal(true)}
          variant="success"
          size="small"
        />
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
                isSelected={selectedNodes.includes(node.id)}
                onSelect={handleSelectNode}
                onDeselect={handleDeselectNode}
                onToggleSelectAll={toggleSelectAll}
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
