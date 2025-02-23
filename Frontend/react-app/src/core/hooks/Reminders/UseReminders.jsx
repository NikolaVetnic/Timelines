import { useEffect, useState } from "react";

import { DAY } from "../../../data/constants";
import checkAndScheduleReminders from "../../utils/CheckAndScheduleReminders";

const useReminders = (timelineData) => {
    const [reminders, setReminders] = useState([]);

    useEffect(() => {
        if (timelineData.length > 0) {
            checkAndScheduleReminders(setReminders);
        }
    }, [timelineData]);

    useEffect(() => {
        const now = new Date();
        const timeUntilMidnight =
            new Date(now.getFullYear(), now.getMonth(), now.getDate() + 1, 0, 0, 0) - now;

        const updateReminders = () => {
            checkAndScheduleReminders(setReminders);
            setInterval(() => checkAndScheduleReminders(setReminders), DAY);
        };
        
        const midnightTimeout = setTimeout(updateReminders, timeUntilMidnight);

        return () => clearTimeout(midnightTimeout);
    }, []);

    return reminders;
};

export default useReminders;
