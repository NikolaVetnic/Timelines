import { useEffect, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { useAuth } from "../../context/AuthContext";

const AuthGuard = ({ children }) => {
  const { isAuthenticated, isLoading } = useAuth();
  const navigate = useNavigate();
  const location = useLocation();
  const [initialCheckDone, setInitialCheckDone] = useState(false);

  useEffect(() => {
    if (!isLoading) {
      if (!isAuthenticated()) {
        navigate("/login", { 
          replace: true,
          state: { from: location } 
        });
      } else {
        setInitialCheckDone(true);
      }
    }
  }, [isAuthenticated, isLoading, navigate, location]);

  if (isLoading || !initialCheckDone) {
    return <div>Loading...</div>;
  }

  return children;
};

export default AuthGuard;
