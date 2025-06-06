import { toast } from "react-toastify";
import { deleteById } from "../core/api/delete";
import { getAll } from "../core/api/getAll";
import { getById } from "../core/api/getById";
import { Post } from "../core/api/post";
import { Put } from "../core/api/put";
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
  static createNote(noteData) {
    const formattedData = {
      title: noteData.title || "Untitled Note",
      content: noteData.content || "",
      timestamp: noteData.timestamp || new Date().toISOString(),
      nodeId: noteData.nodeId,
      isPublic: true,
      sharedWith: noteData.sharedWith || [],
      owner: "username"
    };

    return Post(API_BASE_URL, "/Notes", formattedData)
      .then(response => {
        toast.success("Note created successfully!");
        return response.data;
      })
      .catch(error => {
        const errorMessage = error.response?.data?.message || "Failed to create note";
        toast.error(errorMessage);
        throw error;
      });
  }

  /**
   * Get all notes with pagination
   * @param {number} pageIndex - The page index (default: 0)
   * @param {number} pageSize - The number of items per page (default: 10)
   * @returns {Promise<Object>} - { items: [], totalCount: 0, totalPages: 0 }
   */
  static getAllNotes(pageIndex = 0, pageSize = 10) {
    return getAll(API_BASE_URL, "/Notes", pageIndex, pageSize)
      .then(response => ({
        items: response.notes?.data || [],
        totalCount: response.notes?.count || 0,
        totalPages: Math.ceil((response.notes?.count || 0) / pageSize),
      }))
      .catch(error => {
        const errorMessage = error.response?.data?.message || "Failed to fetch notes";
        toast.error(errorMessage);
        throw error;
      });
  }

  /**
   * Get notes by node ID with server-side pagination
   * @param {string} nodeId - Parent node ID
   * @param {number} pageIndex - Pagination index (0-based)
   * @param {number} pageSize - Items per page
   * @returns {Promise<Object>} - { items: [], totalCount: 0, totalPages: 0 }
   */
  static getNotesByNode(nodeId, pageIndex = 0, pageSize = 10) {
    return getAll(API_BASE_URL, `/Nodes/${nodeId}/Notes`, pageIndex, pageSize)
      .then(response => {
        const notes = response.notes.data?.map(note => ({
          id: note.id,
          title: note.title,
          description: note.description,
          content: note.content,
          owner: note.owner,
          createdAt: note.createdAt,
          lastModifiedAt: note.lastModifiedAt,
          isPublic: note.isPublic,
          sharedWith: note.sharedWith || [],
          nodeId: nodeId,
        })) || [];
        
        return {
          items: notes,
          totalCount: response.notes.count || 0,
          totalPages: Math.ceil((response.notes.count || 0) / pageSize),
        };
      })
      .catch(error => {
        const errorMessage = error.response?.data?.message || 
                            error.message || 
                            "Failed to fetch node notes";
        toast.error(errorMessage);
        throw error;
      });
  }

  /**
   * Get a single note by ID
   * @param {string} id - The note ID
   * @returns {Promise<Object>} - The note data
   */
  static getNoteById(id) {
    return getById(API_BASE_URL, "/Notes/", id)
      .then(response => response.note || response)
      .catch(error => {
        const errorMessage = error.response?.data?.message || "Failed to fetch note";
        toast.error(errorMessage);
        throw error;
      });
  }

  /**
   * Update a note
   * @param {string} id - Note ID to update
   * @param {Object} updateData - Fields to update
   * @returns {Promise<Object>} - Updated note data
   */
  static updateNote(id, updateData) {
    return Put(API_BASE_URL, `/Notes/${id}`, updateData)
      .then(response => {
        toast.success("Note updated successfully!");
        return response.data;
      })
      .catch(error => {
        const errorMessage = error.response?.data?.message || "Failed to update note";
        toast.error(errorMessage);
        throw error;
      });
  }

  /**
   * Delete a note by ID
   * @param {string} id - The note ID to delete
   * @returns {Promise<Object>} - Delete confirmation
   */
  static deleteNote(id) {
    return deleteById(API_BASE_URL, "/Notes/", id)
      .then(response => {
        toast.success("Note deleted successfully!");
        return response;
      })
      .catch(error => {
        const errorMessage = error.response?.data?.message || "Failed to delete note";
        toast.error(errorMessage);
        throw error;
      });
  }
}

export default NoteService;
