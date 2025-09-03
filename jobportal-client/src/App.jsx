import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { AuthProvider } from "./context/AuthProvider";
import Navbar from "./components/Navbar";
import ProtectedRoute from "./components/ProtectedRoute";
import JobsList from "./pages/JobsList";
import JobDetails from "./pages/JobDetails";
import CreateJob from "./pages/CreateJob";
import EditJob from "./pages/EditJob";
import Login from "./pages/Login";
import ResumeAnalyzer from "./pages/ResumeAnalyzer"; 

import { ThemeProvider, createTheme, CssBaseline, Container } from "@mui/material";

function App() {
  // ✅ Define Material UI theme
  const theme = createTheme({
    palette: {
      mode: "light", // change to "dark" if you want a dark theme
      primary: {
        main: "#1976d2", // blue
      },
      secondary: {
        main: "#9c27b0", // purple
      },
    },
    shape: {
      borderRadius: 10,
    },
    typography: {
      fontFamily: "Roboto, Arial, sans-serif",
    },
  });

  return (
    <AuthProvider>
      <ThemeProvider theme={theme}>
        <CssBaseline />
        <Router>
          <Navbar />
          <Container sx={{ mt: 4, mb: 4 }}>
            <Routes>
              <Route path="/" element={<JobsList />} />
              <Route path="/jobs/:id" element={<JobDetails />} />
              <Route path="/login" element={<Login />} />

              {/* Admin Only */}
              <Route
                path="/create-job"
                element={
                  <ProtectedRoute roles={["Admin"]}>
                    <CreateJob />
                  </ProtectedRoute>
                }
              />
              <Route
                path="/edit-job/:id"
                element={
                  <ProtectedRoute roles={["Admin"]}>
                    <EditJob />
                  </ProtectedRoute>
                }
              />
              <Route
                path="/resume-analyzer"
                element={
                  <ProtectedRoute roles={["Admin", "User"]}>
                    <ResumeAnalyzer />
                  </ProtectedRoute>
                }
              />
            </Routes>
          </Container>
        </Router>
      </ThemeProvider>
    </AuthProvider>
  );
}

export default App;
