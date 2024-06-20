import React, {useState, useEffect} from "react";
import axios from "axios";
import {Link, Navigate} from "react-router-dom";
import {signalRService} from "../SignalRService";

const RoomList = () => {
    const [rooms, setRooms] = useState([]);
    const [roomName, setRoomName] = useState('');
    const [roomType, setRoomType] = useState('');
    const [roomDescription, setRoomDescription] = useState('');
    const [navigate, setNavigate] = useState(false);
    const [selectedRoom, setSelectedRoom] = useState(null);
    const [userName] = useState('User' + Math.floor(Math.random() * 1000));
    useEffect(() => {
        const fetchRooms = async () => {
            const response = await axios.get('http://localhost:8080/room/all');
            setRooms(response.data);
        };
        fetchRooms().catch(error => {
            console.error('There was an error!', error);
        });
    }, []);

    const createRoom = async (event) => {
        event.preventDefault();


        axios.post('http://localhost:8080/room/create', {
            name: roomName,
            type: roomType,
            description: roomDescription
        })
            .then(response => {
                setRooms([...rooms, { name: roomName, type: roomType, description: roomDescription }]);
                setRoomName('');
                setRoomType('');
                setRoomDescription('');
            })
            .catch(error => {
                console.error('There was an error!', error);
            });
    };

    const selectRoom = async (roomName) => {
        setSelectedRoom(roomName);
        await signalRService.joinRoom(roomName, userName);
        setNavigate(true);
    };


    if (navigate && selectedRoom) {
        return <Navigate to={`/chat-room?roomName=${selectedRoom}&userName=${userName}`} />;
    }


    return (
        <div className="container-fluid vh-100 w-100">
            <h1 className="text-center my-5">Welcome to ChatConnect</h1>
            <div className="card bg-body-secondary">
                <div className="card-body">
                    <h2 className="my-3">Add Room</h2>
                    <form onSubmit={createRoom} className="mb-3">
                        <div className="mb-3">
                            <input
                                type="text"
                                value={roomName}
                                onChange={(e) => setRoomName(e.target.value)}
                                placeholder="Room Name"
                                required
                                className="form-control"
                            />
                        </div>
                        <div className="mb-3">
                            <select
                                value={roomType}
                                onChange={(e) => setRoomType(e.target.value)}
                                className="form-select"
                            >
                                <option value="">Select Room Type</option>
                                <option value="Public">Public</option>
                                <option value="Private">Private</option>
                            </select>
                        </div>
                        <div className="mb-3">
                            <input
                                type="text"
                                value={roomDescription}
                                onChange={(e) => setRoomDescription(e.target.value)}
                                placeholder="Room Description"
                                className="form-control"
                            />
                        </div>
                        <button type="submit" className="btn btn-primary">Create Room</button>
                    </form>
                    <h3 className="mb-3">List Of Rooms</h3>
                    <ul className="list-group">
                        {rooms.map((room, index) => (
                            <li key={index} className="list-group-item">
                                <button className="room-button" onClick={() => selectRoom(room.name)}>{room.name}</button>
                            </li>
                        ))}
                    </ul>
                </div>
            </div>
        </div>
    );
}

export default RoomList;