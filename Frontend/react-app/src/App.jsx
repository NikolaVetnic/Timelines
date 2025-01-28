import React, { useEffect, useState } from "react";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

import Timeline from "./components/Timelines/Timeline/Timeline";
import TimelineSelect from "./components/Timelines/TimelineSelect/TimelineSelect";
import { checkReminders } from "./core/utils/DisplayReminder";
import timelineData from "./data/timelineData";
import "./styles/App.css";

const saveTimelineData = () => {
    if (!localStorage.getItem('timelineData')) {
        const serializedData = JSON.stringify(timelineData, (key, value) =>
            value instanceof Date ? value.toISOString() : value
        );
        localStorage.setItem('timelineData', serializedData);
        console.log('Timeline data initialized in localStorage');
    }
};

function App() {
    const [selectedTimeline, setSelectedTimeline] = useState(null);
    const [reminders, setReminders] = useState([]);

    const handleTimelineSelect = (selectedOption) => {
        const foundTimeline = timelineData.find(timeline => timeline.id === selectedOption.value);
        setSelectedTimeline(foundTimeline ? { ...foundTimeline, nodes: [...foundTimeline.nodes] } : null);
    };

    useEffect(() => {
        saveTimelineData();
        const savedReminders = JSON.parse(localStorage.getItem("reminders")) || [];
        setReminders(savedReminders);
    }, []);

    useEffect(() => {
        const interval = setInterval(() => checkReminders(reminders, setReminders), 5000);
        return () => clearInterval(interval);
    }, [reminders]);

    return (
        <div className="app-container">
            <ToastContainer />
            <div className="app-content">
                <TimelineSelect onTimelineSelect={handleTimelineSelect} />
                <Timeline selectedTimeline={selectedTimeline} />
            </div>
        </div>
    );
}

export default App;
