import axios from 'axios';

/**
 * Delete a timeline by ID
 * @param {string} apiUrl - The base API URL
 * @param {string} exactPath - Remaining path
 * @param {string} id - The ID of the timeline to delete
 * @returns {Promise<Object>} - Returns the delete confirmation
 */
const Delete = async (apiUrl, exactPath, id) => {
  try {
    const response = await axios.delete(`${apiUrl}${exactPath}${id}`, {
      headers: {
        'Accept': 'application/json',
      },
    });

    return response.data;
  } catch (error) {
    throw error.response ? error.response.data : new Error('Network error');
  }
};

export default Delete;
