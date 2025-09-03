// src/components/JobForm.jsx
import { useState, useEffect } from "react";
import { TextField, Button, Box, Paper, Typography } from "@mui/material";
import { createJob, getJobById, updateJob } from "../api/jobService";
import { useNavigate, useParams } from "react-router-dom";

export default function JobForm({ mode = "create" }) {
  const [title, setTitle] = useState("");
  const [company, setCompany] = useState("");
  const [location, setLocation] = useState("");
  const [description, setDescription] = useState("");

  const navigate = useNavigate();
  const { id } = useParams();

  // Load job data if editing
  useEffect(() => {
    if (mode === "edit" && id) {
      (async () => {
        const job = await getJobById(id);
        setTitle(job.title || "");
        setCompany(job.company || "");
        setLocation(job.location || "");
        setDescription(job.description || "");
      })();
    }
  }, [mode, id]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (mode === "create") {
      await createJob({ title, company, location, description });
    } else {
      await updateJob(id, { title, company, location, description });
    }
    navigate("/"); // ✅ redirect to JobList after success
  };

  return (
    <Paper sx={{ p: 4, borderRadius: 2 }}>
      <Typography variant="h6" sx={{ mb: 3 }}>
        {mode === "create" ? "Create Job" : "Edit Job"}
      </Typography>
      <Box component="form" onSubmit={handleSubmit}>
        <TextField
          label="Title"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          fullWidth
          sx={{ mb: 2 }}
        />
        <TextField
          label="Company"
          value={company}
          onChange={(e) => setCompany(e.target.value)}
          fullWidth
          sx={{ mb: 2 }}
        />
        <TextField
          label="Location"
          value={location}
          onChange={(e) => setLocation(e.target.value)}
          fullWidth
          sx={{ mb: 2 }}
        />
        <TextField
          label="Description"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          fullWidth
          multiline
          rows={4}
          sx={{ mb: 2 }}
        />
        <Button type="submit" variant="contained" fullWidth>
          {mode === "create" ? "Create Job" : "Update Job"}
        </Button>
      </Box>
    </Paper>
  );
}
