import { FaSignOutAlt } from "react-icons/fa";
import { useAuth } from "../../context/AuthContext";
import SearchBar from "../SearchBar/SearchBar";
import "./HeaderBar.css";

const HeaderBar = ({ onSearch }) => {
  const { logout } = useAuth();

  return (
    <header className="headbar">
      <div className="headbar-title">Timeline App</div>
      <div className="headbar-search">
        <SearchBar onSearch={onSearch} />
      </div>
      <div className="headbar-actions">
        <button className="logout-button" onClick={logout}>
          <FaSignOutAlt />
          <span>Logout</span>
        </button>
      </div>
    </header>
  );
};

export default HeaderBar;
