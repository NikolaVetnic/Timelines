import { toast } from "react-toastify";
import { deleteById } from "../core/api/delete";
import { getAll } from "../core/api/getAll";
import { getById } from "../core/api/getById";
import { Post } from "../core/api/post";
import API_BASE_URL, { FILE_NUMBER_TO_TYPE, FILE_TYPE_TO_NUMBER } from "../data/constants";

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
    const fileContent = await new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.readAsDataURL(fileData.file);
      reader.onload = () => resolve(reader.result.split(",")[1]);
      reader.onerror = (error) => reject(error);
    });

    const filePayload = {
      name: fileData.file.name,
      description: fileData.description || "No description",
      size: fileData.file.size,
      type: this.getFileTypeNumber(fileData.file.type) || 1,
      owner: "username",
      content: fileContent, // This is the base64 content
      sharedWith: [],
      isPublic: false,
      nodeId: fileData.nodeId,
    };

    if (!filePayload.name || !filePayload.type || !filePayload.owner || !filePayload.nodeId) {
      throw new Error("Missing required fields for file upload");
    }

    const response = await Post(API_BASE_URL, "/Files", filePayload);
    
    toast.success("File uploaded successfully!");
    
    return {
      ...response,
      url: URL.createObjectURL(fileData.file),
      name: fileData.file.name,
      size: fileData.file.size,
      type: fileData.file.type,
      nodeId: fileData.nodeId
    };
  } catch (error) {
    const errorMessage = error.response?.data?.message ||
                        error.message ||
                        "Failed to upload file";
    toast.error(errorMessage);
  }
}

  static getFileTypeNumber(mimeType) {
    const typeMap = FILE_TYPE_TO_NUMBER;
    return typeMap[mimeType] || 0;
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
    }
  }

/**
 * Get files by node ID with server-side pagination
 * @param {string} nodeId - Parent node ID
 * @param {number} pageIndex - Pagination index (0-based)
 * @param {number} pageSize - Items per page
 * @returns {Promise<Object>} - { items: [], totalCount: 0, totalPages: 0 }
 */
static async getFilesByNode(nodeId, pageIndex = 0, pageSize = 10) {
  try {
    const response = await getAll(API_BASE_URL, `/Nodes/${nodeId}/Files`, pageIndex, pageSize);
    const files = response.fileAssets?.data?.map(fileAsset => ({
      id: fileAsset.id,
      name: fileAsset.name,
      size: fileAsset.size * 1024,
      type: this.getMimeType(fileAsset.type),
      url: fileAsset.content || "",
      nodeId: nodeId,
      description: fileAsset.description,
    })) || [];
    
    return {
      items: files,
      totalCount: response.fileAssets?.count || 0,
      totalPages: Math.ceil((response.fileAssets?.count || 0) / pageSize),
    };
  } catch (error) {
    const errorMessage = error.response?.data?.message || "Failed to fetch files";
    toast.error(errorMessage);
  }
}

  static getMimeType(typeNumber) {
    const types = FILE_NUMBER_TO_TYPE;
    return types[typeNumber] || "application/octet-stream";
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
