import {
  createBrowserRouter,
  createRoutesFromElements,
  Route,
  RouterProvider,
} from "react-router-dom";
import "react-toastify/dist/ReactToastify.css";
import AppLayout from "./components/AppLayout/AppLayout";
import ErrorPage from "./components/Pages/ErrorPage";
import LoginPage from "./components/Pages/LoginPage";
import Timeline from "./components/Timelines/Timeline/Timeline";
import { AuthProvider } from "./context/AuthContext";
import AuthGuard from "./core/auth/AuthGuard";
import TimelineList from "./core/components/lists/TimelineList/TimelineList";
import "./styles/App.css";


const router = createBrowserRouter(
  createRoutesFromElements(
    <Route errorElement={<ErrorPage />}>
      <Route path="/login" element={<LoginPage />} />

      <Route
        path="/"
        element={
          <AuthGuard>
            <AppLayout />
          </AuthGuard>
        }
      >
        <Route index element={<TimelineList />} />
        <Route path="timelines/:id" element={<Timeline />} />
      </Route>

      <Route path="*" element={<ErrorPage />} />
    </Route>
  )
);

function App() {
  return (
    <AuthProvider>
      <RouterProvider router={router} />
    </AuthProvider>
  );
}

export default App;
