import { toast } from "react-toastify";
import { getAll } from "../core/api/getAll";
import API_BASE_URL from "../data/constants";

class SearchService {
    static async searchTimelines(query) {
        try {
          const response = await getAll(
            API_BASE_URL,
            `/Timelines/search?query=${encodeURIComponent(query)}`,
            0,
            10
          );
          return {
            items: response.timelines?.data || [],
            totalCount: response.timelines?.count || 0
          };
        } catch (error) {
          const errorMessage =
            error.response?.data?.message || "Failed to search timelines";
          toast.error(errorMessage);
          return { items: [], totalCount: 0 };
        }
      }
}

export default SearchService;
