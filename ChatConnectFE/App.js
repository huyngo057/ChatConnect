import React from 'react';
import {BrowserRouter, Route, Routes} from 'react-router-dom';
import ChatRoom from './src/pages/ChatRoom';
import Home from "./src/pages/Home";
import "./src/styles/App.css";
import RoomList from "./src/pages/RoomList";
import Login from "./src/pages/Login";
import Register from "./src/pages/Register";
const App = () => {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/chat-room" element={<ChatRoom />} />
                <Route path="/room-list" element={<RoomList />} />
                <Route path="/login" element={<Login />} />
                <Route path="/register" element={<Register />} />
            </Routes>
        </BrowserRouter>
    );
}

export default App;
