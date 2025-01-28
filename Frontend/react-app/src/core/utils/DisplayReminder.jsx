import { toast } from "react-toastify";

export const checkReminders = (reminders, setReminders) => {
    const now = new Date();
    let hasUpdates = false;

    const updatedReminders = reminders.map(reminder => {
        const reminderTime = new Date(reminder.notifyAt);
        if (reminderTime <= now && !reminder.notified) {
            toast.info(`⏰ Reminder: ${reminder.title}`, {
                position: "top-right",
                autoClose: 5000,
                hideProgressBar: false,
                closeOnClick: true,
                pauseOnHover: true,
                draggable: true,
            });

            hasUpdates = true;
            return { ...reminder, notified: true };
        }
        return reminder;
    });

    if (hasUpdates) {
        setReminders(updatedReminders);
        localStorage.setItem("reminders", JSON.stringify(updatedReminders));
    }
};
