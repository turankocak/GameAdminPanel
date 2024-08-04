import React, { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import userService from '../services/userService';
import '../assets/styles/RegisterPage.css';

const RegisterPage = () => {
    const [email, setEmail] = useState('');
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [alertMessage, setAlertMessage] = useState('');
    const [alertType, setAlertType] = useState(''); 
    const navigate = useNavigate();

    const handleRegister = async (e) => {
        e.preventDefault();
        const userData = {
            emailAddress: email,
            username,
            password
        };
        try {
            await userService.register(userData);
            setAlertMessage('Kayıt başarılı!');
            setAlertType('success'); 
            navigate('/login');
        } catch (error) {
            let errorMessage = 'Kayıt başarısız.';
            if (error.response && error.response.data && error.response.data.errors) {
                const errors = error.response.data.errors;
                if (errors.Username) {
                    errorMessage = errors.Username.join(' ');
                } else if (errors.Password) {
                    errorMessage = errors.Password.join(' ');
                } else if (errors.EmailAddress) {
                    errorMessage = errors.EmailAddress.join(' ');
                }
            } else if (error.response && error.response.data && error.response.data.title) {
                errorMessage = error.response.data.title;
            } else if (error.message) {
                errorMessage = error.message;
            }
            setAlertMessage(errorMessage);
            setAlertType('error'); 
        }
    };

    return (
        <div className="register-container">
            <form className="register-form" onSubmit={handleRegister}>
                {alertMessage && (
                    <p className={`alert-message ${alertType}`}>
                        {alertMessage}
                    </p>
                )}
                <input
                    type="email"
                    placeholder="E-posta"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    required
                    className="register-input"
                />
                <input
                    type="text"
                    placeholder="Kullanıcı Adı"
                    value={username}
                    onChange={(e) => setUsername(e.target.value)}
                    required
                    className="register-input"
                />
                <input
                    type="password"
                    placeholder="Şifre"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    required
                    className="register-input"
                />
                <button type="submit" className="register-button">Kayıt Ol</button>
                <p>Zaten bir hesabınız var mı? <Link to="/login">Giriş Yapın</Link></p>
            </form>
        </div>
    );
};

export default RegisterPage;
