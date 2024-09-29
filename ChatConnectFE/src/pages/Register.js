import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
const Register = () => {
    const [userName, setUserName] = useState('');
    const [password, setPassword] = useState('');
    const [email, setEmail] = useState('');
    const [error, setError] = useState(null);
    const navigate = useNavigate();
    const handleSubmit = async (event) => {
        event.preventDefault();

        try {
            const response = await axios.post('http://localhost:8080/account/register', {
                userName,
                password,
                email
            });
            if (response.data) {
                localStorage.setItem('Token', response.data);
                navigate('/login');
            } else {
                setError('Registration failed. Please try again.');
            }
        }
        catch (error) {
            if (error.response && error.response.status === 400) {
                    setError("Invalid password or username. Please try again.");
            } else {
                setError('An error occurred');
            }
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <input type="text" value={userName} onChange={(e) => setUserName(e.target.value)} placeholder="Username" required />
            <input type="password" value={password} onChange={(e) => setPassword(e.target.value)} placeholder="Password" required />
            <input type="text" value={email} onChange={(e) => setEmail(e.target.value)} placeholder="Email" />
            {error && <div>{error}</div>}
            <button type="submit">Register</button>
        </form>
    );
};

export default Register;