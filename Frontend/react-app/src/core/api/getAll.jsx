import { api } from "./apiConfig";

/**
 * Get a list of data with pagination
 * @param {string} apiUrl - The base API URL
 * @param {string} exactPath - The remaining path after the base URL
 * @param {number} pageIndex - The index of the page (default is 0)
 * @param {number} pageSize - The size of the page (default is 10)
 * @param {Object} additionalParams - Any additional query parameters to include in the request
 * @returns {Promise<Object>} - The list of data
 */
export const getAll = (apiUrl, exactPath, pageIndex = 0, pageSize = 10) => {
  return api.get(`${apiUrl}${exactPath}`, {
    params: {
      PageIndex: pageIndex,
      PageSize: pageSize,
    }
  })
  .then(response => response.data)
  .catch(error => {
    const errorMessage = error.response?.data || { message: 'Network error' };
    throw errorMessage;
  });
};
