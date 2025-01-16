import React, { useState } from "react";
import Select from "react-select";

import "./TimelineSelect.css";

const TimelineSelect = ({ onTimelineSelect }) => {
    const [selectedTimeline, setSelectedTimeline] = useState(null);

    const timelineOptions = [
        { value: "timeline1", label: "Timeline 1" },
        { value: "timeline2", label: "Timeline 2" },
        { value: "timeline3", label: "Timeline 3" },
    ];

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
                placeholder="My Timelines"
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
