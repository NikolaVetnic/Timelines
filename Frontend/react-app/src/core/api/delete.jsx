import { api } from './apiConfig';

/**
 * Delete data by ID
 * @param {string} apiUrl - The base API URL
 * @param {string} exactPath - The remaining path after the base URL
 * @param {string} id - The ID of the resource to delete
 * @returns {Promise<Object>} - The delete confirmation message
 */
export const deleteById = (apiUrl, exactPath, id) => {
  if (!id) {
    return Promise.reject({ message: 'ID is required' });
  }

  return api.delete(`${apiUrl}${exactPath}${id}`)
    .then(response => response.data)
    .catch(error => {
      const errorMessage = error.response?.data || { message: 'Network error' };
      throw errorMessage;
    });
};
