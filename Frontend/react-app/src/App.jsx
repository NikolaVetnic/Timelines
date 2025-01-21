import React, { useState } from "react";

import Timeline from "./components/Timelines/Timeline/Timeline";
import TimelineSelect from "./components/Timelines/TimelineSelect/TimelineSelect";
import timelineData from "./data/timelineData";
import "./styles/App.css";

function App() {
    const [selectedTimeline, setSelectedTimeline] = useState(null);

    const handleTimelineSelect = (selectedOption) => {
        const foundTimeline = timelineData.find(timeline => timeline.id === selectedOption.value);
        setSelectedTimeline(foundTimeline);
    };

    return (
        <div className="app-container">
            <div className="app-content">
                <TimelineSelect onTimelineSelect={handleTimelineSelect} />
                <Timeline selectedTimeline={selectedTimeline} />
            </div>
        </div>
    );
}

export default App;
