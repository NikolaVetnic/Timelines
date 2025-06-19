import PropTypes from "prop-types";
import { FaChevronLeft, FaChevronRight } from "react-icons/fa6";
import Button from "../buttons/Button/Button";
import "./Pagination.css";

const Pagination = ({
  currentPage,
  totalPages,
  itemsPerPage,
  onPageChange,
  onItemsPerPageChange,
  itemsPerPageOptions = [5, 10, 15, 20, 25, 30],
}) => {
  return (
    <div className="pagination-controls">
      <div className="pagination-navigation">
        <Button
          icon={<FaChevronLeft />}
          iconOnly
          shape="circle"
          onClick={() => onPageChange(currentPage - 1)}
          disabled={currentPage === 1}
          size="medium"
        />
        <span>
          Page {currentPage} of {totalPages}
        </span>
        <Button
          icon={<FaChevronRight />}
          iconOnly
          shape="circle"
          onClick={() => onPageChange(currentPage + 1)}
          disabled={currentPage >= totalPages}
          size="medium"
        />
      </div>

      <div className="page-size-selector">
        <label>Items per page:</label>
        <select
          value={itemsPerPage}
          onChange={(e) => onItemsPerPageChange(Number(e.target.value))}
        >
          {itemsPerPageOptions.map((size) => (
            <option key={size} value={size}>
              {size}
            </option>
          ))}
        </select>
      </div>
    </div>
  );
};

Pagination.propTypes = {
  currentPage: PropTypes.number.isRequired,
  totalPages: PropTypes.number.isRequired,
  itemsPerPage: PropTypes.number.isRequired,
  onPageChange: PropTypes.func.isRequired,
  onItemsPerPageChange: PropTypes.func.isRequired,
  itemsPerPageOptions: PropTypes.arrayOf(PropTypes.number),
};

export default Pagination;
