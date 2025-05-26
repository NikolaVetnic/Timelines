import { CiEdit } from "react-icons/ci";
import Button from "../../buttons/Button/Button";

const TimelineList = ({
  timelines,
  selectedTimelines,
  isLoading,
  handleTimelineClick,
  toggleTimelineSelection,
  handleEditTimeline
}) => {
  return (
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
  );
};

export default TimelineList;
