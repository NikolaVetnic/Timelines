import axios from 'axios';

/**
 * Delete data by ID
 * @param {string} apiUrl - The base API URL
 * @param {string} exactPath - The remaining path after the base URL
 * @param {string} id - The ID of the resource to delete
 * @returns {Promise<Object>} - The delete confirmation message
 */
const deleteById = async (apiUrl, exactPath, id) => {
  try {
    if (!id) {
      throw new Error('ID is required to delete the resource.');
    }

    const response = await axios.delete(`${apiUrl}${exactPath}${id}`, {
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

export default deleteById;
