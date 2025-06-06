import { toast } from "react-toastify";
import { deleteById } from "../core/api/delete";
import { getAll } from "../core/api/getAll";
import { getById } from "../core/api/getById";
import { getAllWithoutPagination } from "../core/api/getWithoutPagination";
import { Post } from "../core/api/post";
import { Put } from "../core/api/put";
import API_BASE_URL from "../data/constants";

class PhysicalPersonService {
  /**
   * Create a new physical person
   * @param {Object} personData - Physical person data
   * @param {string} timelineId - Associated timeline ID
   * @returns {Promise<Object>} - Created physical person data
   */
  static createPhysicalPerson(personData, timelineId) {
    return Post(API_BASE_URL, "/PhysicalPersons", {
      ...personData,
      timelineId
    })
      .then(response => {
        toast.success("Physical person created successfully!");
        return response.data;
      })
      .catch(error => {
        const errorMessage = error.response?.data?.message || "Failed to create physical person";
        toast.error(errorMessage);
        throw error;
      });
  }

  /**
   * Get all physical persons for a timeline
   * @param {string} timelineId - The timeline ID
   * @returns {Promise<Array>} - Array of physical persons
   */
  static getPhysicalPersonsByTimeline(timelineId) {
    return getAll(API_BASE_URL, `/PhysicalPersons`)
      .then(response => response.physicalPersons || [])
      .catch(error => {
        const errorMessage = error.response?.data?.message || "Failed to fetch physical persons";
        toast.error(errorMessage);
        return [];
      });
  }

  /**
   * Get all physical persons for a timeline
   * @param {string} timelineId - The timeline ID
   * @returns {Promise<Array>} - Array of physical persons
   */
  static getPhysicalPersonsByTimelineWithoutPagination(timelineId) {
    return getAllWithoutPagination(API_BASE_URL, `/PhysicalPersons/Timeline/${timelineId}`)
      .then(response => response.physicalPersons || [])
      .catch(error => {
        const errorMessage = error.response?.data?.message || "Failed to fetch physical persons";
        toast.error(errorMessage);
        return [];
      });
  }

  /**
   * Get a single physical person by ID
   * @param {string} id - The physical person ID
   * @returns {Promise<Object>} - The physical person data
   */
  static getPhysicalPersonById(id) {
    return getById(API_BASE_URL, "/PhysicalPersons/", id)
      .then(response => response.physicalPerson)
      .catch(error => {
        toast.error("Failed to fetch physical person");
        throw error;
      });
  }

  /**
   * Update a physical person
   * @param {string} id - Physical person ID to update
   * @param {Object} updateData - Fields to update
   * @returns {Promise<Object>} - Updated physical person data
   */
  static updatePhysicalPerson(id, updateData) {
    return Put(API_BASE_URL, `/PhysicalPersons/${id}`, updateData)
      .then(response => {
        toast.success("Physical person updated successfully!");
        return response.data;
      })
      .catch(error => {
        const errorMessage = error.response?.data?.message || "Failed to update physical person";
        toast.error(errorMessage);
        throw error;
      });
  }

  /**
   * Delete a physical person by ID
   * @param {string} id - The physical person ID to delete
   * @returns {Promise<Object>} - Delete confirmation
   */
  static deletePhysicalPerson(id) {
    return deleteById(API_BASE_URL, "/PhysicalPersons/", id)
      .then(response => {
        toast.success("Physical person deleted successfully!");
        return response;
      })
      .catch(error => {
        const errorMessage = error.response?.data?.message || "Failed to delete physical person";
        toast.error(errorMessage);
        throw error;
      });
  }
}

export default PhysicalPersonService;
