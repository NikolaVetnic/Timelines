import React, { useEffect, useRef, useState } from "react";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

import Timeline from "./components/Timelines/Timeline/Timeline";
import TimelineSelect from "./components/Timelines/TimelineSelect/TimelineSelect";
import { checkAndScheduleReminders } from "./core/utils/ScheduledReminders";
import timelineData from "./data/timelineData";
import "./styles/App.css";

const saveTimelineData = () => {
    if (!localStorage.getItem("timelineData")) {
        const serializedData = JSON.stringify(timelineData, (key, value) =>
            value instanceof Date ? value.toISOString() : value
        );
        localStorage.setItem("timelineData", serializedData);
        console.log("📂 Timeline data initialized in localStorage");
    }
};

function App() {
    const [selectedTimeline, setSelectedTimeline] = useState(null);
    const [reminders, setReminders] = useState([]);
    const remindersRef = useRef([]);

    const extractRemindersFromNodes = (data) => {
        return data.flatMap((timeline) =>
            timeline.nodes?.flatMap((node) => node.reminders || []) || []
        );
    };

    const handleTimelineSelect = (selectedOption) => {
        const foundTimeline = timelineData.find((timeline) => timeline.id === selectedOption.value);
        setSelectedTimeline(foundTimeline ? { ...foundTimeline, nodes: [...foundTimeline.nodes] } : null);
    };

    useEffect(() => {
        saveTimelineData();
        const savedTimelineData = JSON.parse(localStorage.getItem("timelineData")) || [];

        const extractedReminders = extractRemindersFromNodes(savedTimelineData);
        setReminders(extractedReminders);
        remindersRef.current = extractedReminders;
    }, []);

    useEffect(() => {
        const interval = setInterval(() => {
            checkAndScheduleReminders(remindersRef.current, setReminders);
        }, 60000);
        
        return () => clearInterval(interval);
    }, []);

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
