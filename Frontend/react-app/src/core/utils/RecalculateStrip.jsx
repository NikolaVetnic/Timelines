const recalculateStrip = (cardsRef) => {
    if (cardsRef.current.length > 0) {
        const firstCard = cardsRef.current[0];
        const lastCard = cardsRef.current[cardsRef.current.length - 1];
        const topOffset = firstCard.offsetTop + firstCard.offsetHeight / 2;
        const bottomOffset = lastCard.offsetTop + lastCard.offsetHeight / 2;

        return {
            top: `${topOffset}px`,
            height: `${bottomOffset - topOffset}px`,
        };
    }
    return {};
};

export default recalculateStrip;
