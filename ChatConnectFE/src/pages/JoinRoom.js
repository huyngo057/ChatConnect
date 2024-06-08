import React, { useState } from 'react';
import {Navigate} from "react-router-dom";
import {signalRService} from "../SignalRService";

const JoinRoom = ({ onJoin }) => {
    const [roomName, setRoomName] = useState('');
    const [userName, setUserName] = useState('');
    const [isJoined, setIsJoined] = useState(false);

    const handleJoin = async () => {
        if (roomName.trim() !== '' && userName.trim() !== '') {
            await signalRService.joinRoom(roomName, userName);
            setIsJoined(true);
        }
    };
    if (isJoined) {
        return <Navigate to={`/chat-room?roomName=${roomName}&userName=${userName}`} />;
    }

    return (
        <div className="d-flex flex-column align-items-center justify-content-center vh-100 bg-light">
            <h1 className="h3 mb-3 font-weight-normal">Join a Chat Room</h1>
            <input
                className="form-control mb-3"
                type="text"
                value={roomName}
                onChange={(e) => setRoomName(e.target.value)}
                placeholder="Room Name"
            />
            <input
                className="form-control mb-3"
                type="text"
                value={userName}
                onChange={(e) => setUserName(e.target.value)}
                placeholder="Your Name"
            />
            <button
                className="btn btn-lg btn-primary btn-block"
                onClick={handleJoin}
            >
                Join Room
            </button>
        </div>
    );
};

export default JoinRoom;
