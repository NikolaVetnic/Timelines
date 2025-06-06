import { useCallback, useEffect, useRef, useState } from "react";
import { FaClone, FaTrash } from "react-icons/fa";
import { FaArrowLeft } from "react-icons/fa6";
import { IoMdAdd } from "react-icons/io";
import { PiSelectionAll, PiSelectionAllFill } from "react-icons/pi";
import { useNavigate, useParams } from "react-router";
import { Outlet, useMatches } from "react-router-dom";
import Button from "../../../core/components/buttons/Button/Button";
import CreateNodeModal from "../../../core/components/modals/CreateNodeModal/CreateNodeModal";
import CreateTimelineModal from "../../../core/components/modals/CreateTimelineModal/CreateTimelineModal";
import DeleteModal from "../../../core/components/modals/DeleteModal/DeleteModal";
import recalculateStrip from "../../../core/utils/RecalculateStrip";
import NodeService from "../../../services/NodeService";
import TimelineService from "../../../services/TimelineService";
import Node from "../../Nodes/Node/Node/Node";
import PhysicalPersonPanel from "../PhysicalPerson/PhysicalPerson";
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
  const [showDeleteModal, setDeleteModal] = useState(false);
  const [selectedNodes, setSelectedNodes] = useState([]);
  const [nodesToDelete, setNodesToDelete] = useState([]);
  const [isMobile, setIsMobile] = useState(false);
  const [showCloneModal, setShowCloneModal] = useState(false);

  const matches = useMatches();
  const parentMatch = matches[matches.length - 2];  

  const isDetailsView = matches.some(match => 
    match.pathname.includes('physical-persons')
  );

  const fetchTimeline = useCallback(async () => {
    const response = await TimelineService.getTimelineById(id);
    setTimeline(response);
    setIsLoading(false);
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
    const checkIfMobile = () => {
      setIsMobile(window.width <= 768);
    };
    
    checkIfMobile();
    window.addEventListener('resize', checkIfMobile);
    return () => window.removeEventListener('resize', checkIfMobile);
  }, []);

  useEffect(() => {
    if (nodesRendered && nodesRef.current.length > 0) {
      const newStyle = recalculateStrip(nodesRef, isMobile);
      setStripStyle(newStyle);
    }
  }, [nodesRendered, timeline, updateStrip, isMobile]);

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

  const handleDeleteSelected = () => {
    setNodesToDelete(selectedNodes);
    setDeleteModal(true);
  };

  const confirmDeleteNodes = async () => {
    try {
      setIsLoading(true);
      await Promise.all(
        nodesToDelete.map((nodeId) => NodeService.deleteNode(nodeId))
      );
      setSelectedNodes([]);
      setNodesToDelete([]);
      fetchTimeline();
    } finally {
      setIsLoading(false);
      setDeleteModal(false);
    }
  };

  if (isLoading) {
    return <div className="loading-message">Loading timeline...</div>;
  }

  if (!timeline) {
    return <div className="error-message">Timeline not found</div>;
  }

  return (
    <>
      {!isDetailsView ? (
               <div className="timeline-container" key={timeline.id}>
      <div className="timeline-back-button-container">
        <Button
          className="back-button"
          icon={<FaArrowLeft />}
          iconOnly
          noBackground
          onClick={() => {
            if (parentMatch?.pathname) {
              navigate(parentMatch.pathname);
            } else {
              navigate(-1);
            }
          }}
        />
      </div>

      <div className="timeline-header">
        <h2 className="timeline-title">{timeline.title}</h2>
      </div>

      <div className="timeline-actions">
        {selectedNodes.length > 0 && (
              <Button
                icon={<FaTrash />}
                iconOnly
                onClick={handleDeleteSelected}
                variant="danger"
                tooltip="Delete Selected"
                size="small"
              />
            )}
            
        {timeline?.nodes?.length > 0 && (
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
              tooltip="Select All"
              size="small"
            />
        )}
        <Button
          icon={<FaClone />}
          iconOnly
          onClick={() => setShowCloneModal(true)}
          variant="primary"
          size="small"
          tooltip="Clone this timeline"
        />
        <Button
          icon={<IoMdAdd />}
          iconOnly
          onClick={() => setShowCreateModal(true)}
          variant="success"
          tooltip="Add Node"
          size="small"
        />
      </div>

      <CreateNodeModal
        isOpen={showCreateModal}
        onClose={() => setShowCreateModal(false)}
        timelineId={id}
        onNodeCreated={fetchTimeline}
      />

      <CreateTimelineModal
        isOpen={showCloneModal}
        onClose={() => setShowCloneModal(false)}
        onTimelineCreated={() => {
          setShowCloneModal(false);
        }}
        initialTemplate={id}
      />

      <DeleteModal
        isOpen={showDeleteModal}
        onClose={() => setDeleteModal(false)}
        itemType="node"
        onConfirm={confirmDeleteNodes}
        itemTitle={
          selectedNodes.length === 1 
            ? timeline.nodes.find(n => n.id === selectedNodes[0])?.title 
            : ""
        }
        count={selectedNodes.length}
      />

      <PhysicalPersonPanel timelineId={timeline?.id} />

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
      ) : (
        <Outlet />
      )}
    </>
  );
};

export default Timeline;
