import { toast } from "react-toastify";
import deleteById from "../core/api/delete";
import { getAll, getById } from "../core/api/get";
import Post from "../core/api/post";
import API_BASE_URL from "../data/constants";

class NoteService {
  /**
   * Create a new note
   * @param {Object} noteData - The note data to create
   * @param {string} noteData.nodeId - Parent node ID
   * @param {string} noteData.title - Note title
   * @param {string} noteData.content - Note content
   * @param {string} noteData.timestamp - ISO timestamp
   * @param {number} noteData.importance - Importance level (0-2)
   * @returns {Promise<Object>} - Created note data
   */
  static async createNote(noteData) {
    const formattedData = {
      id: noteData.nodeId, // Using nodeId as the parent reference
      title: noteData.title || "Untitled Note",
      content: noteData.content || "",
      timestamp: noteData.timestamp || new Date().toISOString(),
      importance: noteData.importance || 0,
    };

    try {
      const response = await Post(API_BASE_URL, "/Notes", formattedData);

      if (response.status === 200 || response.status === 201) {
        toast.success("Note created successfully!");
        return response.data;
      }

      return response.data;
    } catch (error) {
      if (error.response?.data?.message) {
        throw new Error(error.response.data.message);
      } else {
        throw new Error("Failed to create note. Please try again.");
      }
    }
  }

  /**
   * Get all notes with pagination
   * @param {number} pageIndex - The page index (default: 0)
   * @param {number} pageSize - The number of items per page (default: 10)
   * @returns {Promise<Object>} - { items: [], totalCount: 0, totalPages: 0 }
   */
  static async getAllNotes(pageIndex = 0, pageSize = 10) {
    try {
      const response = await getAll(
        API_BASE_URL,
        "/Notes",
        pageIndex,
        pageSize
      );

      return {
        items: response.notes?.data || [],
        totalCount: response.notes?.count || 0,
        totalPages: Math.ceil((response.notes?.count || 0) / pageSize),
      };
    } catch (error) {
      let errorMessage = "Failed to fetch notes";

      if (error.response) {
        if (error.response.status === 404) {
          errorMessage = "No notes found";
        } else if (error.response.data?.message) {
          errorMessage = error.response.data.message;
        }
      }

      toast.error(errorMessage);
      throw new Error(errorMessage);
    }
  }

  /**
   * Get notes by node ID
   * @param {string} nodeId - Parent node ID
   * @param {number} pageIndex - Pagination index
   * @param {number} pageSize - Items per page
   * @returns {Promise<Object>} - { items: [], totalCount: 0 }
   */
  static async getNotesByNode(nodeId, pageIndex = 0, pageSize = 10) {
    try {
      const response = await getAll(
        API_BASE_URL,
        `/Notes/node/${nodeId}`,
        pageIndex,
        pageSize
      );

      return {
        items: response.notes?.data || [],
        totalCount: response.notes?.count || 0,
        totalPages: Math.ceil((response.notes?.count || 0) / pageSize),
      };
    } catch (error) {
      let errorMessage = "Failed to fetch node notes";

      if (error.response) {
        if (error.response.status === 404) {
          errorMessage = "No notes found for this node";
        } else if (error.response.data?.message) {
          errorMessage = error.response.data.message;
        }
      }

      toast.error(errorMessage);
      throw new Error(errorMessage);
    }
  }

  /**
   * Get a single note by ID
   * @param {string} id - The note ID
   * @returns {Promise<Object>} - The note data
   */
  static async getNoteById(id) {
    try {
      const response = await getById(API_BASE_URL, "/Notes/", id);
      return response.note || response;
    } catch (error) {
      let errorMessage = "Failed to fetch note";

      if (error.response) {
        if (error.response.status === 404) {
          errorMessage = "Note not found";
        } else if (error.response.data?.message) {
          errorMessage = error.response.data.message;
        }
      }

      toast.error(errorMessage);
      throw new Error(errorMessage);
    }
  }

  /**
   * Update a note
   * @param {string} id - Note ID to update
   * @param {Object} updateData - Fields to update
   * @returns {Promise<Object>} - Updated note data
   */
  static async updateNote(id, updateData) {
    try {
      const response = await Post(API_BASE_URL, `/Notes/${id}`, updateData);
      toast.success("Note updated successfully!");
      return response.data;
    } catch (error) {
      let errorMessage = "Failed to update note";

      if (error.response) {
        if (error.response.status === 404) {
          errorMessage = "Note not found";
        } else if (error.response.data?.message) {
          errorMessage = error.response.data.message;
        }
      }

      toast.error(errorMessage);
      throw new Error(errorMessage);
    }
  }

  /**
   * Delete a note by ID
   * @param {string} id - The note ID to delete
   * @returns {Promise<Object>} - Delete confirmation
   */
  static async deleteNote(id) {
    try {
      const response = await deleteById(API_BASE_URL, "/Notes/", id);

      if (response) {
        toast.success("Note deleted successfully!");
        return response;
      }
    } catch (error) {
      let errorMessage = "Failed to delete note";

      if (error.response) {
        if (error.response.status === 404) {
          errorMessage = "Note not found";
        } else if (error.response.data?.message) {
          errorMessage = error.response.data.message;
        }
      }

      toast.error(errorMessage);
      throw new Error(errorMessage);
    }
  }
}

export default NoteService;
