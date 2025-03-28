import { createWorker } from "tesseract.js";

export const extractTextFromImage = async (imageUrl, progressCallback) => {
  const worker = await createWorker({
    logger: progressCallback
      ? (m) => progressCallback(m.status, m.progress)
      : (m) => console.log(m),
  });

  try {
    await worker.loadLanguage("eng+srp+srp_latn");
    await worker.initialize("eng+srp+srp_latn");
    const { data } = await worker.recognize(imageUrl);
    return data.text;
  } finally {
    await worker.terminate();
  }
};
