import { toast } from "react-toastify";

const checkAndScheduleReminders = ( setReminders ) => {
    const now = new Date();
    const savedTimelineData = JSON.parse(localStorage.getItem("timelineData")) || [];

    const extractedReminders = savedTimelineData.flatMap(timeline =>
        timeline.nodes?.flatMap(node => node.reminders || []) || []
    );

    setReminders(extractedReminders);

    let updatedReminders = [...extractedReminders];

    extractedReminders.forEach((reminder, index) => {
        const reminderTime = new Date(reminder.notifyAt);
        const timeUntilReminder = reminderTime - now;

        if (!reminder.notified && timeUntilReminder > 0) {
            setTimeout(() => {
                toast.info(` Reminder: ${reminder.title} \nDescription: ${reminder.description || "No description provided."}`, {
                    position: "top-right",
                    autoClose: 60000,
                    hideProgressBar: false,
                    closeOnClick: true,
                    pauseOnHover: true,
                    draggable: true,
                });

                updatedReminders[index] = { ...reminder, notified: true };

                savedTimelineData.forEach(timeline => {
                    timeline.nodes?.forEach(node => {
                        if (node.reminders) {
                            node.reminders = node.reminders.map(r =>
                                r.id === reminder.id ? { ...r, notified: true } : r
                            );
                        }
                    });
                });

                localStorage.setItem("timelineData", JSON.stringify(savedTimelineData));
            }, timeUntilReminder);
        }
    });
};

export default checkAndScheduleReminders;
