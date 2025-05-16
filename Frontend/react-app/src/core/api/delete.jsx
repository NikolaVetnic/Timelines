import { api } from './apiConfig';

/**
 * Delete data by ID
 * @param {string} apiUrl - The base API URL
 * @param {string} exactPath - The remaining path after the base URL
 * @param {string} id - The ID of the resource to delete
 * @returns {Promise<Object>} - The delete confirmation message
 */
export const deleteById = async (apiUrl, exactPath, id) => {
  try {
    if (!id) throw new Error('ID is required');
    const response = await api.delete(`${apiUrl}${exactPath}${id}`);
    return response.data;
  } catch (error) {
    const errorMessage = error.response?.data || { message: 'Network error' };
    throw errorMessage;
  }
};

