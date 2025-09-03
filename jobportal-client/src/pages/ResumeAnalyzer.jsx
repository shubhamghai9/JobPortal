import React, { useState } from "react";
import ResumeService from "../api/resumeService";
import {
  Container,
  Typography,
  Button,
  CircularProgress,
  Paper,
  Box,
} from "@mui/material";

export default function ResumeAnalyzer() {
  const [file, setFile] = useState(null);
  const [analysis, setAnalysis] = useState("");
  const [loading, setLoading] = useState(false);

  const handleFileChange = (e) => setFile(e.target.files[0]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!file) return;

    setLoading(true);
    setAnalysis("");

    try {
      const data = await ResumeService.analyzeResume(file);
      setAnalysis(data.aiAnalysis);
    } catch (err) {
      console.error(err);
      setAnalysis("Error analyzing resume.");
    } finally {
      setLoading(false);
    }
  };

  return (
    <Container maxWidth="sm" sx={{ mt: 8 }}>
      <Paper elevation={4} sx={{ p: 4, borderRadius: 2 }}>
        <Typography variant="h5" gutterBottom>
          Resume Analyzer
        </Typography>

        <Box component="form" onSubmit={handleSubmit}>
          <input
            type="file"
            accept=".pdf,.docx,.txt"
            onChange={handleFileChange}
          />
          <Box sx={{ mt: 2 }}>
            <Button type="submit" variant="contained" disabled={loading}>
              {loading ? <CircularProgress size={24} /> : "Upload & Analyze"}
            </Button>
          </Box>
        </Box>

        {analysis && (
          <Box sx={{ mt: 3 }}>
            <Typography variant="h6">Analysis:</Typography>
            <Typography variant="body1" sx={{ whiteSpace: "pre-wrap" }}>
              {analysis}
            </Typography>
          </Box>
        )}
      </Paper>
    </Container>
  );
}
