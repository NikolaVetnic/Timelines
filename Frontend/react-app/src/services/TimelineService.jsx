import { toast } from "react-toastify";
import { getAll, getById } from "../core/api/get"; // Import the getAll function
import Post from "../core/api/post";
import API_BASE_URL from "../data/constants";

class TimelineService {
  static async createTimeline(title) {
    const timelineData = {
      Timeline: {
        Title: title,
      },
    };

    try {
      const response = await Post(API_BASE_URL, "/Timelines", timelineData);
      
      if (response.status === 200 || response.status === 201) {
        toast.success("Timeline created successfully!");
        return response.data;
      }

      return response.data;
    } catch (error) {
      if (error.response && error.response.data && error.response.data.message) {
        throw new Error(error.response.data.message);
      } else {
        throw new Error("Failed to create timeline. Please try again.");
      }
    }
  }

  /**
   * Get all timelines with pagination
   * @param {number} pageIndex - The page index (default: 0)
   * @param {number} pageSize - The number of items per page (default: 10)
   * @returns {Promise<Array>} - Array of timelines
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
                totalPages: Math.ceil((response.timelines?.count || 0) / pageSize)
            };
        } catch (error) {
            let errorMessage = "Failed to fetch timelines";
            
            if (error.response) {
                if (error.response.status === 404) {
                    errorMessage = "No timelines found";
                } else if (error.response.data && error.response.data.message) {
                    errorMessage = error.response.data.message;
                }
            }
            
            toast.error(errorMessage);
            throw new Error(errorMessage);
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
      return response;
    } catch (error) {
      let errorMessage = "Failed to fetch timeline";
      
      if (error.response) {
        if (error.response.status === 404) {
          errorMessage = "Timeline not found";
        } else if (error.response.data && error.response.data.message) {
          errorMessage = error.response.data.message;
        }
      }
      
      toast.error(errorMessage);
      throw new Error(errorMessage);
    }
  }
}

export default TimelineService;
