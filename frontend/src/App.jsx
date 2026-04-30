import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { useEffect } from 'react';
import { AuthProvider } from './context/AuthContext';
import Navbar       from './components/Navbar';
import HomePage     from './pages/HomePage';
import LoginPage    from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';
import AboutPage    from './pages/AboutPage';
import ServicesPage from './pages/ServicesPage';
import BookAppointmentPage from './pages/BookAppointmentPage';
import AdminDashboard from './pages/AdminDashboard';
import PatientDashboard from './pages/PatientDashboard';
import DoctorDashboard from './pages/DoctorDashboard';
import SimpleApiTest from './components/SimpleApiTest';
import AuthTest from './components/AuthTest';
import DashboardTest from './components/DashboardTest';
import AdminRoute from './components/auth/AdminRoute';
import PatientRoute from './components/auth/PatientRoute';
import DoctorRoute from './components/auth/DoctorRoute';

function AppContent() {
  useEffect(() => {
    document.documentElement.setAttribute('data-theme', 'dark');
  }, []);

  return (
    <>
      <Navbar />
      <Routes>
        <Route path="/"         element={<HomePage />} />
        <Route path="/login"    element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />
        <Route path="/about"             element={<AboutPage />} />
        <Route path="/services"           element={<ServicesPage />} />
        <Route path="/book-appointment"   element={<BookAppointmentPage />} />
        <Route path="/api-test" element={<SimpleApiTest />} />
<Route path="/auth-test" element={<AuthTest />} />
<Route path="/dashboard-test" element={<DashboardTest />} />
        <Route path="/admin" element={<AdminRoute><AdminDashboard /></AdminRoute>} />
        <Route path="/patient" element={<PatientRoute><PatientDashboard /></PatientRoute>} />
        <Route path="/doctor" element={<DoctorRoute><DoctorDashboard /></DoctorRoute>} />
      </Routes>
    </>
  );
}

export default function App() {
  return (
    <AuthProvider>
      <BrowserRouter>
        <AppContent />
      </BrowserRouter>
    </AuthProvider>
  );
}
