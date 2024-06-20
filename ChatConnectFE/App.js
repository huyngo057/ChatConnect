import React from 'react';
import {BrowserRouter, Route, Routes} from 'react-router-dom';
import ChatRoom from './src/pages/ChatRoom';
import Home from "./src/pages/Home";
import "./src/styles/App.css";
const App = () => {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/chat-room" element={<ChatRoom />} />
            </Routes>
        </BrowserRouter>
    );
}

export default App;
