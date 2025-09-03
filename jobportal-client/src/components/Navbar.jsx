import { AppBar, Toolbar, Typography, Button, Box } from "@mui/material";
import { Link, useNavigate } from "react-router-dom";
import { useContext } from "react";
import { AuthContext } from "../context/AuthContext";

export default function Navbar() {
  const { user, logout } = useContext(AuthContext);
  const navigate = useNavigate();

  const handleLogout = () => {
    localStorage.removeItem("user");
    localStorage.removeItem("token");
    logout();
    navigate("/login");
  };

  return (
    <AppBar position="static" sx={{ borderRadius: 0 }}>
      <Toolbar>
        <Typography
          variant="h6"
          component={Link}
          to="/"
          sx={{ flexGrow: 1, textDecoration: "none", color: "inherit" }}
        >
          Job Portal
        </Typography>

        <Box sx={{ display: "flex", alignItems: "center", gap: 2 }}>
          {user ? (
            <>
              {/* 👤 Show user name */}
              <Typography variant="body1" sx={{ mr: 2 }}>
                Hi, {user.role}
              </Typography>

              {user.role === "Admin" && (
                <Button color="inherit" onClick={() => navigate("/create-job")}>
                  Create Job
                </Button>
              )}

              {/* 📝 Resume Analyzer (visible to all logged-in users) */}
              <Button color="inherit" onClick={() => navigate("/resume-analyzer")}>
                Resume Analyzer
              </Button>
              
              <Button color="inherit" onClick={handleLogout}>
                Logout
              </Button>
            </>
          ) : (
            <Button color="inherit" onClick={() => navigate("/login")}>
              Login
            </Button>
          )}
        </Box>
      </Toolbar>
    </AppBar>
  );
}
