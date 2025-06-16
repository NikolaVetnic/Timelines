import { useEffect, useState } from "react";
import InfiniteScroll from "react-infinite-scroll-component";

export function InfiniteScrollWrapper({
  fetchData,
  renderItem,
  loader = <p>Loading...</p>,
  endMessage = <p>No more data to load.</p>,
  initialPage = 1,
  itemsPerPage = 5,
}) {
  const [page, setPage] = useState(initialPage);
  const [items, setItems] = useState([]);
  const [totalItems, setTotalItems] = useState(0);
  const [isLoading, setIsLoading] = useState(false);

  const handleFetchData = async (pageNumber) => {
    setIsLoading(true);
    try {
      const { items: newItems, total } = await fetchData(pageNumber);
      setItems((prevItems) =>
        pageNumber === initialPage ? newItems : [...prevItems, ...newItems]
      );
      if (total !== undefined && pageNumber === initialPage) {
        setTotalItems(total);
      }
    } finally {
      setIsLoading(false);
    }
  };

  const handleRefresh = () => {
    setPage(initialPage);
    handleFetchData(initialPage);
  };

  useEffect(() => {
    handleFetchData(initialPage);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const handleLoadMore = () => {
    if (!isLoading) {
      const nextPage = page + 1;
      setPage(nextPage);
      handleFetchData(nextPage);
    }
  };

  const hasMore = totalItems > items.length || totalItems === 0;

  return (
    <InfiniteScroll
      dataLength={items.length}
      next={handleLoadMore}
      hasMore={hasMore}
      loader={loader}
      endMessage={endMessage}
      pullDownToRefresh
      pullDownToRefreshThreshold={50}
      pullDownToRefreshContent={
        <h3 style={{ textAlign: "center" }}>↓ Pull down to refresh</h3>
      }
      releaseToRefreshContent={
        <h3 style={{ textAlign: "center" }}>↑ Release to refresh</h3>
      }
      refreshFunction={handleRefresh}
    >
      <div>
        {items.map((item, index) => (
          <div key={index}>{renderItem(item)}</div>
        ))}
      </div>
    </InfiniteScroll>
  );
}
