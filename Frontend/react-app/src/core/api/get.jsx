import axios from 'axios';

/**
 * Get a list of data with pagination
 * @param {string} apiUrl - The base API URL
 * @param {string} exactPath - The remaining path after the base URL
 * @param {number} pageIndex - The index of the page (default is 0)
 * @param {number} pageSize - The size of the page (default is 10)
 * @param {Object} additionalParams - Any additional query parameters to include in the request
 * @returns {Promise<Object>} - The list of data
 */
const getAll = async (apiUrl, exactPath, pageIndex = 0, pageSize = 10) => {
  try {
    const params = {
      PageIndex: pageIndex,
      PageSize: pageSize,
    };

    const response = await axios.get(`${apiUrl}${exactPath}`, {
      params,
      headers: {
        'Accept': 'application/json',
      },
    });

    return response.data;
  } catch (error) {
    const errorMessage = error.response
      ? error.response.data
      : { message: 'Network error, please try again later.' };
    throw new Error(errorMessage);
  }
};

/**
 * Get a data by ID
 * @param {string} apiUrl - The base API URL
 * @param {string} exactPath - The path to the specific resource
 * @param {string} id - The ID of the resource to fetch
 * @returns {Promise<Object>} - The resource data
 */
const getById = async (apiUrl, exactPath, id) => {
  try {
    if (!id) {
      throw new Error('ID is required to fetch the resource.');
    }

    const response = await axios.get(`${apiUrl}${exactPath}${id}`, {
      headers: {
        'Accept': 'application/json',
      },
    });

    return response.data;
  } catch (error) {
    const errorMessage = error.response
      ? error.response.data
      : { message: 'Network error, please try again later.' };
    throw new Error(errorMessage);
  }
};

export { getAll, getById };

