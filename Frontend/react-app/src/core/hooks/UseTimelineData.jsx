import { useCallback, useEffect, useState } from "react";

import initialTimelineData from "../../data/timelineData";

// todo: connect to backend
const getStoredTimelineData = () => {
    try {
        const savedData = localStorage.getItem("timelineData");
        return savedData ? JSON.parse(savedData) : null;
    } catch (error) {
        console.error("Error parsing timelineData from localStorage:", error);
        localStorage.removeItem("timelineData");
        return null;
    }
};

// todo: connect to backend
const saveTimelineData = (data) => {
    if (!data) return;

    try {
        const serializedData = JSON.stringify(data, (key, value) =>
            value instanceof Date ? value.toISOString() : value
        );
        localStorage.setItem("timelineData", serializedData);
    } catch (error) {
        console.error("Error saving timelineData to localStorage:", error);
    }
};

// todo: connect to backend
const useTimelineData = () => {
    const [timelineData, setTimelineData] = useState([]);

    const syncTimelineData = useCallback(() => {
        setTimelineData((prevData) => {
            const storedData = getStoredTimelineData();
            if (storedData) return storedData;

            const newData = prevData.length ? prevData : initialTimelineData;
            saveTimelineData(newData);
            return newData;
        });
    }, []);

    useEffect(() => {
        syncTimelineData();
    }, [syncTimelineData]);

    useEffect(() => {
        if (timelineData.length > 0) {
            saveTimelineData(timelineData);
        }
    }, [timelineData]);

    return [timelineData, setTimelineData];
};

export default useTimelineData;
