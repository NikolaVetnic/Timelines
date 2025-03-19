import axios from 'axios';

/**
 * Create a new node
 * @param {string} apiUrl - The base API URL
 * @param {Object} nodeData - The node data to send
 * @returns {Promise<Object>} - Returns created node response
 */
const createNode = async (apiUrl, nodeData) => {
  try {
    const response = await axios.post(`${apiUrl}/Nodes`, { node: nodeData }, {
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

/**
 * Get a list of nodes with pagination
 * @param {string} apiUrl - The base API URL
 * @param {number} pageIndex - Page index (default is 0)
 * @param {number} pageSize - Page size (default is 10)
 * @returns {Promise<Object>} - Returns the list of nodes
 */
const getNodes = async (apiUrl, pageIndex = 0, pageSize = 10) => {
  try {
    const response = await axios.get(`${apiUrl}/Nodes`, {
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

export { createNode, getNodes };

