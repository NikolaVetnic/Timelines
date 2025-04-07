import React, { useEffect, useState } from "react";
import { FaTimes, FaTrash } from "react-icons/fa";
import { useNavigate } from "react-router";
import CreateTimelineModal from "../../../core/components/modals/CreateTimelineModal/CreateTimelineModal";
import Pagination from "../../../core/components/pagination/Pagination";
import TimelineService from "../../../services/TimelineService";
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

    const fetchTimelines = async (page = 1, size = 10) => {
        setIsLoading(true);
        setError(null);
        try {
            const response = await TimelineService.getAllTimelines(
                page - 1,
                size
            );
            
            setTimelines(response.items);
            setTotalPages(response.totalPages);
            setSelectedTimelines([]);
        } catch (error) {
            console.error("Error fetching timelines:", error);
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
        if (e.target.closest('.timeline-checkbox') || selectedTimelines.length > 0) {
            return;
        }
        navigate(`/timelines/${timeline.id}`);
    };

    const toggleTimelineSelection = (timelineId) => {
        setSelectedTimelines(prev => 
            prev.includes(timelineId) 
                ? prev.filter(id => id !== timelineId) 
                : [...prev, timelineId]
        );
    };

    const handleDeleteSelected = async () => {
        setIsLoading(true);
        try {
            await Promise.all(
                selectedTimelines.map(id => TimelineService.deleteTimeline(id))
            );
            setIsDeleteModalOpen(false);
            fetchTimelines(currentPage, itemsPerPage);
        } catch (error) {
            console.error("Error deleting timelines:", error);
            setError("Failed to delete timelines");
        } finally {
            setIsLoading(false);
        }
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
                        <button 
                            className="timeline-list-delete-button"
                            onClick={() => setIsDeleteModalOpen(true)}
                        >
                            <FaTrash /> Delete Selected
                        </button>
                    )}
                    <button 
                        className="timeline-list-create-button"
                        onClick={handleOpenModal}
                    >
                        Create New Timeline
                    </button>
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
                        {timelines.map(timeline => (
                            <div 
                                key={timeline.id} 
                                className={`timeline-list-item ${selectedTimelines.includes(timeline.id) ? 'selected' : ''}`}
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
                                    <span>
                                        Timeline:
                                    </span> 
                                    {timeline.title}
                                </h3>
                                <p className="timeline-list-item-description">
                                    {timeline.description || "No description"}
                                </p>
                                <small className="timeline-list-item-date">
                                    Created: {new Date(timeline.createdAt).toLocaleDateString()}
                                </small>
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

            {isDeleteModalOpen && (
                <div className="delete-confirmation-modal">
                    <div className="delete-confirmation-content">
                        <button 
                            className="close-modal-button"
                            onClick={() => setIsDeleteModalOpen(false)}
                        >
                            <FaTimes />
                        </button>
                        <h3>Confirm Deletion</h3>
                        <p>
                            Are you sure you want to delete {selectedTimelines.length} 
                            {selectedTimelines.length === 1 ? ' timeline' : ' timelines'}?
                            This action cannot be undone.
                        </p>
                        <div className="confirmation-buttons">
                            <button 
                                className="cancel-button"
                                onClick={() => setIsDeleteModalOpen(false)}
                            >
                                Cancel
                            </button>
                            <button 
                                className="confirm-delete-button"
                                onClick={handleDeleteSelected}
                            >
                                Delete
                            </button>
                        </div>
                    </div>
                </div>
            )}
        </div>
    );
};

export default TimelineList;
