import React, {useState} from 'react';
import {Navigate, useSearchParams} from 'react-router-dom';
import { signalRService } from '../SignalRService';
import {useAuth} from "../hooks/useAuth";
import useSignalRHandlers from "../hooks/useSignalRHandlers";

const ChatRoom = () => {
    useAuth();
    const [searchParams] = useSearchParams();
    const roomName = searchParams.get('roomName');
    const userName = searchParams.get('userName');
    const [message, setMessage] = useState('');
    const [messages, setMessages] = useState([]);
    useSignalRHandlers(roomName, userName, setMessages);

    const sendMessage = () => {
        if (message.trim() !== '') {
            signalRService.sendMessage(roomName, userName, message);
            setMessage('');
        }
    };

    const leaveRoom = async () => {
        await signalRService.leaveRoom(roomName, userName);
        window.location = '/';
    };


    if (!roomName || !userName) {
        return <Navigate to="/" />;
    }

    return (
        <div className="container">
            <h2 className="mt-4">Chat Room: {roomName}</h2>
            <ul className="list-group mb-4">
                {messages.map((msg, index) => (
                    <li key={index} className={`list-group-item ${msg.type === 'system' ? 'text-warning' : ''}`}>
                        {msg.content}
                    </li>
                ))}
            </ul>
            <div className="input-group mb-3">
                <input
                    type="text"
                    className="form-control"
                    value={message}
                    onChange={(e) => setMessage(e.target.value)}
                    placeholder="Type a message..."
                />
                <div className="input-group-append">
                    <button className="btn btn-outline-secondary" onClick={sendMessage}>Send</button>
                </div>
            </div>
            <button className="btn btn-danger" onClick={leaveRoom}>Leave Room</button>
        </div>
    );
};

export default ChatRoom;
