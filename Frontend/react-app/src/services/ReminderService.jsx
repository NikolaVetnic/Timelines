import deleteById from "../core/api/delete";
import { getAllWithoutPagination, getById } from "../core/api/get";
import Post from "../core/api/post";
import API_BASE_URL from "../data/constants";

class ReminderService {
  /**
   * Create a new reminder
   * @param {Object} reminderData - The reminder data to create
   * @returns {Promise<Object>} - Created reminder data
   */
    static async createReminder(reminderData) {
        const formattedData = {
            title: reminderData.title,
            description: reminderData.description || "",
            dueDateTime: new Date(reminderData.dueDateTime).toISOString(),
            priority: reminderData.priority || 0,
            notificationTime: new Date(reminderData.notificationTime).toISOString(),
            status: reminderData.status || "Pending",
            nodeId: reminderData.nodeId
        };

        const response = await Post(API_BASE_URL, "/Reminders", formattedData);
        
        return response.id;
    }

  /**
   * Get all reminders for a node
   * @param {string} nodeId - Parent node ID
   * @returns {Promise<Array>} - Array of reminders
   */
  static async getRemindersByNode(nodeId) {
    const response = await getAllWithoutPagination(API_BASE_URL, `/Reminders`);
    
    if (response.reminders?.data && Array.isArray(response.reminders.data)) {
      return response.reminders.data.filter(reminder => reminder.node.id === nodeId);
    }
    return [];
  }

   /**
   * Get all reminders for a node
   * @returns {Promise<Array>} - Array of reminders
   */
  static async getAllReminders() {
    const response = await getAllWithoutPagination(API_BASE_URL, `/Reminders`);
    
    return response.reminders;
  }

  /**
   * Get a single reminder by ID
   * @param {string} id - The reminder ID
   * @returns {Promise<Object>} - The reminder data
   */
  static async getReminderById(id) {
    const response = await getById(API_BASE_URL, "/Reminders/", id);
    return response.reminder || response;
  }

  /**
   * Update a reminder
   * @param {string} id - Reminder ID to update
   * @param {Object} updateData - Fields to update
   * @returns {Promise<Object>} - Updated reminder data
   */
  static async updateReminder(id, updateData) {
    const response = await Post(API_BASE_URL, `/Reminders/${id}`, updateData);
    return response.data;
  }

  /**
   * Delete a reminder by ID
   * @param {string} id - The reminder ID to delete
   * @returns {Promise<Object>} - Delete confirmation
   */
  static async deleteReminder(id) {
    const response = await deleteById(API_BASE_URL, "/Reminders/", id);
    return response;
  }
}

export default ReminderService;
