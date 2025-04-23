import deleteById from "../core/api/delete";
import { getAllWithoutPagination, getById } from "../core/api/get";
import Post from "../core/api/post";
import API_BASE_URL from "../data/constants";
import { toast } from "react-toastify";

class ReminderService {
  /**
   * Create a new reminder
   * @param {Object} reminderData - The reminder data to create
   * @returns {Promise<Object>} - Created reminder data
   */
  static async createReminder(reminderData) {
    try {
      const formattedData = {
        title: reminderData.title,
        description: reminderData.description || "",
        dueDateTime: new Date(reminderData.dueDateTime).toISOString(),
        priority: reminderData.priority || 0,
        notificationTime: new Date(reminderData.notificationTime).toISOString(),
        status: reminderData.status || "Pending",
        nodeId: reminderData.nodeId,
      };

      const response = await Post(API_BASE_URL, "/Reminders", formattedData);
      toast.success("Reminder created successfully!");
      return response.id;
    } catch (error) {
      toast.error(error.message || "Failed to create reminder");
      throw error;
    }
  }

  /**
   * Get all reminders for a node
   * @param {string} nodeId - Parent node ID
   * @returns {Promise<Array>} - Array of reminders
   */
  static async getRemindersByNode(nodeId) {
    try {
      const response = await getAllWithoutPagination(
        API_BASE_URL,
        `/Reminders`
      );

      if (response.reminders?.data && Array.isArray(response.reminders.data)) {
        return response.reminders.data.filter(
          (reminder) => reminder.node.id === nodeId
        );
      }
      return [];
    } catch (error) {
      toast.error("Failed to load reminders");
      throw error;
    }
  }

  /**
   * Get all reminders
   * @returns {Promise<Array>} - Array of reminders
   */
  static async getAllReminders() {
    try {
      const response = await getAllWithoutPagination(
        API_BASE_URL,
        `/Reminders`
      );
      return response.reminders;
    } catch (error) {
      toast.error("Failed to load all reminders");
      throw error;
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
      toast.error("Failed to load reminder details");
      throw error;
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
      toast.error(error.message || "Failed to update reminder");
      throw error;
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
      toast.success("Reminder deleted successfully!");
      return response;
    } catch (error) {
      toast.error(error.message || "Failed to delete reminder");
      throw error;
    }
  }
}

export default ReminderService;
