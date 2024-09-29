import { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

export const useAuth = () => {
    const navigate = useNavigate();

    useEffect(() => {
        const token = localStorage.getItem('Token');

        if (!token) {
            navigate('/login');
        }
    }, [navigate]);
};