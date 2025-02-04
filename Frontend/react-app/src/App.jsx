import React, { useCallback, useEffect, useState } from "react";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

import Timeline from "./components/Timelines/Timeline/Timeline";
import TimelineSelect from "./components/Timelines/TimelineSelect/TimelineSelect";
import useReminders from "./core/hooks/Reminders/UseReminders";
import useTimelineData from "./core/hooks/UseTimelineData";
import "./styles/App.css";

function App() {
    const [timelineData, setTimelineData] = useTimelineData();
    const [selectedTimeline, setSelectedTimeline] = useState(null);
    useReminders(timelineData);

    const handleTimelineSelect = (selectedOption) => {
        const foundTimeline = timelineData.find(timeline => timeline.id === selectedOption.value);
        setSelectedTimeline(foundTimeline || null);
    };

    const updateSelectedTimeline = useCallback(() => {
        setSelectedTimeline((prevSelected) => {
            if (!prevSelected) return null;
            return timelineData.find(t => t.id === prevSelected.id) || null;
        });
    }, [timelineData]);

    useEffect(() => {
        updateSelectedTimeline();
    }, [updateSelectedTimeline]);

    return (
        <div className="app-container">
            <ToastContainer />
            <div className="app-content">
                <TimelineSelect onTimelineSelect={handleTimelineSelect} />
                <Timeline 
                    selectedTimeline={selectedTimeline} 
                    setSelectedTimeline={setSelectedTimeline} 
                    setTimelineData={setTimelineData} 
                    timelineData={timelineData}
                    updateSelectedTimeline={updateSelectedTimeline}
                />
            </div>
        </div>
    );
}

export default App;
