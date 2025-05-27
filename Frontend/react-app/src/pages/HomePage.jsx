import { useCallback, useEffect, useRef, useState } from "react";
import { FaTrash } from "react-icons/fa";
import { IoMdAdd } from "react-icons/io";
import { PiSelectionAll, PiSelectionAllFill } from "react-icons/pi";
import { useNavigate } from "react-router";
import Button from "../core/components/buttons/Button/Button";
import TimelineList from "../core/components/lists/TimelineList/TimelineList";
import CreateTimelineModal from "../core/components/modals/CreateTimelineModal/CreateTimelineModal";
import DeleteModal from "../core/components/modals/DeleteModal/DeleteModal";
import EditTimelineModal from "../core/components/modals/EditTimelineModal/EditTimelineModal";
import Pagination from "../core/components/pagination/Pagination";
import TimelineService from "../services/TimelineService";
import "./PagesStyle/HomePage.css";

const HomePage = () => {
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
  const [selectedTimelineTitle, setSelectedTimelineTitle] = useState("");

  const [isMobile, setIsMobile] = useState(false);
  const [loadingMore, setLoadingMore] = useState(false);
  const [allTimelinesLoaded, setAllTimelinesLoaded] = useState(false);
  const observer = useRef();
  const loadMoreRef = useRef();

  // Detect mobile view
  useEffect(() => {
    const checkIfMobile = () => {
      setIsMobile(window.innerWidth <= 768);
    };
    checkIfMobile();
    window.addEventListener("resize", checkIfMobile);
    return () => window.removeEventListener("resize", checkIfMobile);
  }, []);

  // Infinite scroll implementation
  useEffect(() => {
    if (!isMobile || loadingMore || allTimelinesLoaded) return;

    const observerCallback = (entries) => {
      const [entry] = entries;
      if (entry.isIntersecting && currentPage < totalPages) {
        loadMoreTimelines();
      }
    };

    const options = {
      root: null,
      rootMargin: "100px",
      threshold: 0.1,
    };

    observer.current = new IntersectionObserver(observerCallback, options);

    if (loadMoreRef.current) {
      observer.current.observe(loadMoreRef.current);
    }

    return () => {
      if (observer.current) {
        observer.current.disconnect();
      }
    };
  }, [isMobile, loadingMore, allTimelinesLoaded, currentPage, totalPages]);

  const loadMoreTimelines = useCallback(async () => {
    if (loadingMore || allTimelinesLoaded || currentPage >= totalPages) return;

    setLoadingMore(true);
    try {
      const nextPage = currentPage + 1;
      const response = await TimelineService.getAllTimelines(
        nextPage - 1,
        itemsPerPage
      );

      if (response.items.length === 0) {
        setAllTimelinesLoaded(true);
        return;
      }

      setTimelines((prev) => [...prev, ...response.items]);
      setCurrentPage(nextPage);
      setTotalPages(response.totalPages);
    } finally {
      setLoadingMore(false);
    }
  }, [currentPage, itemsPerPage, loadingMore, allTimelinesLoaded, totalPages]);

  const fetchTimelines = async (page = 1, size = 10) => {
    setIsLoading(true);
    setError(null);
    setAllTimelinesLoaded(false);
    try {
      const response = await TimelineService.getAllTimelines(page - 1, size);
      if (page === 1) {
        setTimelines(response.items);
      } else {
        setTimelines((prev) => [...prev, ...response.items]);
      }
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

  const handleDeleteClick = async () => {
  if (selectedTimelines.length === 1) {
      const timeline = await TimelineService.getTimelineById(selectedTimelines[0]);
      setSelectedTimelineTitle(timeline.title);
  }
  setIsDeleteModalOpen(true);
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
        <div className="timeline-list-actions">
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
              tooltip="Select All"
              size="small"
            />
          )}
          <Button
            icon={<IoMdAdd />}
            iconOnly
            size="small"
            onClick={handleOpenModal}
            tooltip="Create/Clone timeline"
            variant="success"
          />
        </div>
        {selectedTimelines.length > 0 && (
            <div className="timeline-list-action-delete">
              <Button
                icon={<FaTrash />}
                iconOnly
                variant="danger"
                tooltip="Delete Selected"
                size="small"
                onClick={handleDeleteClick}
              />
            </div>
          )}
      </div>

      <TimelineList
        timelines={timelines}
        selectedTimelines={selectedTimelines}
        isLoading={isLoading}
        handleTimelineClick={handleTimelineClick}
        toggleTimelineSelection={toggleTimelineSelection}
        handleEditTimeline={handleEditTimeline}
      />

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
          isOpen={isModalOpen}
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
          itemTitle={selectedTimelineTitle}
          count={selectedTimelines.length}
        />
      )}
    </div>
  );
};

export default HomePage;
