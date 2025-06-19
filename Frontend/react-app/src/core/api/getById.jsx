import { api } from './apiConfig'; // Import the configured axios instance

/**
 * Get a data by ID
 * @param {string} apiUrl - The base API URL
 * @param {string} exactPath - The path to the specific resource
 * @param {string} id - The ID of the resource to fetch
 * @returns {Promise<Object>} - The resource data
 */
export const getById = (apiUrl, exactPath, id) => {
  // Validate ID first
  if (!id) {
    return Promise.reject({ message: 'ID is required to fetch the resource.' });
  }

  return api.get(`${apiUrl}${exactPath}${id}`)
    .then(response => response.data)
    .catch(error => {
      const errorMessage = error.response?.data || { 
        message: 'Network error, please try again later.' 
      };
      throw errorMessage;
    });
};
