import React, { useState } from 'react';

const Register = () => {
    // 1. تحديد الـ Category (بشكل افتراضي Patient)
    const [category, setCategory] = useState('Patient');
    const [employeeRole, setEmployeeRole] = useState('Receptionist');

    // 2. تجميع كل البيانات في Object واحد
    const [formData, setFormData] = useState({
        fullName: '',
        email: '',
        password: '',
        phone: '',
        // Patient fields
        nationalID: '',
        gender: 0,
        dateOfBirth: '',
        address: '',
        bloodType: '',
        // Doctor fields
        specializationID: 1,
        clinicName: ''
    });

    // 3. دالة تحديث البيانات عند الكتابة
    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setFormData({
            ...formData,
            [name]: value
        });
    };

    // 4. دالة إرسال البيانات للـ Backend (ASP.NET Core)
    const handleSubmit = async (e) => {
        e.preventDefault();

        let endpoint = '';
        let payload = {};

        if (category === 'Patient') {
            endpoint = 'http://localhost:5000/api/patientauth/register';
            payload = {
                fullName: formData.fullName,
                email: formData.email,
                password: formData.password,
                nationalID: formData.nationalID,
                phone: formData.phone || null,
                gender: formData.gender,
                dateOfBirth: formData.dateOfBirth || null,
                address: formData.address || null,
                bloodType: formData.bloodType || null
            };
        } else if (category === 'Doctor') {
            endpoint = 'http://localhost:5000/api/doctorauth/register';
            payload = {
                fullName: formData.fullName,
                email: formData.email,
                password: formData.password,
                specializationID: formData.specializationID,
                phone: formData.phone || null,
                clinicName: formData.clinicName || null
            };
        } else {
            // Employee
            endpoint = 'http://localhost:5000/api/auth/register';
            const roleMap = {
                'Admin': 1,
                'Technician': 2,
                'Receptionist': 3
            };
            payload = {
                fullName: formData.fullName,
                email: formData.email,
                password: formData.password,
                phone: formData.phone,
                role: roleMap[employeeRole]
            };
        }

        try {
            const response = await fetch(endpoint, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(payload),
            });

            if (response.ok) {
                alert(`Successfully registered as a ${category === 'Employee' ? employeeRole : category}!`);
            } else {
                const errorData = await response.json();
                console.error("Errors:", errorData);
                alert("Registration failed. Please check the console for details.");
            }
        } catch (error) {
            console.error("Network Error:", error);
            alert("Could not connect to the server.");
        }
    };

    return (
        <div style={styles.container}>
            <div style={styles.card}>
                <h2 style={styles.header}>Create New Account</h2>
                
                {/* الجزء الخاص باختيار النوع (Patient / Doctor / Employee) */}
                <div style={styles.toggleWrapper}>
                    <button
                        type="button"
                        onClick={() => setCategory('Patient')}
                        style={category === 'Patient' ? styles.activeToggle : styles.toggleBtn}
                    >
                        Patient
                    </button>
                    <button
                        type="button"
                        onClick={() => setCategory('Doctor')}
                        style={category === 'Doctor' ? styles.activeToggle : styles.toggleBtn}
                    >
                        Doctor
                    </button>
                    <button
                        type="button"
                        onClick={() => setCategory('Employee')}
                        style={category === 'Employee' ? styles.activeToggle : styles.toggleBtn}
                    >
                        Employee
                    </button>
                </div>

                <form onSubmit={handleSubmit} style={styles.form}>
                    {/* الحقول الأساسية لكل المستخدمين */}
                    <div style={styles.fieldGroup}>
                        <label style={styles.label}>Full Name</label>
                        <input
                            name="fullName"
                            type="text"
                            placeholder="Enter your name"
                            style={styles.input}
                            onChange={handleInputChange}
                            required
                        />
                    </div>

                    <div style={styles.fieldGroup}>
                        <label style={styles.label}>Email Address</label>
                        <input
                            name="email"
                            type="email"
                            placeholder="example@mail.com"
                            style={styles.input}
                            onChange={handleInputChange}
                            required
                        />
                    </div>

                    <div style={styles.fieldGroup}>
                        <label style={styles.label}>Password</label>
                        <input
                            name="password"
                            type="password"
                            placeholder="********"
                            style={styles.input}
                            onChange={handleInputChange}
                            required
                        />
                    </div>

                    <div style={styles.fieldGroup}>
                        <label style={styles.label}>Phone Number (Optional)</label>
                        <input
                            name="phone"
                            type="text"
                            placeholder="01xxxxxxxxx"
                            style={styles.input}
                            onChange={handleInputChange}
                        />
                    </div>

                    {/* Patient-specific fields */}
                    {category === 'Patient' && (
                        <>
                            <div style={styles.fieldGroup}>
                                <label style={styles.label}>National ID *</label>
                                <input
                                    name="nationalID"
                                    type="text"
                                    placeholder="National ID"
                                    style={styles.input}
                                    onChange={handleInputChange}
                                    required
                                />
                            </div>
                            <div style={styles.fieldGroup}>
                                <label style={styles.label}>Phone (Optional)</label>
                                <input
                                    name="phone"
                                    type="text"
                                    placeholder="Phone Number"
                                    style={styles.input}
                                    onChange={handleInputChange}
                                />
                            </div>
                            <div style={styles.fieldGroup}>
                                <label style={styles.label}>Gender (Optional)</label>
                                <select
                                    name="gender"
                                    value={formData.gender}
                                    onChange={handleInputChange}
                                    style={styles.input}
                                >
                                    <option value="0">Male</option>
                                    <option value="1">Female</option>
                                </select>
                            </div>
                            <div style={styles.fieldGroup}>
                                <label style={styles.label}>Date of Birth (Optional)</label>
                                <input
                                    name="dateOfBirth"
                                    type="date"
                                    style={styles.input}
                                    onChange={handleInputChange}
                                />
                            </div>
                            <div style={styles.fieldGroup}>
                                <label style={styles.label}>Address (Optional)</label>
                                <input
                                    name="address"
                                    type="text"
                                    placeholder="Address"
                                    style={styles.input}
                                    onChange={handleInputChange}
                                />
                            </div>
                            <div style={styles.fieldGroup}>
                                <label style={styles.label}>Blood Type (Optional)</label>
                                <input
                                    name="bloodType"
                                    type="text"
                                    placeholder="A+, B+, O-, etc."
                                    style={styles.input}
                                    onChange={handleInputChange}
                                />
                            </div>
                        </>
                    )}

                    {/* Doctor-specific fields */}
                    {category === 'Doctor' && (
                        <>
                            <div style={styles.fieldGroup}>
                                <label style={styles.label}>Specialization ID *</label>
                                <input
                                    name="specializationID"
                                    type="number"
                                    value={formData.specializationID}
                                    style={styles.input}
                                    onChange={handleInputChange}
                                    required
                                />
                            </div>
                            <div style={styles.fieldGroup}>
                                <label style={styles.label}>Phone (Optional)</label>
                                <input
                                    name="phone"
                                    type="text"
                                    placeholder="Phone Number"
                                    style={styles.input}
                                    onChange={handleInputChange}
                                />
                            </div>
                            <div style={styles.fieldGroup}>
                                <label style={styles.label}>Clinic Name (Optional)</label>
                                <input
                                    name="clinicName"
                                    type="text"
                                    placeholder="Clinic Name"
                                    style={styles.input}
                                    onChange={handleInputChange}
                                />
                            </div>
                        </>
                    )}

                    {/* Employee-specific fields */}
                    {category === 'Employee' && (
                        <div style={styles.fieldGroup}>
                            <label style={styles.label}>Employee Role</label>
                            <select
                                value={employeeRole}
                                onChange={(e) => setEmployeeRole(e.target.value)}
                                style={styles.input}
                                required
                            >
                                <option value="Admin">Admin</option>
                                <option value="Technician">Technician</option>
                                <option value="Receptionist">Receptionist</option>
                            </select>
                        </div>
                    )}

                    <button type="submit" style={styles.submitButton}>
                        Register as {category === 'Employee' ? employeeRole : category}
                    </button>
                </form>

            </div>
        </div>
    );
};

