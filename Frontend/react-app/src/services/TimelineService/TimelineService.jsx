import axios from 'axios';

/**
 * Create a new timeline
 * @param {string} apiUrl - The base API URL
 * @param {Object} timelineData - The timeline data to send
 * @returns {Promise<Object>} - Returns created timeline response
 */
const createTimeline = async (apiUrl, timelineData) => {
  try {
    const response = await axios.post(`${apiUrl}/Timelines`, { timeline: timelineData }, {
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
 * Get a list of timelines with pagination
 * @param {string} apiUrl - The base API URL
 * @param {number} pageIndex - Page index (default is 0)
 * @param {number} pageSize - Page size (default is 10)
 * @returns {Promise<Object>} - Returns the list of timelines
 */
const getTimelines = async (apiUrl, pageIndex = 0, pageSize = 10) => {
  try {
    const response = await axios.get(`${apiUrl}/Timelines`, {
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
 * @param {string} timelineId - The ID of the timeline to fetch
 * @returns {Promise<Object>} - Returns the timeline data
 */
const getTimelineById = async (apiUrl, timelineId) => {
  try {
    const response = await axios.get(`${apiUrl}/Timelines/${timelineId}`, {
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
 * Delete a timeline by ID
 * @param {string} apiUrl - The base API URL
 * @param {string} timelineId - The ID of the timeline to delete
 * @returns {Promise<Object>} - Returns the delete confirmation
 */
const deleteTimeline = async (apiUrl, timelineId) => {
  try {
    const response = await axios.delete(`${apiUrl}/Timelines/${timelineId}`, {
      headers: {
        'Accept': 'application/json',
      },
    });

    return response.data;
  } catch (error) {
    throw error.response ? error.response.data : new Error('Network error');
  }
};

export { createTimeline, deleteTimeline, getTimelineById, getTimelines };

