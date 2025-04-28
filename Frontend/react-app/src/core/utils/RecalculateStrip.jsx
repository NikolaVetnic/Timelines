const recalculateStrip = (nodesRef, isMobile = false) => {
  if (nodesRef.current.length > 1) {
    const firstNode = nodesRef.current[0];
    const lastNode = nodesRef.current[nodesRef.current.length - 1];

    if (!lastNode) return {};

    const topOffset = firstNode.offsetTop + firstNode.offsetHeight / 1.5;
    const bottomOffset = lastNode.offsetTop + lastNode.offsetHeight / 1.5;

    return {
      top: `${topOffset}px`,
      height: `${bottomOffset - topOffset}px`,
      left: isMobile ? '20px' : '50%',
      transform: isMobile ? 'none' : 'translateX(-50%)'
    };
  }
  return {};
};

export default recalculateStrip;
