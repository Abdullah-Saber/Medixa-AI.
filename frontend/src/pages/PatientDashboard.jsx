import React, { useState, useEffect, useCallback } from 'react';
import { useLanguage } from '../context/LanguageContext';
import { useAuth } from '../context/AuthContext';
import { orderService, resultService } from '../services/api';

const PatientDashboard = () => {
  const { isRTL } = useLanguage();
  const { user, logout } = useAuth();
  
  const [stats, setStats] = useState({
    totalOrders: 0,
    pendingResults: 0,
    completedResults: 0,
    upcomingAppointments: 0
  });
  
  const [recentOrders, setRecentOrders] = useState([]);
  const [recentResults, setRecentResults] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const loadDashboardData = useCallback(async () => {
    try {
      setLoading(true);
      
      // Load patient's orders
      const orders = await orderService.getByPatient(user.id);
      const results = await resultService.getAll(); // Filter by patient
      
      // Calculate stats
      const pendingResults = results.filter(r => !r.resultText).length;
      const completedResults = results.filter(r => r.resultText).length;
      
      setStats({
        totalOrders: orders.length,
        pendingResults,
        completedResults,
        upcomingAppointments: 0 // TODO: Implement when appointment service is ready
      });
      
      // Get recent orders (last 5)
      const sortedOrders = orders
        .sort((a, b) => new Date(b.orderDate) - new Date(a.orderDate))
        .slice(0, 5);
      setRecentOrders(sortedOrders);
      
      // Get recent results (last 5)
      const sortedResults = results
        .sort((a, b) => new Date(b.resultDate) - new Date(a.resultDate))
        .slice(0, 5);
      setRecentResults(sortedResults);
      
    } catch (err) {
      console.error('Failed to load dashboard data:', err);
      setError('Failed to load dashboard data');
    } finally {
      setLoading(false);
    }
  }, [user.id]);

  useEffect(() => {
    loadDashboardData();
  }, [loadDashboardData]);

  
  const formatDate = (dateString) => {
    return new Date(dateString).toLocaleDateString();
  };

  const getStatusColor = (status) => {
    switch (status) {
      case 'Completed':
        return '#27ae60';
      case 'Pending':
        return '#f39c12';
      case 'InProgress':
        return '#3498db';
      default:
        return '#95a5a6';
    }
  };

  if (loading) {
    return (
      <div style={{ ...styles.container, direction: isRTL ? 'rtl' : 'ltr' }}>
        <div style={styles.loading}>Loading dashboard...</div>
      </div>
    );
  }

  if (error) {
    return (
      <div style={{ ...styles.container, direction: isRTL ? 'rtl' : 'ltr' }}>
        <div style={styles.error}>
          {error}
          <button onClick={loadDashboardData} style={styles.retryBtn}>
            Retry
          </button>
        </div>
      </div>
    );
  }

  return (
    <div style={{ ...styles.container, direction: isRTL ? 'rtl' : 'ltr' }}>
      {/* Header */}
      <header style={styles.header}>
        <div style={styles.headerContent}>
          <div>
            <h1 style={styles.title}>Patient Dashboard</h1>
            <p style={styles.subtitle}>
              Welcome back, <strong>{user?.name}</strong>
            </p>
          </div>
          <button onClick={logout} style={styles.logoutBtn}>
            Logout
          </button>
        </div>
      </header>

      {/* Stats Grid */}
      <section style={styles.statsSection}>
        <div style={styles.statsGrid}>
          <div style={styles.statCard}>
            <div style={styles.statIcon}>📋</div>
            <div style={styles.statValue}>{stats.totalOrders}</div>
            <div style={styles.statLabel}>Total Orders</div>
          </div>
          <div style={styles.statCard}>
            <div style={styles.statIcon}>⏳</div>
            <div style={styles.statValue}>{stats.pendingResults}</div>
            <div style={styles.statLabel}>Pending Results</div>
          </div>
          <div style={styles.statCard}>
            <div style={styles.statIcon}>✅</div>
            <div style={styles.statValue}>{stats.completedResults}</div>
            <div style={styles.statLabel}>Completed Results</div>
          </div>
          <div style={styles.statCard}>
            <div style={styles.statIcon}>📅</div>
            <div style={styles.statValue}>{stats.upcomingAppointments}</div>
            <div style={styles.statLabel}>Appointments</div>
          </div>
        </div>
      </section>

      {/* Main Content */}
      <div style={styles.mainGrid}>
        {/* Recent Orders */}
        <section style={styles.section}>
          <h2 style={styles.sectionTitle}>Recent Orders</h2>
          <div style={styles.listContainer}>
            {recentOrders.length === 0 ? (
              <div style={styles.emptyState}>No orders found</div>
            ) : (
              recentOrders.map((order) => (
                <div key={order.orderId} style={styles.listItem}>
                  <div style={styles.itemHeader}>
                    <span style={styles.itemId}>Order #{order.orderId?.toString().substring(0, 8)}</span>
                    <span 
                      style={{ 
                        ...styles.statusBadge, 
                        backgroundColor: getStatusColor(order.status) 
                      }}
                    >
                      {order.status}
                    </span>
                  </div>
                  <div style={styles.itemDetails}>
                    <span>Order Date: {formatDate(order.orderDate)}</span>
                    <span>Tests: {order.orderDetails?.length || 0}</span>
                    <span>Total: ${order.totalAmount}</span>
                  </div>
                </div>
              ))
            )}
          </div>
        </section>

        {/* Recent Results */}
        <section style={styles.section}>
          <h2 style={styles.sectionTitle}>Recent Results</h2>
          <div style={styles.listContainer}>
            {recentResults.length === 0 ? (
              <div style={styles.emptyState}>No results found</div>
            ) : (
              recentResults.map((result) => (
                <div key={result.resultId} style={styles.listItem}>
                  <div style={styles.itemHeader}>
                    <span style={styles.itemId}>Result #{result.resultId?.toString().substring(0, 8)}</span>
                    <span 
                      style={{ 
                        ...styles.statusBadge, 
                        backgroundColor: result.resultText ? '#27ae60' : '#f39c12' 
                      }}
                    >
                      {result.resultText ? 'Completed' : 'Pending'}
                    </span>
                  </div>
                  <div style={styles.itemDetails}>
                    <span>Test Date: {formatDate(result.resultDate)}</span>
                    <span>Value: {result.resultValue}</span>
                    {result.resultText && (
                      <span style={styles.resultText}>{result.resultText.substring(0, 50)}...</span>
                    )}
                  </div>
                </div>
              ))
            )}
          </div>
        </section>
      </div>

      {/* Quick Actions */}
      <section style={styles.actionsSection}>
        <h2 style={styles.sectionTitle}>Quick Actions</h2>
        <div style={styles.actionButtons}>
          <button style={styles.actionBtn}>Book Appointment</button>
          <button style={styles.actionBtn}>View All Orders</button>
          <button style={styles.actionBtn}>View All Results</button>
          <button style={styles.actionBtn}>Update Profile</button>
        </div>
      </section>
    </div>
  );
};

