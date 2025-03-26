import React from "react";

const TimelineHeader = ({ root, selectedTimeline, setIsCreateModalActive }) => {
  return (
    <div className={`${root}-header`}>
      <h2 className={`${root}-title`}>{selectedTimeline.title}</h2>
      <button className={`${root}-add-node-btn`} onClick={() => setIsCreateModalActive(true)}>
        ➕ Add Node
      </button>
    </div>
  );
};

export default TimelineHeader;
