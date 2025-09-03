import api from './axiosInstance';

export const getJobs = async (page = 1, pageSize = 10, search = '') => {
  const response = await api.get(`/jobs`, {
    params: { page, pageSize, search },
  });
  return response.data;
};

export const getJobById = async (id) => {
  const response = await api.get(`/jobs/${id}`);
  return response.data;
};

export const createJob = async (job) => {
  const response = await api.post(`/jobs`, job);
  return response.data;
};

export const updateJob = async (id, job) => {
  const response = await api.put(`/jobs/${id}`, job);
  return response.data;
};

export const deleteJob = async (id) => {
  const response = await api.delete(`/jobs/${id}`);
  return response.data;
};
