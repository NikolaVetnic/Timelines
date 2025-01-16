import React from "react";

import "./styles/App.css";

import Timeline from "./components/Timeline/Timeline";
import TimelineSelect from "./components/TimelineSelect/TimelineSelect";

function App() {
    const handleTimelineSelect = (selectedTimeline) => {
        // console.log("Selected Timeline:", selectedTimeline);
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
