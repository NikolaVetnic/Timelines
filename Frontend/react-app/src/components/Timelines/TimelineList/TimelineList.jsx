import React, { useEffect, useState } from "react";
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

    const handleTimelineClick = (timeline) => {
        navigate(`/timelines/${timeline.id}`);
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
                <button 
                    className="timeline-list-create-button"
                    onClick={handleOpenModal}
                >
                    Create New Timeline
                </button>
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
                                className="timeline-list-item"
                                onClick={() => handleTimelineClick(timeline)}
                            >
                                <h3 className="timeline-list-item-title">{timeline.title}</h3>
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
        </div>
    );
};

export default TimelineList;
