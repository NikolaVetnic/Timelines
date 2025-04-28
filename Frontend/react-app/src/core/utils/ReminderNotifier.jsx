import { useCallback, useEffect } from 'react';
import { toast } from 'react-toastify';
import ReminderService from '../../services/ReminderService';

const ReminderNotifier = () => {
  const showReminderNotification = useCallback((reminder) => {
    toast.info(
      `REMINDER: ${reminder.title}
      Description: ${reminder.description || "No additional details"}`,
      {
        position: "top-right",
        autoClose: 60000,
        closeOnClick: false,
        draggable: true,
        toastId: `reminder-${reminder.id}`,
      }
    );
  }, []);

  const checkAndTriggerReminders = useCallback(async () => {
    try {
      const now = new Date();
      const response = await ReminderService.getAllReminders();
      const allReminders = response.data || [];

      allReminders.forEach(reminder => {
        if (!reminder.notificationTime) return;

        const reminderTime = new Date(reminder.notificationTime);
        const timeDifference = reminderTime.getTime() - now.getTime();

        // todo: enable this check when update is available
        // if (reminder.status === "Notified") return;
        if(reminderTime.getTime() < now.getTime()) return;

        if (timeDifference <= 0) {
          showReminderNotification(reminder);
          return;
        }

        setTimeout(() => {
          showReminderNotification(reminder);
        }, timeDifference);
      });

    } catch (error) {
      console.error('[ReminderCheck] Error:', error);
    }
  }, [showReminderNotification]);

  useEffect(() => {
    checkAndTriggerReminders();
    const interval = setInterval(checkAndTriggerReminders, 60000);
    
    return () => {
      clearInterval(interval);
    };
  }, [checkAndTriggerReminders]);

  return null;
};

export default ReminderNotifier;
