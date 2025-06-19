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
  static createTimeline(title, description) {
    const timelineData = {
      Title: title,
      Description: description,
    };

    return Post(API_BASE_URL, "/Timelines", timelineData)
      .then(response => {
        toast.success("Timeline created successfully!");
        return response.data;
      })
      .catch(error => {
        const errorMessage = error.response?.data?.message || "Failed to create timeline";
        toast.error(errorMessage);
        throw error;
      });
  }

  /**
   * Clone a timeline
   * @param {string} timelineId - Timeline id
   * @param {string} title - Timeline title
   * @param {string} description - Timeline description
   * @returns {Promise<Object>} - Created timeline data
   */
  static cloneTimeline(timelineId, title, description) {
    return Post(API_BASE_URL, `/Timelines/Clone/${timelineId}`, {
      title,
      description
    })
      .then(response => {
        toast.success("Timeline cloned successfully!");
        return response.timeline.id;
      })
      .catch(error => {
        const errorMessage = error.response?.data?.message || "Failed to clone timeline";
        toast.error(errorMessage);
        throw error;
      });
  }

  /**
   * Get all timelines with pagination
   * @param {number} pageIndex - The page index (default: 0)
   * @param {number} pageSize - The number of items per page (default: 10)
   * @returns {Promise<Object>} - { items: [], totalCount: 0, totalPages: 0 }
   */
  static getAllTimelines(pageIndex = 0, pageSize = 10) {
    return getAll(API_BASE_URL, "/Timelines", pageIndex, pageSize)
      .then(response => ({
        items: response.timelines?.data || [],
        totalCount: response.timelines?.count || 0,
        totalPages: Math.ceil((response.timelines?.count || 0) / pageSize),
      }))
      .catch(error => {
        const errorMessage = error.response?.data?.message || "Failed to fetch timelines";
        toast.error(errorMessage);
        throw error;
      });
  }

  /**
   * Get a single timeline by ID
   * @param {string} id - The timeline ID
   * @returns {Promise<Object>} - The timeline data
   */
  static getTimelineById(id) {
    return getById(API_BASE_URL, "/Timelines/", id)
      .then(response => response.timeline || response)
      .catch(error => {
        const errorMessage = error.response?.data?.message || "Failed to fetch timeline";
        toast.error(errorMessage);
        throw error;
      });
  }

  /**
   * Update a timeline
   * @param {string} id - Timeline ID to update
   * @param {Object} updateData - Fields to update
   * @param {string} [updateData.Title] - New title
   * @param {string} [updateData.Description] - New description
   * @returns {Promise<Object>} - Updated timeline data
   */
  static updateTimeline(id, updateData) {
    return Put(API_BASE_URL, `/Timelines/${id}`, updateData)
      .then(response => {
        toast.success("Timeline updated successfully!");
        return response.data;
      })
      .catch(error => {
        const errorMessage = error.response?.data?.message || "Failed to update timeline";
        toast.error(errorMessage);
        throw error;
      });
  }

  /**
   * Delete a timeline by ID
   * @param {string} id - The timeline ID to delete
   * @returns {Promise<Object>} - Delete confirmation
   */
  static deleteTimeline(id) {
    return deleteById(API_BASE_URL, "/Timelines/", id)
      .then(response => {
        toast.success("Timeline deleted successfully!");
        return response;
      })
      .catch(error => {
        const errorMessage = error.response?.data?.message || "Failed to delete timeline";
        toast.error(errorMessage);
        throw error;
      });
  }

  /**
   * Search timelines by query
   * @param {string} query - Search query
   * @returns {Promise<Object>} - { items: [], totalCount: 0 }
   */
  static searchTimelines(query) {
    return this.getAllTimelines(0, 1000)
      .then(allTimelinesResponse => {
        const allTimelines = allTimelinesResponse.items || [];
        const filtered = allTimelines.filter(timeline => {
          const searchContent = `${timeline.title} ${timeline.description}`.toLowerCase();
          return searchContent.includes(query.toLowerCase());
        });

        return {
          items: filtered,
          totalCount: filtered.length
        };
      })
      .catch(error => {
        const errorMessage = error.response?.data?.message || "Search failed";
        toast.error(errorMessage);
        return { items: [], totalCount: 0 };
      });
  }
}

export default TimelineService;
