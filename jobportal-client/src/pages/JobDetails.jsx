import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { getJobById } from "../api/jobService";
import {
  Container,
  Card,
  CardContent,
  Typography,
  Button,
  Box,
  Divider,
  CircularProgress,
} from "@mui/material";

export default function JobDetails() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [job, setJob] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchJob = async () => {
      try {
        const data = await getJobById(id);
        setJob(data);
      } catch (error) {
        console.error("Failed to load job:", error);
      } finally {
        setLoading(false);
      }
    };
    fetchJob();
  }, [id]);

  if (loading) {
    return (
      <Box sx={{ display: "flex", justifyContent: "center", mt: 5 }}>
        <CircularProgress />
      </Box>
    );
  }

  if (!job) {
    return (
      <Container sx={{ mt: 5 }}>
        <Typography variant="h6" color="error">
          Job not found.
        </Typography>
      </Container>
    );
  }

  return (
    <Container sx={{ mt: 4 }}>
      <Card sx={{ borderRadius: 3, boxShadow: 4, p: 2 }}>
        <CardContent>
          <Typography variant="h4" fontWeight="bold" gutterBottom>
            {job.title}
          </Typography>

          <Typography variant="h6" color="text.secondary" gutterBottom>
            {job.company}
          </Typography>

          <Typography variant="subtitle1" gutterBottom>
            📍 {job.location}
          </Typography>

          <Divider sx={{ my: 2 }} />

          <Typography variant="body1" paragraph>
            {job.description}
          </Typography>

          <Typography variant="caption" display="block" color="text.secondary">
            Posted: {new Date(job.postedDate).toLocaleDateString()}
          </Typography>
        </CardContent>

        <Box sx={{ display: "flex", justifyContent: "space-between", p: 2 }}>
          <Button variant="outlined" onClick={() => navigate(-1)}>
            Back
          </Button>
          <Button
            variant="contained"
            color="primary"
            onClick={() => alert("Apply functionality coming soon!")}
          >
            Apply Now
          </Button>
        </Box>
      </Card>
    </Container>
  );
}
