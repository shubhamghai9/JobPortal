import { useState, useEffect } from 'react';
import { AuthContext } from './AuthContext';
import { login as loginService, refreshToken } from '../api/authService';

const getUserFromToken = (token, role) => {
  if (!token || !role) return null;
  return { token, role };
};

export const AuthProvider = ({ children }) => {
  const [user, setUser] = useState(null);

  useEffect(() => {
    const token = localStorage.getItem('jwtToken');
    const role = localStorage.getItem('role');
    const currentUser = getUserFromToken(token, role);
    if (currentUser) {
      setUser(currentUser);
    }
  }, []);

  const login = async (username, password) => {
    const data = await loginService(username, password);
    localStorage.setItem('jwtToken', data.token);
    localStorage.setItem('refreshToken', data.refreshToken);
    localStorage.setItem('role', data.role);
    setUser({ token: data.token, role: data.role });
  };

  const logout = () => {
    localStorage.clear();
    setUser(null);
  };

  const refresh = async () => {
    const data = await refreshToken();
    localStorage.setItem('jwtToken', data.token);
    setUser((prev) => ({ ...prev, token: data.token }));
  };

  return (
    <AuthContext.Provider value={{ user, login, logout, refresh }}>
      {children}
    </AuthContext.Provider>
  );
};