const styles = {
  container: {
    minHeight: '100vh',
    background: 'radial-gradient(circle at top left, rgba(11,206,170,.14), transparent 22%), radial-gradient(circle at top right, rgba(11,206,170,.08), transparent 20%), linear-gradient(180deg, #03111c 0%, #04111e 100%)',
    padding: '20px',
    color: '#e8f4f0',
  },
  loading: {
    textAlign: 'center',
    padding: '50px',
    fontSize: '18px',
    color: '#0bceaa',
  },
  error: {
    textAlign: 'center',
    padding: '50px',
    fontSize: '16px',
    color: '#e74c3c',
  },
  retryBtn: {
    marginLeft: '10px',
    padding: '8px 16px',
    backgroundColor: '#0bceaa',
    color: 'white',
    border: 'none',
    borderRadius: '4px',
    cursor: 'pointer',
  },
  header: {
    marginBottom: '30px',
    padding: '20px 0',
    borderBottom: '1px solid rgba(11,206,170,0.2)',
  },
  headerContent: {
    display: 'flex',
    justifyContent: 'space-between',
    alignItems: 'center',
    flexWrap: 'wrap',
    gap: '15px',
  },
  title: {
    fontSize: '28px',
    fontWeight: '700',
    margin: '0 0 8px 0',
    color: '#effcfb',
  },
  subtitle: {
    fontSize: '14px',
    color: 'rgba(232,244,240,0.7)',
    margin: 0,
  },
  logoutBtn: {
    padding: '10px 24px',
    background: '#e74c3c',
    color: 'white',
    border: 'none',
    borderRadius: '8px',
    cursor: 'pointer',
    fontWeight: '600',
    fontSize: '14px',
  },
  statsSection: {
    marginBottom: '30px',
  },
  statsGrid: {
    display: 'grid',
    gridTemplateColumns: 'repeat(auto-fit, minmax(200px, 1fr))',
    gap: '20px',
  },
  statCard: {
    background: 'rgba(255,255,255,0.04)',
    border: '1px solid rgba(11,206,170,0.2)',
    borderRadius: '12px',
    padding: '24px',
    textAlign: 'center',
  },
  statIcon: {
    fontSize: '32px',
    marginBottom: '12px',
  },
  statValue: {
    fontSize: '28px',
    fontWeight: '700',
    color: '#0bceaa',
    marginBottom: '4px',
  },
  statLabel: {
    fontSize: '14px',
    color: 'rgba(232,244,240,0.7)',
  },
  mainGrid: {
    display: 'grid',
    gridTemplateColumns: '1fr 1fr',
    gap: '24px',
    marginBottom: '30px',
  },
  section: {
    background: 'rgba(255,255,255,0.04)',
    border: '1px solid rgba(11,206,170,0.2)',
    borderRadius: '12px',
    padding: '24px',
  },
  sectionTitle: {
    fontSize: '18px',
    fontWeight: '600',
    margin: '0 0 20px 0',
    color: '#effcfb',
  },
  listContainer: {
    display: 'flex',
    flexDirection: 'column',
    gap: '12px',
  },
  listItem: {
    background: 'rgba(255,255,255,0.03)',
    border: '1px solid rgba(11,206,170,0.1)',
    borderRadius: '8px',
    padding: '16px',
  },
  itemHeader: {
    display: 'flex',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginBottom: '8px',
  },
  itemId: {
    fontWeight: '600',
    color: '#0bceaa',
  },
  statusBadge: {
    padding: '4px 8px',
    borderRadius: '4px',
    fontSize: '12px',
    fontWeight: '600',
    color: 'white',
  },
  itemDetails: {
    display: 'flex',
    flexDirection: 'column',
    gap: '4px',
    fontSize: '14px',
    color: 'rgba(232,244,240,0.7)',
  },
  resultText: {
    color: '#27ae60',
    fontStyle: 'italic',
  },
  emptyState: {
    textAlign: 'center',
    padding: '40px',
    color: 'rgba(232,244,240,0.5)',
    fontStyle: 'italic',
  },
  actionsSection: {
    background: 'rgba(255,255,255,0.04)',
    border: '1px solid rgba(11,206,170,0.2)',
    borderRadius: '12px',
    padding: '24px',
  },
  actionButtons: {
    display: 'grid',
    gridTemplateColumns: 'repeat(auto-fit, minmax(200px, 1fr))',
    gap: '12px',
  },
  actionBtn: {
    padding: '14px 20px',
    background: 'rgba(11,206,170,0.1)',
    border: '1px solid rgba(11,206,170,0.3)',
    borderRadius: '8px',
    color: '#0bceaa',
    cursor: 'pointer',
    fontSize: '14px',
    fontWeight: '500',
    transition: 'all 0.3s ease',
  },
};

export default PatientDashboard;
