import React, { useCallback, useEffect, useState } from "react";
import Timeline from "../../Timelines/Timeline/Timeline";
import TimelineSelect from "../../Timelines/TimelineSelect/TimelineSelect";
import useReminders from "../../../core/hooks/Reminders/UseReminders";
import useTimelineData from "../../../core/hooks/UseTimelineData";

function TimelinePage() {
    const [timelineData, setTimelineData] = useTimelineData();
    const [selectedTimeline, setSelectedTimeline] = useState(null);
    useReminders(timelineData);

    const handleTimelineSelect = (selectedOption) => {
        const foundTimeline = timelineData.find(
            (timeline) => timeline.id === selectedOption.value
        );
        setSelectedTimeline(foundTimeline || null);
    };

    const updateSelectedTimeline = useCallback(() => {
        setSelectedTimeline((prevSelected) => {
            if (!prevSelected) return null;
            return timelineData.find((t) => t.id === prevSelected.id) || null;
        });
    }, [timelineData]);

    useEffect(() => {
        updateSelectedTimeline();
    }, [updateSelectedTimeline]);

    return (
        <div>
            <TimelineSelect onTimelineSelect={handleTimelineSelect} />
            <Timeline
                selectedTimeline={selectedTimeline}
                setSelectedTimeline={setSelectedTimeline}
                setTimelineData={setTimelineData}
                timelineData={timelineData}
                updateSelectedTimeline={updateSelectedTimeline}
            />
        </div>
    );
}

export default TimelinePage;
