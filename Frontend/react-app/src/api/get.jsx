import axios from 'axios';

/**
 * Get a list of timelines with pagination
 * @param {string} apiUrl - The base API URL
 * @param {string} exactPath - Remaining path
 * @param {number} pageIndex - Page index (default is 0)
 * @param {number} pageSize - Page size (default is 10)
 * @returns {Promise<Object>} - Returns the list of timelines
 */
const getAll = async (apiUrl, exactPath, pageIndex = 0, pageSize = 10) => {
  try {
    const response = await axios.get(`${apiUrl}${exactPath}`, {
      params: {
        PageIndex: pageIndex,
        PageSize: pageSize,
      },
      headers: {
        'Accept': 'application/json',
      },
    });

    return response.data;
  } catch (error) {
    throw error.response ? error.response.data : new Error('Network error');
  }
};

/**
 * Get a timeline by ID
 * @param {string} apiUrl - The base API URL
 * @param {string} exactPath - Remaining path from API URL to Id
 * @param {string} id - The ID of the timeline to fetch
 * @returns {Promise<Object>} - Returns the timeline data
 */
const getById = async (apiUrl, exactPath, id) => {
  try {
    const response = await axios.get(`${apiUrl}${exactPath}${id}`, {
      headers: {
        'Accept': 'application/json',
      },
    });

    return response.data;
  } catch (error) {
    throw error.response ? error.response.data : new Error('Network error');
  }
};

export { getAll, getById };

