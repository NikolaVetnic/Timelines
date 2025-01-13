import React, { useEffect, useRef, useState } from "react";

import recalculateStrip from "../../core/utils/RecalculateStrip";
import timelineData from "../../data/timelineData";
import TimelineCard from "../TimelineCard/TimelineCard";
import "./Timeline.css";

const Timeline = () => {
    const stripRef = useRef(null);
    const cardsRef = useRef([]);
    const [stripStyle, setStripStyle] = useState({});
    const [updateStrip, setUpdateStrip] = useState(false);

    useEffect(() => {
        const newStyle = recalculateStrip(cardsRef);
        setStripStyle(newStyle);
    }, [updateStrip]);

    return (
        <div className="timeline-container">
            <div
                className="timeline-strip"
                ref={stripRef}
                style={stripStyle}
            ></div>
            <div className="timeline-cards">
                {timelineData.map((item, index) => (
                    <TimelineCard
                        key={item.id}
                        item={item}
                        ref={(el) => (cardsRef.current[index] = el)}
                        onToggle={() => setUpdateStrip(!updateStrip)}
                    />
                ))}
            </div>
        </div>
    );
};

export default Timeline;
