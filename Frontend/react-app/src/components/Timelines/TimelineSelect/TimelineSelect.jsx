import React, { useEffect, useState } from "react";
import Select from "react-select";
import "./TimelineSelect.css";

const TimelineSelect = ({ onTimelineSelect }) => {
    const [selectedTimeline, setSelectedTimeline] = useState(null);
    const [timelineOptions, setTimelineOptions] = useState([]);

    // todo: connect to backend
    useEffect(() => {
        const storedData = localStorage.getItem("timelineData");
        if (storedData) {
            const parsedData = JSON.parse(storedData, (key, value) =>
                key.includes("timestamp") || key.includes("startDate") ? new Date(value) : value
            );

            const options = parsedData.map((timeline) => ({
                value: timeline.id,
                label: timeline.title,
            }));
            setTimelineOptions(options);
        }
    }, []);

    const handleTimelineChange = (selectedOption) => {
        setSelectedTimeline(selectedOption);
        if (onTimelineSelect) {
            onTimelineSelect(selectedOption);
        }
    };

    return (
        <div className="dropdown-container">
            <Select
                options={timelineOptions}
                value={selectedTimeline}
                onChange={handleTimelineChange}
                placeholder="Select a Timeline"
                className="timeline-dropdown"
                classNamePrefix="react-select"
                components={{
                    DropdownIndicator: () => null,
                    IndicatorSeparator: () => null,
                }}
                isSearchable={false}
                styles={{
                    control: (base) => ({
                        ...base,
                        backgroundColor: "transparent",
                        border: "none",
                        boxShadow: "none",
                        justifyContent: "center",
                        cursor: "pointer",
                    }),
                    singleValue: (base) => ({
                        ...base,
                        textAlign: "center",
                        color: "var(--enmGray)",
                    }),
                    placeholder: (base) => ({
                        ...base,
                        textAlign: "center",
                        color: "var(--enmGray)",
                    }),
                    option: (base, state) => ({
                        ...base,
                        textAlign: "center",
                        color: state.isSelected ? "#fff" : "var(--enmGray)",
                    }),
                }}
            />
        </div>
    );
};

export default TimelineSelect;
