import { useCallback, useState } from 'react';
import FileService from '../../../../../services/FileService';

export const useFileAnalysis = () => {
  const [files, setFiles] = useState([]);
  const [isLoadingFiles, setIsLoadingFiles] = useState(false);
  const [pagination, setPagination] = useState({
    pageIndex: 0,
    pageSize: 10,
    totalCount: 0,
    totalPages: 0
  });

  const fetchFiles = useCallback(async () => {
    setIsLoadingFiles(true);
    try {
      const response = await FileService.getAllFiles(
        pagination.pageIndex, 
        pagination.pageSize
      );
      
      if (response && response.items) {
        setFiles(response.items);
        setPagination(prev => ({
          ...prev,
          totalCount: response.totalCount,
          totalPages: Math.ceil(response.totalCount / pagination.pageSize)
        }));
      }
    } catch (error) {
      console.error("Error fetching files:", error);
    } finally {
      setIsLoadingFiles(false);
    }
  }, [pagination.pageIndex, pagination.pageSize]);

  const handlePageChange = useCallback((newPageIndex) => {
    setPagination(prev => ({ ...prev, pageIndex: newPageIndex }));
  }, []);

  return {
    files,
    isLoadingFiles,
    pagination,
    fetchFiles,
    handlePageChange
  };
};
