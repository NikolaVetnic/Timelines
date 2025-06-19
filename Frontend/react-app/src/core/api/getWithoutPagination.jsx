import { api } from './apiConfig'; // Import the configured axios instance

/**
 * Get a list of data without pagination
 * @param {string} apiUrl - The base API URL
 * @param {string} exactPath - The remaining path after the base URL
 * @param {Object} additionalParams - Any additional query parameters to include in the request
 * @returns {Promise<Object>} - The list of data
 */
export const getAllWithoutPagination = (apiUrl, exactPath, additionalParams = {}) => {
  return api.get(`${apiUrl}${exactPath}`, {
    params: additionalParams
  })
    .then(response => response.data)
    .catch(error => {
      const errorMessage = error.response?.data || { 
        message: 'Network error, please try again later.' 
      };
      throw errorMessage;
    });
};
