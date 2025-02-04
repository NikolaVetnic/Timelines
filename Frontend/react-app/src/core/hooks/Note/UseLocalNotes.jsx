import { useCallback, useEffect, useState } from "react";

const LOCAL_STORAGE_KEY = "timelineData";

const useLocalNotes = (timelineId, nodeId) => {
  const [notes, setNotes] = useState([]);

  useEffect(() => {
    try {
      const storedData = JSON.parse(localStorage.getItem(LOCAL_STORAGE_KEY)) || [];
      const timeline = storedData.find(t => t.id === timelineId);
      const node = timeline?.nodes.find(n => n.id === nodeId);
      setNotes(node?.notes || []);
    } catch (error) {
      console.error("Error loading notes:", error);
    }
  }, [timelineId, nodeId]);

  const updateLocalStorage = useCallback((updatedNotes) => {
    try {
      const storedData = JSON.parse(localStorage.getItem(LOCAL_STORAGE_KEY)) || [];
      const timelineIndex = storedData.findIndex(t => t.id === timelineId);
      
      if (timelineIndex !== -1) {
        const nodeIndex = storedData[timelineIndex].nodes.findIndex(n => n.id === nodeId);
        if (nodeIndex !== -1) {
          storedData[timelineIndex].nodes[nodeIndex].notes = updatedNotes;
          localStorage.setItem(LOCAL_STORAGE_KEY, JSON.stringify(storedData));
        }
      }
    } catch (error) {
      console.error("Error saving notes:", error);
    }
  }, [timelineId, nodeId]);

  return { notes, setNotes, updateLocalStorage };
};

export default useLocalNotes;
