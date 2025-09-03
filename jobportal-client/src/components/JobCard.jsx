import { Card, CardContent, Typography, CardActions, Button } from "@mui/material";

export default function JobCard({ job, user, onEdit, onDelete, onView }) {
  return (
    <Card sx={{ borderRadius: 3, boxShadow: 4, height: "100%", display: "flex", flexDirection: "column" }}>
      <CardContent sx={{ flexGrow: 1 }}>
        <Typography variant="h6" fontWeight="bold" gutterBottom>
          {job.title}
        </Typography>

        <Typography variant="subtitle2" color="text.secondary">
          {job.company}
        </Typography>
        <Typography variant="body2" color="text.secondary" gutterBottom>
          📍 {job.location}
        </Typography>

        <Typography
          variant="body2"
          sx={{ mt: 1, mb: 1 }}
          noWrap // truncate long descriptions
        >
          {job.description}
        </Typography>

        <Typography variant="caption" color="text.secondary">
          Posted: {new Date(job.postedDate).toLocaleDateString()}
        </Typography>
      </CardContent>

      <CardActions sx={{ justifyContent: "flex-end", p: 2 }}>
        <Button size="small" onClick={onView}>
          View Details
        </Button>
        {user?.role === "Admin" && (
          <>
            <Button variant="contained" size="small" onClick={onEdit}>
              Edit
            </Button>
            <Button variant="outlined" color="error" size="small" onClick={onDelete}>
              Delete
            </Button>
          </>
        )}
      </CardActions>
    </Card>
  );
}
