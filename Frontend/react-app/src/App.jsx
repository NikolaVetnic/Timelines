import React, { useState } from "react";
import Timeline from "./components/Timelines/Timeline/Timeline";
import TimelineSelect from "./components/Timelines/TimelineSelect/TimelineSelect";
import CreateTimelineModal from "./core/components/modals/CreateTimelineModal/CreateTimelineModal";
import timelineData from "./data/timelineData";
import "./styles/App.css";

function App() {
    const [selectedTimeline, setSelectedTimeline] = useState(null);
    const [isModalOpen, setIsModalOpen] = useState(false);

    const handleTimelineSelect = (selectedOption) => {
        const foundTimeline = timelineData.find(timeline => timeline.id === selectedOption.value);
        setSelectedTimeline(foundTimeline ? { ...foundTimeline, nodes: [...foundTimeline.nodes] } : null);
    };

    const handleOpenModal = () => {
        setIsModalOpen(true);
    };

    const handleCloseModal = () => {
        setIsModalOpen(false);
    };

    return (
        <div className="app-container">
            <div className="app-content">
                <TimelineSelect onTimelineSelect={handleTimelineSelect} />
                <button onClick={handleOpenModal}>Create Timeline</button>
                <Timeline selectedTimeline={selectedTimeline} />
            </div>

            {isModalOpen && <CreateTimelineModal onClose={handleCloseModal} />}
        </div>
    );
}

export default App;