// الستايلات (تقدري تنقليها لملف CSS منفصل لو حبيتي)
const styles = {
    container: {
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center',
        minHeight: '100vh',
        padding: '20px',
        background: 'transparent'
    },
    card: {
        backgroundColor: 'rgba(4,17,30,0.95)',
        padding: '34px',
        borderRadius: '24px',
        boxShadow: '0 30px 90px rgba(0,0,0,0.35)',
        width: '100%',
        maxWidth: '520px',
        color: '#e8f4f0',
        border: '1px solid rgba(11,206,170,0.18)'
    },
    header: {
        textAlign: 'center',
        marginBottom: '25px',
        color: '#f4fbfa'
    },
    toggleWrapper: {
        display: 'flex',
        backgroundColor: 'rgba(255,255,255,0.05)',
        borderRadius: '12px',
        padding: '6px',
        marginBottom: '30px',
        border: '1px solid rgba(11,206,170,0.14)'
    },
    toggleBtn: {
        flex: 1,
        padding: '12px',
        border: 'none',
        borderRadius: '10px',
        cursor: 'pointer',
        backgroundColor: 'transparent',
        transition: '0.3s',
        fontSize: '16px',
        color: '#d7f3f0'
    },
    activeToggle: {
        flex: 1,
        padding: '12px',
        border: 'none',
        borderRadius: '10px',
        cursor: 'pointer',
        backgroundColor: 'rgba(11,206,170,0.95)',
        color: '#04111e',
        fontWeight: '700',
        fontSize: '16px'
    },
    form: {
        display: 'flex',
        flexDirection: 'column'
    },
    fieldGroup: {
        marginBottom: '15px'
    },
    label: {
        display: 'block',
        marginBottom: '5px',
        fontSize: '14px',
        color: '#a7dcd7'
    },
    input: {
        width: '100%',
        padding: '14px',
        borderRadius: '12px',
        border: '1px solid rgba(11,206,170,0.2)',
        fontSize: '16px',
        boxSizing: 'border-box',
        color: '#e8f4f0',
        backgroundColor: 'rgba(255,255,255,0.04)'
    },
    doctorSection: {
        marginTop: '10px',
        padding: '18px',
        backgroundColor: 'rgba(255,255,255,0.04)',
        borderRadius: '16px',
        borderLeft: '4px solid rgba(11,206,170,0.8)'
    },
    subHeader: {
        margin: '0 0 15px 0',
        fontSize: '16px',
        color: '#cbf6f0'
    },
    submitButton: {
        marginTop: '20px',
        padding: '14px',
        backgroundColor: '#0bceaa',
        color: '#04111e',
        border: 'none',
        borderRadius: '12px',
        fontSize: '18px',
        fontWeight: '700',
        cursor: 'pointer',
        transition: 'background 0.3s'
    }
};

export default Register;