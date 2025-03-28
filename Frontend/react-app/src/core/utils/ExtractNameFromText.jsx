export const extractNamesFromText = (text) => {
  const namePatterns = [
    /(?:name|first name|first)[:\s]+([a-z]+)[\s]+(?:last name|last|surname)[:\s]+([a-z]+)/i,
    /^([A-Z][a-z]+)\s+([A-Z][a-z]+)/,
    /([A-Z][a-z]+),\s+([A-Z][a-z]+)/,
  ];

  for (const pattern of namePatterns) {
    const match = text.match(pattern);
    if (match) {
      if (pattern.toString().includes(",")) {
        return { firstName: match[2], lastName: match[1] };
      }
      return { firstName: match[1], lastName: match[2] };
    }
  }

  return { firstName: "", lastName: "" };
};
