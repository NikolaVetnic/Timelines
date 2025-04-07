import { toast } from "react-toastify";
import deleteById from "../core/api/delete";
import { getAll, getById } from "../core/api/get";
import Post from "../core/api/post";
import API_BASE_URL from "../data/constants";

class NodeService {
  /**
   * Create a new node
   * @param {Object} nodeData - The node data to create
   * @param {string} nodeData.title - Node title
   * @param {string} nodeData.description - Node description
   * @param {string} nodeData.timestamp - ISO timestamp
   * @param {number} nodeData.importance - Importance level (0-2)
   * @param {string} nodeData.phase - Phase identifier
   * @param {Array<string>} nodeData.categories - Array of categories
   * @param {Array<string>} nodeData.tags - Array of tags
   * @param {string} nodeData.timelineId - Parent timeline ID
   * @returns {Promise<Object>} - Created node data
   */
  static async createNode(nodeData) {
    const formattedData = {
      title: nodeData.title,
      description: nodeData.description,
      timestamp: nodeData.timestamp || new Date().toISOString(),
      importance: nodeData.importance || 0,
      phase: nodeData.phase || "Uncategorized",
      categories: nodeData.categories || [],
      tags: nodeData.tags || [],
      timelineId: nodeData.timelineId,
    };

    try {
      const response = await Post(API_BASE_URL, "/Nodes", formattedData);

      if (response.status === 200 || response.status === 201) {
        toast.success("Node created successfully!");
        return response.data;
      }

      return response.data;
    } catch (error) {
      if (error.response?.data?.message) {
        throw new Error(error.response.data.message);
      } else {
        throw new Error("Failed to create node. Please try again.");
      }
    }
  }

  /**
   * Get all nodes with pagination
   * @param {number} pageIndex - The page index (default: 0)
   * @param {number} pageSize - The number of items per page (default: 10)
   * @returns {Promise<Object>} - { items: [], totalCount: 0, totalPages: 0 }
   */
  static async getAllNodes(pageIndex = 0, pageSize = 10) {
    try {
      const response = await getAll(
        API_BASE_URL,
        "/Nodes",
        pageIndex,
        pageSize
      );

      return {
        items: response.nodes?.data || [],
        totalCount: response.nodes?.count || 0,
        totalPages: Math.ceil((response.nodes?.count || 0) / pageSize),
      };
    } catch (error) {
      let errorMessage = "Failed to fetch nodes";

      if (error.response) {
        if (error.response.status === 404) {
          errorMessage = "No nodes found";
        } else if (error.response.data?.message) {
          errorMessage = error.response.data.message;
        }
      }

      toast.error(errorMessage);
      throw new Error(errorMessage);
    }
  }

  /**
   * Get nodes by timeline ID
   * @param {string} timelineId - Parent timeline ID
   * @param {number} pageIndex - Pagination index
   * @param {number} pageSize - Items per page
   * @returns {Promise<Object>} - { items: [], totalCount: 0 }
   */
  static async getNodesByTimeline(timelineId, pageIndex = 0, pageSize = 10) {
    try {
      const response = await getAll(
        API_BASE_URL,
        `/Nodes/timeline/${timelineId}`,
        pageIndex,
        pageSize
      );

      return {
        items: response.nodes?.data || [],
        totalCount: response.nodes?.count || 0,
        totalPages: Math.ceil((response.nodes?.count || 0) / pageSize),
      };
    } catch (error) {
      let errorMessage = "Failed to fetch timeline nodes";

      if (error.response) {
        if (error.response.status === 404) {
          errorMessage = "No nodes found for this timeline";
        } else if (error.response.data?.message) {
          errorMessage = error.response.data.message;
        }
      }

      toast.error(errorMessage);
      throw new Error(errorMessage);
    }
  }

  /**
   * Get a single node by ID
   * @param {string} id - The node ID
   * @returns {Promise<Object>} - The node data
   */
  static async getNodeById(id) {
    try {
      const response = await getById(API_BASE_URL, "/Nodes/", id);
      return response.node || response; // Adapt based on your API response structure
    } catch (error) {
      let errorMessage = "Failed to fetch node";

      if (error.response) {
        if (error.response.status === 404) {
          errorMessage = "Node not found";
        } else if (error.response.data?.message) {
          errorMessage = error.response.data.message;
        }
      }

      toast.error(errorMessage);
      throw new Error(errorMessage);
    }
  }

  /**
   * Delete a node by ID
   * @param {string} id - The node ID to delete
   * @returns {Promise<Object>} - Delete confirmation
   */
  static async deleteNode(id) {
    try {
      const response = await deleteById(API_BASE_URL, "/Nodes/", id);

      if (response) {
        toast.success("Node deleted successfully!");
        return response;
      }
    } catch (error) {
      let errorMessage = "Failed to delete node";

      if (error.response) {
        if (error.response.status === 404) {
          errorMessage = "Node not found";
        } else if (error.response.data?.message) {
          errorMessage = error.response.data.message;
        }
      }

      toast.error(errorMessage);
      throw new Error(errorMessage);
    }
  }
}

export default NodeService;
