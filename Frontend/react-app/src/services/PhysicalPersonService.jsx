// src/services/PhysicalPersonService.js
import { toast } from "react-toastify";
import { deleteById } from "../core/api/delete";
import { getAll } from "../core/api/getAll";
import { getById } from "../core/api/getById";
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
  static async createPhysicalPerson(personData, timelineId) {
    try {
      const response = await Post(API_BASE_URL, "/PhysicalPersons", {
        ...personData,
        timelineId
      });
      toast.success("Physical person created successfully!");
      return response.data;
    } catch (error) {
      const errorMessage =
        error.response?.data?.message || "Failed to create physical person";
      toast.error(errorMessage);
    }
  }

  /**
   * Get all physical persons for a timeline
   * @param {string} timelineId - The timeline ID
   * @returns {Promise<Array>} - Array of physical persons
   */
  static async getPhysicalPersonsByTimeline(timelineId) {
    try {
      const response = await getAll(API_BASE_URL, `/PhysicalPersons`);
      return response.physicalPersons || [];
    } catch (error) {
      const errorMessage =
        error.response?.data?.message || "Failed to fetch physical persons";
      toast.error(errorMessage);
      return [];
    }
  }

/**
 * Get all physical persons for a timeline
 * @param {string} timelineId - The timeline ID
 * @returns {Promise<Array>} - Array of physical persons
 */
static async getPhysicalPersonsByTimelineWithoutPagination(timelineId) {
  try {
    const response = await getAll(API_BASE_URL, '/PhysicalPersons');
    
    const filteredPersons = response.physicalPersons?.data?.filter(person => 
      person.timeline?.id === timelineId
    ) || [];
    
    return filteredPersons;
  } catch (error) {
    const errorMessage =
      error.response?.data?.message || "Failed to fetch physical persons";
    toast.error(errorMessage);
    return [];
  }
}

  /**
   * Get a single physical person by ID
   * @param {string} id - The physical person ID
   * @returns {Promise<Object>} - The physical person data
   */
  static async getPhysicalPersonById(id) {
    try {
      const response = await getById(API_BASE_URL, "/PhysicalPersons/", id);
      return response.physicalPerson;
    } catch (error) {
      const errorMessage = "Failed to fetch physical person";
      toast.error(errorMessage);
    }
  }

  /**
   * Update a physical person
   * @param {string} id - Physical person ID to update
   * @param {Object} updateData - Fields to update
   * @returns {Promise<Object>} - Updated physical person data
   */
  static async updatePhysicalPerson(id, updateData) {
    try {
      const response = await Put(API_BASE_URL, `/PhysicalPersons/${id}`, updateData);
      toast.success("Physical person updated successfully!");
      return response.data;
    } catch (error) {
      const errorMessage =
        error.response?.data?.message || "Failed to update physical person";
      toast.error(errorMessage);
    }
  }

  /**
   * Delete a physical person by ID
   * @param {string} id - The physical person ID to delete
   * @returns {Promise<Object>} - Delete confirmation
   */
  static async deletePhysicalPerson(id) {
    try {
      const response = await deleteById(API_BASE_URL, "/PhysicalPersons/", id);
      toast.success("Physical person deleted successfully!");
      return response;
    } catch (error) {
      const errorMessage =
        error.response?.data?.message || "Failed to delete physical person";
      toast.error(errorMessage);
    }
  }
}

export default PhysicalPersonService;
