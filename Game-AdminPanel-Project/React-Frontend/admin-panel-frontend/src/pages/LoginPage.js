import React, { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import userService from '../services/userService';
import '../assets/styles/LoginPage.css';

const LoginPage = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [alertMessage, setAlertMessage] = useState('');
    const navigate = useNavigate();

    const handleLogin = async (e) => {
        e.preventDefault();
        try {
            await userService.login(username, password);
            setAlertMessage('Giriş başarılı!');
            navigate('/configurations');
        } catch (error) {
            setAlertMessage('Hata: ' + error.message);
        }
    };

    return (
        <div className="login-container">
            <form className="login-form" onSubmit={handleLogin}>
                {alertMessage && (
                    <p className="alert-message">
                        {alertMessage}
                    </p>
                )}
                <input 
                    type="text" 
                    placeholder="Kullanıcı Adı" 
                    value={username} 
                    onChange={(e) => setUsername(e.target.value)} 
                    required 
                    className="login-input"
                />
                <input 
                    type="password" 
                    placeholder="Şifre" 
                    value={password} 
                    onChange={(e) => setPassword(e.target.value)} 
                    required 
                    className="login-input"
                />
                <button type="submit" className="login-button">Giriş Yap</button>
                <p>Hesabınız yok mu? <Link to="/register">Kayıt Olun</Link></p>
            </form>
        </div>
    );
};

export default LoginPage;
