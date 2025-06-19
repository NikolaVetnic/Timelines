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
    const accessTokenExpiresAt = localStorage.getItem("expires_at");
    const refreshTokenExpiresAt = localStorage.getItem("refresh_expires_at");
    const storedToken = localStorage.getItem("token");
    const refreshToken = localStorage.getItem("refresh_token");
    
    const currentTimeInSeconds = Math.floor(Date.now() / 1000);
    
    const isAccessTokenValid = !!storedToken && !!accessTokenExpiresAt && 
                             currentTimeInSeconds < Number(accessTokenExpiresAt);
    const isRefreshTokenValid = !!refreshToken && !!refreshTokenExpiresAt && 
                              currentTimeInSeconds < Number(refreshTokenExpiresAt);
    
    return isAccessTokenValid || isRefreshTokenValid;
  }, []);

  const refreshToken = useCallback(async () => {
    try {
      const refreshToken = localStorage.getItem("refresh_token");
      if (!refreshToken) {
        handleLogout(true);
        return false;
      }

      const refreshTokenExpiresAt = localStorage.getItem("refresh_expires_at");
      const currentTimeInSeconds = Math.floor(Date.now() / 1000);
      
      if (refreshTokenExpiresAt && currentTimeInSeconds >= Number(refreshTokenExpiresAt)) {
        handleLogout(true);
        return false;
      }

      setIsLoading(true);
      const data = await Post(API_BASE_URL, '/Auth/Token', {
        grant_type: "refresh_token",
        refresh_token: refreshToken
      }, true);

      if (data.access_token) {
        localStorage.setItem("token", data.access_token);
        setToken(data.access_token);
        
        const accessTokenExpiresAt = Math.floor(Date.now() / 1000) + (data.expires_in || 3600);
        localStorage.setItem("expires_at", accessTokenExpiresAt.toString());

        if (data.refresh_token) {
          localStorage.setItem("refresh_token", data.refresh_token);
          const refreshExpiresAt = Math.floor(Date.now() / 1000) + (data.refresh_expires_in || 604800);
          localStorage.setItem("refresh_expires_at", refreshExpiresAt.toString());
        }

        return true;
      }
      return false;
    } catch (error) {
      console.error("Refresh token failed:", error);
      handleLogout(true);
      return false;
    } finally {
      setIsLoading(false);
    }
  }, []);

  useEffect(() => {
    const checkAuth = async () => {
      if (!token) return;
      
      const expiresAt = localStorage.getItem("expires_at");
      const currentTimeInSeconds = Math.floor(Date.now() / 1000);
      
      const isAboutToExpire = expiresAt && (Number(expiresAt) - currentTimeInSeconds < 300);
      
      if (!isAuthenticated() || isAboutToExpire) {
        await refreshToken();
      }
    };

    checkAuth();
    
    const interval = setInterval(checkAuth, 300000);
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
      
      const currentTimeInSeconds = Math.floor(Date.now() / 1000);
      const accessTokenExpiresAt = currentTimeInSeconds + data.expires_in;
      localStorage.setItem("expires_at", accessTokenExpiresAt.toString());

      const refreshExpiresAt = currentTimeInSeconds + (data.refresh_expires_in || 604800);
      localStorage.setItem("refresh_expires_at", refreshExpiresAt.toString());

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
    localStorage.removeItem("refresh_expires_at");
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
