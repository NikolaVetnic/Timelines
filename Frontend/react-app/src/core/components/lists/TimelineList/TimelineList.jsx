const TimelineListContent = ({
  timelines,
  selectedTimelines,
  handleTimelineClick,
  toggleTimelineSelection,
  isMobile,
  loadingMore,
  allTimelinesLoaded,
  currentPage,
  totalPages,
  loadMoreRef,
}) => {
  return timelines.length === 0 ? (
    <div className="timeline-list-empty">
      <p className="timeline-list-empty-message">
        No timelines found. Create one to get started!
      </p>
    </div>
  ) : (
    <>
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
            <small className="timeline-list-item-date">
              Created: {new Date(timeline.createdAt).toLocaleDateString()}
            </small>
          </div>
        ))}
      </div>
      {isMobile && (
        <div ref={loadMoreRef} className="load-more-trigger">
          {loadingMore ? (
            <div className="loading-spinner">Loading...</div>
          ) : (
            !allTimelinesLoaded &&
            currentPage < totalPages && <div>Scroll to load more</div>
          )}
        </div>
      )}
    </>
  );
};

export default TimelineListContent;
