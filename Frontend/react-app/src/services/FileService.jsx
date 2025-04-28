import { toast } from "react-toastify";
import deleteById from "../core/api/delete";
import { getAll, getById } from "../core/api/get";
import Post from "../core/api/post";
import API_BASE_URL from "../data/constants";

class FileService {
  /**
   * Upload a new file
   * @param {Object} fileData - The file data to upload
   * @param {string} fileData.nodeId - Parent node ID
   * @param {File} fileData.file - The file object
   * @param {string} fileData.title - File title
   * @param {string} fileData.description - File description
   * @returns {Promise<Object>} - Uploaded file data
   */
  static async uploadFile(fileData) {
    try {
      const formData = new FormData();
      formData.append("file", fileData.file);
      formData.append("nodeId", fileData.nodeId);
      formData.append("title", fileData.title || fileData.file.name);
      formData.append("description", fileData.description || "");

      const response = await Post(API_BASE_URL, "/Files", formData, {
        headers: {
          "Content-Type": "multipart/form-data",
        },
      });

      toast.success("File uploaded successfully!");
      return response.data;
    } catch (error) {
      const errorMessage =
        error.response?.data?.message || "Failed to upload file";
      toast.error(errorMessage);
      throw new Error(errorMessage);
    }
  }

  /**
   * Get all files with pagination
   * @param {number} pageIndex - The page index (default: 0)
   * @param {number} pageSize - The number of items per page (default: 10)
   * @returns {Promise<Object>} - { items: [], totalCount: 0, totalPages: 0 }
   */
  static async getAllFiles(pageIndex = 0, pageSize = 10) {
    try {
      const response = await getAll(
        API_BASE_URL,
        "/Files",
        pageIndex,
        pageSize
      );

      return {
        items: response.files?.data || [],
        totalCount: response.files?.count || 0,
        totalPages: Math.ceil((response.files?.count || 0) / pageSize),
      };
    } catch (error) {
      const errorMessage =
        error.response?.data?.message || "Failed to fetch files";
      toast.error(errorMessage);
      throw new Error(errorMessage);
    }
  }

  /**
   * Get files by node ID
   * @param {string} nodeId - Parent node ID
   * @param {number} pageIndex - Pagination index
   * @param {number} pageSize - Items per page
   * @returns {Promise<Object>} - { items: [], totalCount: 0 }
   */
  static async getFilesByNode(nodeId, pageIndex = 0, pageSize = 10) {
    try {
      const response = await getAll(
        API_BASE_URL,
        `/Files/node/${nodeId}`,
        pageIndex,
        pageSize
      );

      return {
        items: response.files?.data || [],
        totalCount: response.files?.count || 0,
        totalPages: Math.ceil((response.files?.count || 0) / pageSize),
      };
    } catch (error) {
      const errorMessage =
        error.response?.data?.message || "Failed to fetch node files";
      toast.error(errorMessage);
      throw new Error(errorMessage);
    }
  }

  /**
   * Get a single file by ID
   * @param {string} id - The file ID
   * @returns {Promise<Object>} - The file data
   */
  static async getFileById(id) {
    try {
      const response = await getById(API_BASE_URL, "/Files/", id);
      return response.file || response;
    } catch (error) {
      const errorMessage =
        error.response?.data?.message || "Failed to fetch file";
      toast.error(errorMessage);
      throw new Error(errorMessage);
    }
  }

  /**
   * Update file metadata
   * @param {string} id - File ID to update
   * @param {Object} updateData - Fields to update (title, description, etc.)
   * @returns {Promise<Object>} - Updated file data
   */
  static async updateFile(id, updateData) {
    try {
      const response = await Post(API_BASE_URL, `/Files/${id}`, updateData);
      toast.success("File updated successfully!");
      return response.data;
    } catch (error) {
      const errorMessage =
        error.response?.data?.message || "Failed to update file";
      toast.error(errorMessage);
      throw new Error(errorMessage);
    }
  }

  /**
   * Delete a file by ID
   * @param {string} id - The file ID to delete
   * @returns {Promise<Object>} - Delete confirmation
   */
  static async deleteFile(id) {
    try {
      const response = await deleteById(API_BASE_URL, "/Files/", id);
      toast.success("File deleted successfully!");
      return response;
    } catch (error) {
      const errorMessage =
        error.response?.data?.message || "Failed to delete file";
      toast.error(errorMessage);
      throw new Error(errorMessage);
    }
  }

  /**
   * Download a file by ID
   * @param {string} id - The file ID to download
   * @returns {Promise<void>}
   */
  static async downloadFile(id) {
    try {
      const response = await getById(API_BASE_URL, "/Files/download/", id, {
        responseType: "blob",
      });

      // Create download link and trigger click
      const url = window.URL.createObjectURL(new Blob([response]));
      const link = document.createElement("a");
      link.href = url;
      link.setAttribute(
        "download",
        response.headers["content-disposition"]?.split("filename=")[1] || "file"
      );
      document.body.appendChild(link);
      link.click();
      link.remove();

      toast.success("File download started!");
    } catch (error) {
      const errorMessage =
        error.response?.data?.message || "Failed to download file";
      toast.error(errorMessage);
      throw new Error(errorMessage);
    }
  }
}

export default FileService;
