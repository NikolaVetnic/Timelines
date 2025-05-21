import { toast } from "react-toastify";
import { deleteById } from "../core/api/delete";
import { getAll } from "../core/api/getAll";
import { getById } from "../core/api/getById";
import { Post } from "../core/api/post";
import { Put } from "../core/api/put";
import API_BASE_URL from "../data/constants";

class TimelineService {
  /**
   * Create a new timeline
   * @param {string} title - Timeline title
   * @param {string} description - Timeline description
   * @returns {Promise<Object>} - Created timeline data
   */
  static async createTimeline(title, description) {
    try {
      const timelineData = {
        Title: title,
        Description: description,
      };

      const response = await Post(API_BASE_URL, "/Timelines", timelineData);
      toast.success("Timeline created successfully!");
      return response.data;
    } catch (error) {
      const errorMessage =
        error.response?.data?.message || "Failed to create timeline";
      toast.error(errorMessage);
    }
  }

    /**
   * Create a new timeline
   * @param {string} timelineId - Timeline id
   * @param {string} title - Timeline title
   * @param {string} description - Timeline description
   * @returns {Promise<Object>} - Created timeline data
   */
  static async cloneTimeline(timelineId, title, description) {
  try {
    const response = await Post(API_BASE_URL, `/Timelines/Clone/${timelineId}`, {
      title,
      description
    });
    toast.success("Timeline cloned successfully!");
    return response.timeline.id;
  } catch (error) {
    const errorMessage = error.response?.data?.message || "Failed to clone timeline";
    toast.error(errorMessage);
  }
}

  /**
   * Get all timelines with pagination
   * @param {number} pageIndex - The page index (default: 0)
   * @param {number} pageSize - The number of items per page (default: 10)
   * @returns {Promise<Object>} - { items: [], totalCount: 0, totalPages: 0 }
   */
  static async getAllTimelines(pageIndex = 0, pageSize = 10) {
    try {
      const response = await getAll(
        API_BASE_URL,
        "/Timelines",
        pageIndex,
        pageSize
      );

      return {
        items: response.timelines?.data || [],
        totalCount: response.timelines?.count || 0,
        totalPages: Math.ceil((response.timelines?.count || 0) / pageSize),
      };
    } catch (error) {
      const errorMessage =
        error.response?.data?.message || "Failed to fetch timelines";
      toast.error(errorMessage);
    }
  }

  /**
   * Get a single timeline by ID
   * @param {string} id - The timeline ID
   * @returns {Promise<Object>} - The timeline data
   */
  static async getTimelineById(id) {
    try {
      const response = await getById(API_BASE_URL, "/Timelines/", id);
      return response.timeline || response;
    } catch (error) {
      const errorMessage =
        error.response?.data?.message || "Failed to fetch timeline";
      toast.error(errorMessage);
    }
  }

  /**
   * Update a timeline
   * @param {string} id - Timeline ID to update
   * @param {Object} updateData - Fields to update
   * @param {string} [updateData.Title] - New title
   * @param {string} [updateData.Description] - New description
   * @returns {Promise<Object>} - Updated timeline data
   */
  static async updateTimeline(id, updateData) {
    try {
      const response = await Put(API_BASE_URL, `/Timelines/${id}`, updateData);
      toast.success("Timeline updated successfully!");
      return response.data;
    } catch (error) {
      const errorMessage =
        error.response?.data?.message || "Failed to update timeline";
      toast.error(errorMessage);
    }
  }

  /**
   * Delete a timeline by ID
   * @param {string} id - The timeline ID to delete
   * @returns {Promise<Object>} - Delete confirmation
   */
  static async deleteTimeline(id) {
    try {
      const response = await deleteById(API_BASE_URL, "/Timelines/", id);
      toast.success("Timeline deleted successfully!");
      return response;
    } catch (error) {
      const errorMessage =
        error.response?.data?.message || "Failed to delete timeline";
      toast.error(errorMessage);
    }
  }
}

export default TimelineService;
