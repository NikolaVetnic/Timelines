import { toast } from "react-toastify";
import deleteById from "../core/api/delete";
import { getAllWithoutPagination, getById } from "../core/api/get";
import Post from "../core/api/post";
import API_BASE_URL from "../data/constants";

class ReminderService {
  /**
   * Create a new reminder
   * @param {Object} reminderData - The reminder data to create
   * @param {string} reminderData.title - Reminder title
   * @param {string} reminderData.description - Reminder description
   * @param {string} reminderData.dueDateTime - ISO timestamp
   * @param {number} reminderData.priority - Priority level (0-2)
   * @param {string} reminderData.notificationTime - Notification time
   * @param {string} reminderData.status - Reminder status
   * @param {string} reminderData.nodeId - Parent node ID
   * @returns {Promise<Object>} - Created reminder data
   */
  static async createReminder(reminderData) {
    const formattedData = {
      title: reminderData.title,
      description: reminderData.description || "",
      dueDateTime: reminderData.dueDateTime || new Date().toISOString(),
      priority: reminderData.priority || 0,
      notificationTime: reminderData.notificationTime || new Date().toISOString(),
      status: reminderData.status || "Pending",
      nodeId: reminderData.nodeId
    };

    try {
      const response = await Post(API_BASE_URL, "/Reminders", formattedData);

      if (response.status === 200 || response.status === 201) {
        toast.success("Reminder created successfully!");
        return response.data;
      }

      return response.data;
    } catch (error) {
      if (error.response?.data?.message) {
        throw new Error(error.response.data.message);
      } else {
        throw new Error("Failed to create reminder. Please try again.");
      }
    }
  }

  /**
   * Get all reminders for a node
   * @param {string} nodeId - Parent node ID
   * @returns {Promise<Array>} - Array of reminders
   */
  static async getRemindersByNode(nodeId) {
    try {
      const response = await getAllWithoutPagination(API_BASE_URL, `/Reminders`);

    if (response.data && Array.isArray(response.data)) {
        const filteredReminders = response.data.filter(reminder => reminder.nodeId === nodeId || (reminder.nodeId && reminder.nodeId.id === nodeId));
        return filteredReminders;
    }

      return [];
    } catch (error) {
      let errorMessage = "Failed to fetch reminders";

      if (error.response) {
        if (error.response.status === 404) {
          return [];
        } else if (error.response.data?.message) {
          errorMessage = error.response.data.message;
        }
      }

      toast.error(errorMessage);
      throw new Error(errorMessage);
    }
  }

  /**
   * Get a single reminder by ID
   * @param {string} id - The reminder ID
   * @returns {Promise<Object>} - The reminder data
   */
  static async getReminderById(id) {
    try {
      const response = await getById(API_BASE_URL, "/Reminders/", id);
      return response.reminder || response;
    } catch (error) {
      let errorMessage = "Failed to fetch reminder";

      if (error.response) {
        if (error.response.status === 404) {
          errorMessage = "Reminder not found";
        } else if (error.response.data?.message) {
          errorMessage = error.response.data.message;
        }
      }

      toast.error(errorMessage);
      throw new Error(errorMessage);
    }
  }

  /**
   * Update a reminder
   * @param {string} id - Reminder ID to update
   * @param {Object} updateData - Fields to update
   * @returns {Promise<Object>} - Updated reminder data
   */
  static async updateReminder(id, updateData) {
    try {
      const response = await Post(API_BASE_URL, `/Reminders/${id}`, updateData);
      toast.success("Reminder updated successfully!");
      return response.data;
    } catch (error) {
      let errorMessage = "Failed to update reminder";

      if (error.response) {
        if (error.response.status === 404) {
          errorMessage = "Reminder not found";
        } else if (error.response.data?.message) {
          errorMessage = error.response.data.message;
        }
      }

      toast.error(errorMessage);
      throw new Error(errorMessage);
    }
  }

  /**
   * Delete a reminder by ID
   * @param {string} id - The reminder ID to delete
   * @returns {Promise<Object>} - Delete confirmation
   */
  static async deleteReminder(id) {
    try {
      const response = await deleteById(API_BASE_URL, "/Reminders/", id);

      if (response) {
        toast.success("Reminder deleted successfully!");
        return response;
      }
    } catch (error) {
      let errorMessage = "Failed to delete reminder";

      if (error.response) {
        if (error.response.status === 404) {
          errorMessage = "Reminder not found";
        } else if (error.response.data?.message) {
          errorMessage = error.response.data.message;
        }
      }

      toast.error(errorMessage);
      throw new Error(errorMessage);
    }
  }
}

export default ReminderService;
