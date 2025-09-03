import api from "./axiosInstance";

const ResumeService = {
  analyzeResume: async (file) => {
    const formData = new FormData();
    formData.append("file", file);

    const response = await api.post("/resume/analyze", formData, {
      headers: {
        "Content-Type": "multipart/form-data",
      },
    });

    return response.data;
  },
};

export default ResumeService;
