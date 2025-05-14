import axios from 'axios';

/**
 * Update existing data
 * @param {string} apiUrl - The base API URL
 * @param {string} exactPath - Remaining path (should include ID if needed)
 * @param {Object} data - The data to send for update
 * @returns {Promise<Object>} - Returns updated data response
 */
const Put = async (apiUrl, exactPath, data) => {
  try {
    const response = await axios.put(`${apiUrl}${exactPath}`, data, {
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

export default Put;
