export const LOCAL_STORAGE_KEY = "timelineData";
export const MAX_FILE_SIZE = 10 * 1024 * 1024; // 10MB
export const DAY = 24 * 60 * 60 * 1000;

export const KEYWORDS = [
  "contract",
  "agreement",
  "signature",
  "constructors",
  "Vladimir Vuckovic",
  // Serbian Cyrillic keywords
  "уговор",
  "потпис",
  "ратификација",
  // Serbian Latin keywords
  "SUP",
  "MARKA I TIP",
  "ОСПОРЕНИ АКТИ ДРЖАВНИХ ОРГАНА:",
  "Дражен Медојевић",
  "Мироје Јовановић"
];

export const ACCEPTED_FILE_TYPES = {
  "image/*": [".png", ".jpg", ".jpeg", ".gif", ".webp"],
  "application/pdf": [".pdf"],
  "application/msword": [".doc", ".docx"],
  "application/vnd.ms-excel": [".xls", ".xlsx"],
  "application/vnd.ms-powerpoint": [".ppt", ".pptx"],
  "text/plain": [".txt"],
};
