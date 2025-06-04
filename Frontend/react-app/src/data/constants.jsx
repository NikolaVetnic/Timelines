const API_BASE_URL =
    process.env.REACT_APP_API_BASE_URL || "https://localhost:25051/api";

export default API_BASE_URL;

export const LOCAL_STORAGE_KEY = "timelineData";
export const MAX_FILE_SIZE = 10 * 1024 * 1024;
export const DAY = 24 * 60 * 60 * 1000;

export const FILE_TYPE_TO_NUMBER = {
    "application/pdf": 1,
    "application/msword": 2,
    "application/vnd.openxmlformats-officedocument.wordprocessingml.document": 2,
    "application/vnd.ms-excel": 3,
    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet": 3,
    "application/vnd.ms-powerpoint": 4,
    "application/vnd.openxmlformats-officedocument.presentationml.presentation": 4,
    "text/plain": 5,
    "image/jpeg": 6,
    "image/png": 6,
    "image/gif": 6,
};

export const FILE_NUMBER_TO_TYPE = {
    1: "application/pdf",
    2: "application/msword",
};

export const FILE_TYPES = {
    "image/*": [".png", ".jpg", ".jpeg", ".gif", ".webp"],
    "application/pdf": [".pdf"],
    "application/msword": [".doc", ".docx"],
    "application/vnd.ms-excel": [".xls", ".xlsx"],
    "application/vnd.ms-powerpoint": [".ppt", ".pptx"],
    "text/plain": [".txt"],
};

export const PRIORITY = {
    1: "Low",
    2: "Medium",
    3: "High",
};

export const PRIORITY_OPTIONS = [
    { value: 1, label: "Low" },
    { value: 2, label: "Medium" },
    { value: 3, label: "High" },
];
