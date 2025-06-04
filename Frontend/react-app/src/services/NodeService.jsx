import { toast } from "react-toastify";
import { deleteById } from "../core/api/delete";
import { getAll } from "../core/api/getAll";
import { getById } from "../core/api/getById";
import { Post } from "../core/api/post";
import { Put } from "../core/api/put";
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
    try {
      const formattedData = {
        title: nodeData.title,
        description: nodeData.description || "",
        timestamp: nodeData.timestamp || new Date().toISOString(),
        importance: nodeData.importance || 0,
        phase: nodeData.phase || "Uncategorized",
        categories: nodeData.categories || [],
        tags: nodeData.tags || [],
        timelineId: nodeData.timelineId,
      };

      const response = await Post(API_BASE_URL, "/Nodes", formattedData);
      toast.success("Node created successfully!");
      return response.data;
    } catch (error) {
      const errorMessage =
        error.response?.data?.message || "Failed to create node";
      toast.error(errorMessage);
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
      const errorMessage =
        error.response?.data?.message || "Failed to fetch nodes";
      toast.error(errorMessage);
    }
  }

  /**
   * Get nodes by timeline ID
   * @param {string} timelineId - Parent timeline ID
   * @param {number} pageIndex - Pagination index (default: 0)
   * @param {number} pageSize - Items per page (default: 10)
   * @returns {Promise<Object>} - { items: [], totalCount: 0, totalPages: 0 }
   */
  static async getNodesByTimeline(timelineId, pageIndex = 0, pageSize = 10) {
    try {
      const response = await getAll(
        API_BASE_URL,
        `/Timeline/${timelineId}/Nodes`,
        pageIndex,
        pageSize
      );

      return {
        items: response.nodes?.data || [],
        totalCount: response.nodes?.count || 0,
        totalPages: Math.ceil((response.nodes?.count || 0) / pageSize),
      };
    } catch (error) {
      const errorMessage =
        error.response?.data?.message || "Failed to fetch timeline nodes";
      toast.error(errorMessage);
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
      return response.node || response;
    } catch (error) {
      const errorMessage =
        error.response?.data?.message || "Failed to fetch node";
      toast.error(errorMessage);
    }
  }

/**
 * Update a node with complete data
 * @param {Object} node - Full node object
 * @param {Object} updates - Fields to update
 * @returns {Promise<Object>} - Updated node data
 */
static async updateNode(node, updates) {
  try {
    const currentData = await this.getNodeById(node.id);
    
    const updatedNode = {
      ...currentData,
      ...updates,
      id: node.id
    };

    const response = await Put(
      API_BASE_URL, 
      `/Nodes/${node.id}`,
      updatedNode,
      {
        headers: {
          'Content-Type': 'application/json'
        }
      }
    );

    toast.success("Node updated successfully!");
    return response.data;
  } catch (error) {
    const errorMessage = error.ValidationErrors[0].errorMessage;
    toast.error(errorMessage);
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
      toast.success("Node deleted successfully!");
      return response;
    } catch (error) {
      const errorMessage =
        error.response?.data?.message || "Failed to delete node";
      toast.error(errorMessage);
    }
  }
}

export default NodeService;
