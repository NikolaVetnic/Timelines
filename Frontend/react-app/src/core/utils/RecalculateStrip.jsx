const recalculateStrip = (nodesRef) => {
    if (nodesRef.current.length > 0) {
        const firstNode = nodesRef.current[0];
        const lastNode = nodesRef.current[nodesRef.current.length - 1];

        if (!firstNode || !lastNode) return {};

        const topOffset = firstNode.offsetTop + firstNode.offsetHeight / 1;
        const bottomOffset = lastNode.offsetTop + lastNode.offsetHeight / 1;

        return {
            top: `${topOffset}px`,
            height: `${bottomOffset - topOffset}px`,
        };
    }
    return {};
};

export default recalculateStrip;
