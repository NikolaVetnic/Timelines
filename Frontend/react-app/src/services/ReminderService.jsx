import { toast } from "react-toastify";
import { deleteById } from "../core/api/delete";
import { getAll } from "../core/api/getAll";
import { getById } from "../core/api/getById";
import { getAllWithoutPagination } from "../core/api/getWithoutPagination";
import { Post } from "../core/api/post";
import API_BASE_URL from "../data/constants";

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
        notifyAt: reminderData.notifyAt,
        priority: reminderData.priority || 0,
        nodeId: reminderData.nodeId,
      };

      const response = await Post(API_BASE_URL, "/Reminders", formattedData);
      toast.success("Reminder created successfully!");
      return response;
    } catch (error) {
      const errorMessage = error.response?.data?.message || 
                        error.message || 
                        "Failed to create reminder";
      toast.error(errorMessage);
    }
  }

  /**
   * Get reminders by node ID with server-side pagination
   * @param {string} nodeId - Parent node ID
   * @param {number} pageIndex - Pagination index (0-based)
   * @param {number} pageSize - Items per page
   * @returns {Promise<Object>} - { items: [], totalCount: 0, totalPages: 0 }
   */
  static async getRemindersByNode(nodeId, pageIndex = 0, pageSize = 10) {
    try {
      const response = await getAll(API_BASE_URL, `/Nodes/${nodeId}/Reminders`, pageIndex, pageSize);

      const reminders = response.reminders.data?.map(reminder => ({
        id: reminder.id,
        title: reminder.title,
        description: reminder.description,
        notifyAt: reminder.notifyAt,
        priority: reminder.priority,
        nodeId: nodeId,
      })) || [];
      
      return {
        items: reminders,
        totalCount: response.reminders.count || 0,
        totalPages: Math.ceil((response.reminders.count || 0) / pageSize),
      };
    } catch (error) {
      const errorMessage = error.response?.data?.message || 
                        error.message || 
                        "Failed to fetch node reminders";
      toast.error(errorMessage);
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
    }
  }
}

export default ReminderService;
