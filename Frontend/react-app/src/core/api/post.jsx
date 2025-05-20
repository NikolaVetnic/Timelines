// post.js
import qs from 'qs';
import { api } from './apiConfig';

/**
 * Create new data
 * @param {string} apiUrl - The base API URL
 * @param {string} exactPath - Remaining path
 * @param {Object} data - The data to send
 * @returns {Promise<Object>} - Returns created data response
 */
export const Post = async (apiUrl, exactPath, data, isFormUrlEncoded = false) => {
  try {
    const config = {
      headers: {
        'Content-Type': isFormUrlEncoded 
          ? 'application/x-www-form-urlencoded' 
          : 'application/json'
      }
    };

    const requestData = isFormUrlEncoded ? qs.stringify(data) : data;
    const response = await api.post(`${apiUrl}${exactPath}`, requestData, config);
    return response.data;
  } catch (error) {
    const errorMessage = error.response?.data || { message: 'Network error' };
    throw errorMessage;
  }
};
