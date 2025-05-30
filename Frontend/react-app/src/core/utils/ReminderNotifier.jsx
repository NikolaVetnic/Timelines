import { useCallback, useEffect, useState } from 'react';
import { toast } from 'react-toastify';
import ReminderService from '../../services/ReminderService';
import ReminderModal from '../components/modals/ReminderModal/ReminderModal';

const STORAGE_KEY = 'dismissedReminders';

const ReminderNotifier = () => {
  const [currentReminder, setCurrentReminder] = useState(null);
  const [modalOpen, setModalOpen] = useState(false);
  const [processedReminders, setProcessedReminders] = useState(() => {
    if (typeof window !== 'undefined') {
      const saved = localStorage.getItem(STORAGE_KEY);
      return saved ? new Set(JSON.parse(saved)) : new Set();
    }
    return new Set();
  });

  useEffect(() => {
    if (typeof window !== 'undefined') {
      localStorage.setItem(STORAGE_KEY, JSON.stringify(Array.from(processedReminders)));
    }
  }, [processedReminders]);

  const showReminderNotification = useCallback((reminder) => {
    if (processedReminders.has(reminder.id)) return;

    if (reminder.priority === 1) {
      toast.info(
        `REMINDER: ${reminder.title}\nDescription: ${reminder.description || "No additional details"}`,
        {
          position: "top-right",
          autoClose: 60000,
          closeOnClick: false,
          draggable: true,
          toastId: `reminder-${reminder.id}`,
          onClose: () => {
            setProcessedReminders(prev => {
              const newSet = new Set(prev);
              newSet.add(reminder.id);
              return newSet;
            });
          }
        }
      );
    } else {
      setCurrentReminder(reminder);
      setModalOpen(true);
    }
  }, [processedReminders]);

  const handleModalClose = useCallback(() => {
    if (currentReminder) {
      setProcessedReminders(prev => {
        const newSet = new Set(prev);
        newSet.add(currentReminder.id);
        return newSet;
      });
    }
    setModalOpen(false);
    setCurrentReminder(null);
  }, [currentReminder]);

  const checkAndTriggerReminders = useCallback(async () => {
    try {
      const response = await ReminderService.getAllReminders();
      const allReminders = response.data || [];
      
      const now = new Date();
      const today = new Date(now.getFullYear(), now.getMonth(), now.getDate());
      
      const todaysReminders = allReminders.filter(reminder => {
        if (processedReminders.has(reminder.id)) return false;
        
        const notifyDate = new Date(reminder.notifyAt);
        const reminderDate = new Date(notifyDate.getFullYear(), notifyDate.getMonth(), notifyDate.getDate());
        
        return reminderDate.getTime() === today.getTime();
      });

      const currentTime = now.getTime();
      const dueReminders = todaysReminders.filter(reminder => {
        const notifyTimestamp = new Date(reminder.notifyAt).getTime();
        return notifyTimestamp <= currentTime;
      });

      if (dueReminders.length > 0 && !modalOpen) {
        showReminderNotification(dueReminders[0]);
      }
    } catch (error) {
      console.error("Error checking reminders:", error);
    }
  }, [modalOpen, processedReminders, showReminderNotification]);

  useEffect(() => {
    checkAndTriggerReminders();
    const interval = setInterval(checkAndTriggerReminders, 30 * 1000);

    return () => clearInterval(interval);
  }, [checkAndTriggerReminders]);

  return (
    <ReminderModal
      reminder={currentReminder}
      isOpen={modalOpen}
      onClose={handleModalClose}
    />
  );
};

export default ReminderNotifier;
