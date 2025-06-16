import { createContext, useCallback, useContext, useEffect, useState } from "react";
import { Post } from "../core/api/post";
import API_BASE_URL from "../data/constants";

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [user, setUser] = useState(null);
  const [token, setToken] = useState(localStorage.getItem("token"));
  const [error, setError] = useState(null);
  const [sessionExpired, setSessionExpired] = useState(false);
  const [isLoading, setIsLoading] = useState(false);

  const isAuthenticated = useCallback(() => {
    const expiresAt = localStorage.getItem("expires_at");
    const storedToken = localStorage.getItem("token");
    return !!storedToken && !!expiresAt && Date.now() < Number(expiresAt);
  }, []);

  const refreshToken = useCallback(async () => {
    try {
      const refreshToken = localStorage.getItem("refresh_token");
      if (!refreshToken) return false;

      setIsLoading(true);
      const data = await Post(API_BASE_URL, '/Auth/Token', {
        grant_type: "refresh_token",
        refresh_token: refreshToken
      }, true);

      if (data.access_token) {
        localStorage.setItem("token", data.access_token);
        setToken(data.access_token);
        
        const expiresAt = Date.now() + (data.expires_in || 3600) * 1000;
        localStorage.setItem("expires_at", expiresAt);

        return true;
      }
      return false;
    } catch (error) {
      console.error("Refresh token failed:", error);
      return false;
    } finally {
      setIsLoading(false);
    }
  }, []);

  useEffect(() => {
    const checkAuth = async () => {
      if (!token) return;
      
      const expiresAt = localStorage.getItem("expires_at");
      const isAboutToExpire = expiresAt && (Number(expiresAt) - Date.now() < 30000);
      
      if (!isAuthenticated() || isAboutToExpire) {
        const refreshed = await refreshToken();
        if (!refreshed) {
          handleLogout(true);
        }
      }
    };

    checkAuth();
    
    const interval = setInterval(checkAuth, 30000);
    return () => clearInterval(interval);
  }, [token, isAuthenticated, refreshToken]);

const login = async (credentials) => {
  try {
    setIsLoading(true);
    setError(null);
    setSessionExpired(false);

    const requestData = {
      grant_type: "password",
      username: credentials.username,
      password: credentials.password,
      scope: "openid profile email offline_access api"
    };

    const data = await Post(API_BASE_URL, '/Auth/Token', requestData, true);
    
    if (!data.access_token) {
      throw new Error(data.error_description || 'Authentication failed');
    }

    localStorage.setItem("token", data.access_token);
    localStorage.setItem("refresh_token", data.refresh_token);
    
    const expiresAt = Date.now() + data.expires_in * 1000;
    localStorage.setItem("expires_at", expiresAt);

    let userInfo = {};
    if (data.id_token) {
      try {
        const idTokenParts = data.id_token.split('.');
        if (idTokenParts.length === 3) {
          const payload = JSON.parse(
            atob(
              idTokenParts[1].replace(/-/g, '+').replace(/_/g, '/')
            )
          );
          userInfo.username = payload.username || credentials.username;
        }
      } catch (error) {
        console.error('Error decoding id_token:', error);
        userInfo.username = credentials.username;
      }
    } else {
      userInfo.username = credentials.username;
    }

    setToken(data.access_token);
    setUser(userInfo);

    return true;
  } catch (err) {
    setError(err.error_description || err.message || "Login failed");
    return false;
  } finally {
    setIsLoading(false);
  }
};

  const logout = () => {
    handleLogout(false);
  };

  const handleLogout = (expired = false) => {
    localStorage.removeItem("token");
    localStorage.removeItem("refresh_token");
    localStorage.removeItem("expires_at");
    setToken(null);
    setUser(null);
    setSessionExpired(expired);
  };

  return (
    <AuthContext.Provider
      value={{
        user,
        token,
        error,
        sessionExpired,
        isLoading,
        login,
        logout,
        isAuthenticated,
      }}
    >
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => useContext(AuthContext);
