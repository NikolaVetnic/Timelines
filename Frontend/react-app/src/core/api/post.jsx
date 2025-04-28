import axios from 'axios';

/**
 * Create new data
 * @param {string} apiUrl - The base API URL
 * @param {string} exactPath - Remaining path
 * @param {Object} data - The data to send
 * @returns {Promise<Object>} - Returns created data response
 */
const Post = async (apiUrl, exactPath, data) => {
  try {
    const response = await axios.post(`${apiUrl}${exactPath}`, data, {
      headers: {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
      },
    });

    return response.data;
  } catch (error) {
    throw error.response ? error.response.data : new Error('Network error, please try again later.');
  }
};

export default Post;
