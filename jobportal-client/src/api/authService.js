import axios from 'axios';

const API_URL = 'https://localhost:7160/api/v1/auth';

export const login = async (username, password) => {
  const response = await axios.post(`${API_URL}/login`, { username, password });
  return response.data; // { token, refreshToken, role }
};

export const refreshToken = async () => {
  const storedRefreshToken = localStorage.getItem('refreshToken');
  const response = await axios.post(`${API_URL}/refresh`, {
    refreshToken: storedRefreshToken,
  });
  return response.data; // { token }
};
