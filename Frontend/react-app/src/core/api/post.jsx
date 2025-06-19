import qs from 'qs';
import { api } from './apiConfig';

/**
 * Create new data
 * @param {string} apiUrl - The base API URL
 * @param {string} exactPath - Remaining path
 * @param {Object} data - The data to send
 * @param {boolean} [isFormUrlEncoded=false] - Whether to send as form-urlencoded
 * @returns {Promise<Object>} - Returns created data response
 */
export const Post = (apiUrl, exactPath, data, isFormUrlEncoded = false) => {
  const config = {
    headers: {
      'Content-Type': isFormUrlEncoded 
        ? 'application/x-www-form-urlencoded' 
        : 'application/json'
    }
  };

  const requestData = isFormUrlEncoded ? qs.stringify(data) : data;

  return api.post(`${apiUrl}${exactPath}`, requestData, config)
    .then(response => response.data)
    .catch(error => {
      const errorMessage = error.response?.data || { message: 'Network error' };
      throw errorMessage;
    });
};
