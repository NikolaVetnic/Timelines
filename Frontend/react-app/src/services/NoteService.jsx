import { toast } from "react-toastify";
import deleteById from "../core/api/delete";
import { getAll, getById } from "../core/api/get";
import Post from "../core/api/post";
import Put from "../core/api/put";
import API_BASE_URL from "../data/constants";

class NoteService {
  /**
   * Create a new note
   * @param {Object} noteData - The note data to create
   * @param {string} noteData.nodeId - Parent node ID
   * @param {string} noteData.title - Note title
   * @param {string} noteData.content - Note content
   * @param {string} noteData.timestamp - ISO timestamp
   * @param {boolean} noteData.isPublic - Visibility flag
   * @param {string[]} noteData.sharedWith - Array of user IDs
   * @param {string} noteData.owner - Owner ID
   * @returns {Promise<Object>} - Created note data
   */
  static async createNote(noteData) {
    try {
      const formattedData = {
        title: noteData.title || "Untitled Note",
        content: noteData.content || "",
        timestamp: noteData.timestamp || new Date().toISOString(),
        nodeId: noteData.nodeId,
        isPublic: true,
        sharedWith: noteData.sharedWith || [],
        owner: "username"
      };

      const response = await Post(API_BASE_URL, "/Notes", formattedData);
      toast.success("Note created successfully!");
      return response.data;
    } catch (error) {
      const errorMessage =
        error.response?.data?.message || "Failed to create note";
      toast.error(errorMessage);
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
      const errorMessage =
        error.response?.data?.message || "Failed to fetch notes";
      toast.error(errorMessage);
    }
  }

  /**
 * Get notes by node ID with server-side pagination
 * @param {string} nodeId - Parent node ID
 * @param {number} pageIndex - Pagination index (0-based)
 * @param {number} pageSize - Items per page
 * @returns {Promise<Object>} - { items: [], totalCount: 0, totalPages: 0 }
 */
static async getNotesByNode(nodeId, pageIndex = 0, pageSize = 10) {
  try {
    const response = await getAll(API_BASE_URL, `/Nodes/${nodeId}/Notes`, pageIndex, pageSize);

    const notes = response.notes.data?.map(note => ({
      id: note.id,
      name: note.name,
      description: note.description,
      content: note.content,
      owner: note.owner,
      isPublic: note.isPublic,
      sharedWith: note.sharedWith || [],
      nodeId: nodeId,
    })) || [];
    
    return {
      items: notes,
      totalCount: response.notes.count || 0,
      totalPages: Math.ceil((response.notes.count || 0) / pageSize),
    };
  } catch (error) {
    const errorMessage = error.response?.data?.message || 
                        error.message || 
                        "Failed to fetch node notes";
    toast.error(errorMessage);
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
      const errorMessage =
        error.response?.data?.message || "Failed to fetch note";
      toast.error(errorMessage);
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
      const response = await Put(API_BASE_URL, `/Notes/${id}`, updateData);
      toast.success("Note updated successfully!");
      return response.data;
    } catch (error) {
      const errorMessage =
        error.response?.data?.message || "Failed to update note";
      toast.error(errorMessage);
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
      toast.success("Note deleted successfully!");
      return response;
    } catch (error) {
      const errorMessage =
        error.response?.data?.message || "Failed to delete note";
      toast.error(errorMessage);
    }
  }
}

export default NoteService;
