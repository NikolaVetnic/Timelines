import React, { useState } from "react";
import "./TimelineCard.css";

const TimelineCard = ({ item }) => {
    const [isOpen, setIsOpen] = useState(false);

    const toggleCard = () => {
        setIsOpen(!isOpen);
    };

    return (
        <div className={`timeline-card ${isOpen ? "open" : ""}`}>
            <div className="card-header" onClick={toggleCard}>
                <h3>{item.title}</h3>
                <span>{isOpen ? "-" : "+"}</span>
            </div>
            {isOpen && (
                <div className="card-content">
                    <p>{item.description}</p>
                    {item.image && <img src={item.image} alt={item.title} />}
                </div>
            )}
        </div>
    );
};

export default TimelineCard;
