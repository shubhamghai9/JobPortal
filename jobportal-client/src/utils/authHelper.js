import jwtDecode from 'jwt-decode';

export const getUserFromToken = () => {
  const token = localStorage.getItem('token');
  if (!token) return null;

  try {
    const decoded = jwtDecode(token);
    // .NET JWT typically has claims like: name, role, sub (userId)
    return {
      id: decoded.sub,
      name: decoded.name || decoded.unique_name,
      role: decoded.role || decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']
    };
  } catch (error) {
    console.error('Invalid token:', error);
    return null;
  }
};
