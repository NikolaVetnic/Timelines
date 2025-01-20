import React from "react";

import Timeline from "./components/Timelines/Timeline/Timeline";
import TimelineSelect from "./components/Timelines/TimelineSelect/TimelineSelect";
import "./styles/App.css";

function App() {
    const handleTimelineSelect = (selectedTimeline) => {
    };

    return (
        <div className="app-container">
            <div className="app-content">
                <TimelineSelect onTimelineSelect={handleTimelineSelect} />
                <Timeline />
            </div>
        </div>
    );
}

export default App;
