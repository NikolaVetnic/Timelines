import React, { useEffect, useState } from "react";
import { CiEdit } from "react-icons/ci";
import { FaTrash } from "react-icons/fa";
import { PiSelectionAll, PiSelectionAllFill } from "react-icons/pi";
import { useNavigate } from "react-router";
import TimelineService from "../../../../services/TimelineService";
import Button from "../../buttons/Button/Button";
import CreateTimelineModal from "../../modals/CreateTimelineModal/CreateTimelineModal";
import DeleteModal from "../../modals/DeleteModal/DeleteModal";
import EditTimelineModal from "../../modals/EditTimelineModal/EditTimelineModal";
import Pagination from "../../pagination/Pagination";
import "./TimelineList.css";

const TimelineList = () => {
  const navigate = useNavigate();
  const [timelines, setTimelines] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  const [itemsPerPage, setItemsPerPage] = useState(10);
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(null);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [selectedTimelines, setSelectedTimelines] = useState([]);
  const [isDeleteModalOpen, setIsDeleteModalOpen] = useState(false);
  const [editModalOpen, setEditModalOpen] = useState(false);
  const [timelineToEdit, setTimelineToEdit] = useState(null);

  const fetchTimelines = async (page = 1, size = 10) => {
    setIsLoading(true);
    setError(null);
    try {
      const response = await TimelineService.getAllTimelines(page - 1, size);
      setTimelines(response.items);
      setTotalPages(response.totalPages);
      setSelectedTimelines([]);
    } catch (error) {
      setError(error.message || "Failed to load timelines");
      setTimelines([]);
      setTotalPages(1);
    } finally {
      setIsLoading(false);
    }
  };

  const handleOpenModal = () => {
    setIsModalOpen(true);
  };

  const handleCloseModal = () => {
    setIsModalOpen(false);
  };

  const handleTimelineCreated = () => {
    setIsModalOpen(false);
    fetchTimelines(currentPage, itemsPerPage);
  };

  useEffect(() => {
    fetchTimelines(currentPage, itemsPerPage);
  }, [currentPage, itemsPerPage]);

  useEffect(() => {
    setSelectedTimelines([]);
  }, [timelines]);

  const handleItemsPerPageChange = (newSize) => {
    const validatedSize = Math.max(5, Math.min(30, newSize));
    setItemsPerPage(validatedSize);
    setCurrentPage(1);
  };

  const handlePageChange = (newPage) => {
    setCurrentPage(newPage);
  };

  const handleRefresh = () => {
    fetchTimelines(currentPage, itemsPerPage);
  };

  const handleTimelineClick = (timeline, e) => {
    if (e.target.closest(".timeline-checkbox")) {
      return;
    }

    navigate(`/timelines/${timeline.id}`);
    setSelectedTimelines([]);
  };

  const toggleTimelineSelection = (timelineId) => {
    setSelectedTimelines((prev) =>
      prev.includes(timelineId)
        ? prev.filter((id) => id !== timelineId)
        : [...prev, timelineId]
    );
  };

  const toggleSelectAll = () => {
    if (selectedTimelines.length === timelines.length) {
      setSelectedTimelines([]);
    } else {
      setSelectedTimelines(timelines.map((timeline) => timeline.id));
    }
  };

  const handleDeleteSelected = async () => {
    setIsLoading(true);
    await Promise.all(
      selectedTimelines.map((id) => TimelineService.deleteTimeline(id))
    );
    setIsDeleteModalOpen(false);
    fetchTimelines(currentPage, itemsPerPage);
    setIsLoading(false);
  };

  const handleEditTimeline = (timeline) => {
    setTimelineToEdit(timeline);
    setEditModalOpen(true);
  };

  if (isLoading) {
    return <div className="loading-state">Loading timelines...</div>;
  }

  if (error) {
    return (
      <div className="error-state">
        {error} <button onClick={handleRefresh}>Retry</button>
      </div>
    );
  }

  return (
    <div className="timeline-list-container">
      <div className="timeline-list-header">
        <h2 className="timeline-list-title">Timelines</h2>
        <div className="timeline-list-actions">
          {selectedTimelines.length > 0 && (
            <Button
              icon={<FaTrash />}
              iconOnly
              variant="danger"
              size="small"
              onClick={() => setIsDeleteModalOpen(true)}
            />
          )}
          {timelines.length > 0 && (
            <Button
              icon={
                selectedTimelines.length === timelines.length ? (
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
          )}
          <Button
            text="Create New Timeline"
            onClick={handleOpenModal}
            variant="success"
          />
        </div>
      </div>

      <div className="timeline-list-content">
        {timelines.length === 0 ? (
          <div className="timeline-list-empty">
            <p className="timeline-list-empty-message">
              No timelines found. Create one to get started!
            </p>
          </div>
        ) : (
          <div className="timeline-list-grid">
            {timelines.map((timeline) => (
              <div
                key={timeline.id}
                className={`timeline-list-item ${
                  selectedTimelines.includes(timeline.id) ? "selected" : ""
                }`}
                onClick={(e) => handleTimelineClick(timeline, e)}
              >
                <div className="timeline-checkbox-container">
                  <input
                    type="checkbox"
                    className="timeline-checkbox"
                    checked={selectedTimelines.includes(timeline.id)}
                    onChange={() => toggleTimelineSelection(timeline.id)}
                    onClick={(e) => e.stopPropagation()}
                  />
                </div>
                <h3 className="timeline-list-item-title">
                  <span>Timeline:</span>
                  {timeline.title}
                </h3>
                <p className="timeline-list-item-description">
                  {timeline.description || "No description"}
                </p>
                {/* <small className="timeline-list-item-date">
                  Created: {new Date(timeline.createdAt).toLocaleDateString()}
                </small> */}
                <div className="timeline-edit-button">
                  <Button
                    icon={<CiEdit />}
                    iconOnly
                    variant="info"
                    shape="square"
                    size="little"
                    disabled={isLoading}
                    onClick={(e) => {
                      e.stopPropagation();
                      handleEditTimeline(timeline);
                    }}
                  />
                </div>
              </div>
            ))}
          </div>
        )}
      </div>

      <div className="timeline-list-pagination-container">
        <Pagination
          currentPage={currentPage}
          totalPages={totalPages}
          itemsPerPage={itemsPerPage}
          onPageChange={handlePageChange}
          onItemsPerPageChange={handleItemsPerPageChange}
        />
      </div>

      {isModalOpen && (
        <CreateTimelineModal
          onClose={handleCloseModal}
          onTimelineCreated={handleTimelineCreated}
        />
      )}

      {editModalOpen && (
        <EditTimelineModal
          timeline={timelineToEdit}
          onClose={() => setEditModalOpen(false)}
          onTimelineUpdated={() => fetchTimelines(currentPage, itemsPerPage)}
        />
      )}

      {isDeleteModalOpen && (
        <DeleteModal
          itemType="timeline"
          isOpen={isDeleteModalOpen}
          onClose={() => setIsDeleteModalOpen(false)}
          onConfirm={handleDeleteSelected}
          count={selectedTimelines.length}
        />
      )}
    </div>
  );
};

export default TimelineList;
