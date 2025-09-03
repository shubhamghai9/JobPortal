import { useState, useEffect, useCallback, useContext } from "react";
import { getJobs, deleteJob } from "../api/jobService";
import JobCard from "../components/JobCard";
import { AuthContext } from "../context/AuthContext";
import { useNavigate } from "react-router-dom";
import { Container, Grid, TextField } from "@mui/material";

export default function JobsList() {
  const [jobs, setJobs] = useState([]);
  const [search, setSearch] = useState("");
  const { user } = useContext(AuthContext);
  const navigate = useNavigate();

  const fetchJobs = useCallback(async () => {
    const data = await getJobs(1, 10, search);
    setJobs(data.data || data); // API may return { data: [] }
  }, [search]);

  useEffect(() => {
    fetchJobs();
  }, [fetchJobs]);

  const handleDelete = async (id) => {
    if (window.confirm("Are you sure you want to delete this job?")) {
      await deleteJob(id);
      fetchJobs();
    }
  };

  return (
    <Container sx={{ mt: 4 }}>
      <TextField
        fullWidth
        label="Search jobs..."
        variant="outlined"
        value={search}
        onChange={(e) => setSearch(e.target.value)}
        sx={{ mb: 3 }}
      />
      <Grid container spacing={2}>
        {jobs.map((job) => (
          <Grid item xs={12} md={6} lg={4} key={job.id}>
            <JobCard
              job={job}
              user={user}
              onView={() => navigate(`/jobs/${job.id}`)}   // 👈 View Details page
              onEdit={() => navigate(`/edit-job/${job.id}`)}
              onDelete={() => handleDelete(job.id)}
            />
          </Grid>
        ))}
      </Grid>
    </Container>
  );
}
