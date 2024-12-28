import React from "react";
import "./Timeline.css";
import TimelineCard from "../TimelineCard/TimelineCard";
import timelineData from "../../data/timelineData";

const Timeline = () => {
    return (
        <div className="timeline-container">
            <div className="timeline-strip"></div>
            <div className="timeline-cards">
                {timelineData.map((item) => (
                    <TimelineCard key={item.id} item={item} />
                ))}
            </div>
        </div>
    );
};

export default Timeline;
