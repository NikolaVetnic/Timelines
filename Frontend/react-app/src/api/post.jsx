import axios from 'axios';

/**
 * Create a new timeline
 * @param {string} apiUrl - The base API URL
 * @param {string} exactPath - Remaining path
 * @param {Object} data - The timeline data to send
 * @returns {Promise<Object>} - Returns created timeline response
 */
const Post = async (apiUrl, exactPath, data) => {
  try {
    const response = await axios.post(`${apiUrl}${exactPath}`, { data: data }, {
      headers: {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
      },
    });

    return response.data;
  } catch (error) {
    throw error.response ? error.response.data : new Error('Network error');
  }
};

export default Post;
