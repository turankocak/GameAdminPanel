import axios from 'axios';

const API_URL = 'http://localhost:5113/api'; 

// Kullanıcı kaydını gerçekleştirir
const register = async (userData) => {
    try {
        console.log('Kayıt işlemi gerçekleştiriliyor:', userData);
        const response = await axios.post(`${API_URL}/user/register`, userData);
        console.log('API yanıtı:', response.data);
        return response.data;
    } catch (error) {
        if (error.response) {
            console.error('API kayıt hatası:', error.response.data);
            throw new Error(error.response.data.title || 'Kullanıcı adı veya e-posta zaten kullanılıyor');
        } else if (error.request) {
            console.error('API kayıt isteği hatası:', error.request);
            throw new Error('Sunucu yanıt vermedi');
        } else {
            console.error('API kayıt bilinmeyen hatası:', error.message);
            throw new Error('Bilinmeyen bir hata oluştu');
        }
    }
};

// Kullanıcı girişini gerçekleştirir
const login = async (username, password) => {
    try {
        console.log('Giriş işlemi gerçekleştiriliyor:', { username, password });
        const response = await axios.post(`${API_URL}/user/login`, {
            username,
            password
        });
        console.log('API yanıtı:', response.data);
        return response.data;
    } catch (error) {
        if (error.response) {
            console.error('API giriş hatası:', error.response.data);
            throw new Error(error.response.data.title || 'Kullanıcı adı veya şifre hatalı');
        } else if (error.request) {
            console.error('API giriş isteği hatası:', error.request);
            throw new Error('Sunucu yanıt vermedi');
        } else {
            console.error('API giriş bilinmeyen hatası:', error.message);
            throw new Error('Bilinmeyen bir hata oluştu');
        }
    }
};

const userService = {
    register,
    login
};

export default userService;
