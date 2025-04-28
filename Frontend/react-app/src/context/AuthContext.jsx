import { createContext, useContext, useState } from "react";

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [user, setUser] = useState(null);
  const [token, setToken] = useState(localStorage.getItem("token"));
  const [error, setError] = useState(null);

  const login = async (credentials) => {
    try {
      setError(null);
      
      const mockUser = {
        id: "mock-user-123",
        username: credentials.username || "MockUser",
        name: credentials.name || "Mock User",
        role: credentials.role || "user",
      };
      
      const mockToken = `mock.${btoa(JSON.stringify({
        user: mockUser,
        exp: Math.floor(Date.now() / 1000) + 60 * 60
      }))}`;
      
      localStorage.setItem("token", mockToken);
      setToken(mockToken);
      setUser(mockUser);
      return true;
      
    } catch (err) {
      setError(err.message || "Login failed");
      return false;
    }
  };

  const logout = () => {
    localStorage.removeItem("token");
    setToken(null);
    setUser(null);
  };

  const isAuthenticated = () => {
    if (!token) return false;
    
    if (token.startsWith("mock.")) {
      try {
        const payload = JSON.parse(atob(token.split(".")[1]));
        return payload.exp > Math.floor(Date.now() / 1000);
      } catch {
        return false;
      }
    }
    
    return true;
  };

  return (
    <AuthContext.Provider
      value={{ user, token, error, login, logout, isAuthenticated }}
    >
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => useContext(AuthContext);
