import axios from 'axios';
import authHeader from './authHeader';
import { refreshToken as refreshTokenService } from './authService';

const api = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL,
});

// Add JWT to all requests
api.interceptors.request.use(
  (config) => {
    config.headers = { ...config.headers, ...authHeader() };
    return config;
  },
  (error) => Promise.reject(error)
);

// Handle expired tokens
api.interceptors.response.use(
  (response) => response,
  async (error) => {
    const originalRequest = error.config;

    if (error.response?.status === 401 && !originalRequest._retry) {
      originalRequest._retry = true;

      try {
        const data = await refreshTokenService();
        localStorage.setItem('jwtToken', data.token);

        // Retry with new token
        originalRequest.headers['Authorization'] = `Bearer ${data.token}`;
        return api(originalRequest);
      } catch (refreshError) {
        console.error('Refresh token failed:', refreshError);
        localStorage.clear();
        window.location.href = '/login';
      }
    }

    return Promise.reject(error);
  }
);

export default api;
