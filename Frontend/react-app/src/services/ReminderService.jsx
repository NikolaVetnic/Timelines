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
  static createReminder(reminderData) {
    const formattedData = {
      title: reminderData.title,
      description: reminderData.description || "",
      notifyAt: reminderData.notifyAt,
      priority: reminderData.priority || 0,
      nodeId: reminderData.nodeId,
      colorHex: reminderData.colorHex || this.getDefaultColorForPriority(reminderData.priority)
    };

    return Post(API_BASE_URL, "/Reminders", formattedData)
      .then(response => {
        toast.success("Reminder created successfully!");
        return response;
      })
      .catch(error => {
        const errorMessage = error.response?.data?.message || 
                          error.message || 
                          "Failed to create reminder";
        toast.error(errorMessage);
        throw error;
      });
  }

  static getDefaultColorForPriority(priority) {
    switch(priority) {
      case 2:
        return '#ffa500';
      case 1: 
        return '#ffff00';
      default:
        return '#ff0000';
    }
  }

  /**
   * Get reminders by node ID with server-side pagination
   * @param {string} nodeId - Parent node ID
   * @param {number} pageIndex - Pagination index (0-based)
   * @param {number} pageSize - Items per page
   * @returns {Promise<Object>} - { items: [], totalCount: 0, totalPages: 0 }
   */
  static getRemindersByNode(nodeId, pageIndex = 0, pageSize = 10) {
    return getAll(API_BASE_URL, `/Nodes/${nodeId}/Reminders`, pageIndex, pageSize)
      .then(response => {
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
      })
      .catch(error => {
        toast.error("Failed to fetch node reminders");
        throw error;
      });
  }

  /**
   * Get all reminders
   * @returns {Promise<Array>} - Array of reminders
   */
  static getAllReminders() {
    return getAllWithoutPagination(API_BASE_URL, `/Reminders`)
      .then(response => response.reminders || [])
      .catch(error => {
        toast.error("Failed to load all reminders");
        throw error;
      });
  }

  /**
   * Get a single reminder by ID
   * @param {string} id - The reminder ID
   * @returns {Promise<Object>} - The reminder data
   */
  static getReminderById(id) {
    return getById(API_BASE_URL, "/Reminders/", id)
      .then(response => response.reminder || response)
      .catch(error => {
        toast.error("Failed to load reminder details");
        throw error;
      });
  }

  /**
   * Update a reminder
   * @param {string} id - Reminder ID to update
   * @param {Object} updateData - Fields to update
   * @returns {Promise<Object>} - Updated reminder data
   */
  static updateReminder(id, updateData) {
    return Post(API_BASE_URL, `/Reminders/${id}`, updateData)
      .then(response => {
        toast.success("Reminder updated successfully!");
        return response.data;
      })
      .catch(error => {
        toast.error(error.message || "Failed to update reminder");
        throw error;
      });
  }

  /**
   * Delete a reminder by ID
   * @param {string} id - The reminder ID to delete
   * @returns {Promise<Object>} - Delete confirmation
   */
  static deleteReminder(id) {
    return deleteById(API_BASE_URL, "/Reminders/", id)
      .then(response => {
        toast.success("Reminder deleted successfully!");
        return response;
      })
      .catch(error => {
        toast.error(error.message || "Failed to delete reminder");
        throw error;
      });
  }
}

export default ReminderService;
