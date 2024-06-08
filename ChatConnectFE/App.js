import React from 'react';
import {BrowserRouter, Link, Route, Router, Routes} from 'react-router-dom';
import JoinRoom from './src/pages/JoinRoom';
import ChatRoom from './src/pages/ChatRoom';
const App = () => {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<JoinRoom />} />
                <Route path="/chat-room" element={<ChatRoom />} />
            </Routes>
        </BrowserRouter>
    );
}

export default App;
