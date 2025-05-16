import { api } from './apiConfig'; // Import the configured axios instance

/**
 * Get a data by ID
 * @param {string} apiUrl - The base API URL
 * @param {string} exactPath - The path to the specific resource
 * @param {string} id - The ID of the resource to fetch
 * @returns {Promise<Object>} - The resource data
 */
export const getById = async (apiUrl, exactPath, id) => {
  try {
    if (!id) {
      throw { message: 'ID is required to fetch the resource.' }; // Consistent error object
    }

    const response = await api.get(`${apiUrl}${exactPath}${id}`);
    return response.data;
  } catch (error) {
    const errorMessage = error.response?.data || { 
      message: 'Network error, please try again later.' 
    };
    throw errorMessage; // Throw the error object directly
  }
};
