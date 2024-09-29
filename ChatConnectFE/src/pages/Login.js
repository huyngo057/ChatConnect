import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import api from "../api";

const Login = () => {
    const [userName, setUserName] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    const handleSubmit = async (event) => {
        event.preventDefault();

        const response = await api.post('http://localhost:8080/account/login', {
            userName,
            password
        });

        if (response.data) {
            localStorage.setItem('Token', response.data);
            navigate('/room-list');
        } else {
            setError('Invalid username or password');
        }
    };

    const handleRegister = () => {
        navigate('/register');
    };

    return (
        <form onSubmit={handleSubmit}>
            <input type="text" value={userName} onChange={(e) => setUserName(e.target.value)} placeholder="Username" required />
            <input type="password" value={password} onChange={(e) => setPassword(e.target.value)} placeholder="Password" required />
            {error && <div>{error}</div>}
            <button type="submit">Login</button>
            <button type="button" onClick={handleRegister}>Register</button>
        </form>
    );
};

export default Login;