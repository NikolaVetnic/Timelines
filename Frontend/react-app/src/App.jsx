import {
  createBrowserRouter,
  createRoutesFromElements,
  Route,
  RouterProvider,
} from "react-router-dom";
import "react-toastify/dist/ReactToastify.css";
import AppLayout from "./components/AppLayout/AppLayout";
import PhysicalPersonDetails from "./components/Timelines/PhysicalPersonDetails/PhysicalPersonDetails";
import Timeline from "./components/Timelines/Timeline/Timeline";
import { AuthProvider } from "./context/AuthContext";
import AuthGuard from "./core/auth/AuthGuard";
import ErrorPage from "./pages/ErrorPage";
import HomePage from "./pages/HomePage";
import LoginPage from "./pages/LoginPage";
import "./styles/App.css";


const router = createBrowserRouter(
  createRoutesFromElements(
    <Route errorElement={<ErrorPage />}>
  <Route path="/login" element={<LoginPage />} />
  <Route
    path="/"
    element={<AuthGuard><AppLayout /></AuthGuard>}
    handle={{ 
      crumb: { 
        title: "Home",
        static: true
      } 
    }}
  >
    <Route index element={<HomePage />} />
    
    <Route
      path="timelines/:id"
      element={<Timeline />}
      handle={{ 
        crumb: ({ params }) => ({
          title: "Timeline",
          dynamic: true,
          idParam: 'id'
        }) 
      }}
    >
     <Route
  path="physical-persons/:personId"
  element={<PhysicalPersonDetails />}
  handle={{
    crumb: ({ params }) => ({
      title: params.personId, // This will be replaced by the actual name
      dynamic: true,
      personIdParam: 'personId',
      type: 'person' // Add this to identify person crumbs
    })
  }}
/>
    </Route>
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
