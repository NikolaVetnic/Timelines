import { api } from './apiConfig';

/**
 * Update existing data
 * @param {string} apiUrl - The base API URL
 * @param {string} exactPath - Remaining path (should include ID if needed)
 * @param {Object} data - The data to send for update
 * @returns {Promise<Object>} - Returns updated data response
 */
export const Put = async (apiUrl, exactPath, data) => {
  try {
    const response = await api.put(`${apiUrl}${exactPath}`, data);
    return response.data;
  } catch (error) {
    const errorMessage = error.response?.data || { message: 'Network error' };
    throw errorMessage;
  }
};
