import { useEffect, useRef, useState } from "react";
import checkAndScheduleReminders from "../../utils/CheckAndScheduleReminders";

const useReminders = (timelineData) => {
    const [reminders, setReminders] = useState([]);
    const remindersRef = useRef([]);

    useEffect(() => {
        if (timelineData.length > 0) {
            const extractedReminders = timelineData.flatMap(timeline =>
                timeline.nodes?.flatMap(node => node.reminders || []) || []
            );
            setReminders(extractedReminders);
            remindersRef.current = extractedReminders;
        }
    }, [timelineData]);

    useEffect(() => {
        const interval = setInterval(() => {
            checkAndScheduleReminders(setReminders);
        }, 30000);
        
        return () => clearInterval(interval);
    }, []);

    return reminders;
};

export default useReminders;
