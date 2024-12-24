import * as signalR from '@microsoft/signalr';

class SignalRService {
    constructor() {
        this.connection = new signalR.HubConnectionBuilder()
            .withUrl("http://localhost:7192/chat-hub")
            .withAutomaticReconnect()
            .build();

        this.connection.start()
            .then(() => console.log('Connected to SignalR hub!'))
            .catch(err => console.error('Error connecting to SignalR hub:', err));

        this.connection.on("Disconnect", () => {
            console.log("Disconnected from server");
        });
    }

    async joinRoom(roomName, userName) {
        try {
            await this.connection.invoke("JoinRoom", roomName, userName);
            console.log(`Joined room ${roomName} as ${userName}`);
        } catch (err) {
            console.error('Error joining room:', err);
        }
    }

    async leaveRoom(roomName, userName) {
        try {
            await this.connection.invoke("LeaveRoom", roomName, userName);
            console.log(`Leaved room ${roomName} as ${userName}`);
        } catch (err) {
            console.error('Error leaving room:', err);
        }
    }

    sendMessage(roomName, userName, message) {
        this.connection.invoke("SendMessageToRoom", roomName, userName, message)
            .catch(err => console.error('Error sending message:', err));
    }

    registerMessageHandler(onMessageReceived) {
        this.connection.on("ReceiveMessage", (userName, message) => {
            onMessageReceived(userName, message);
        });
    }

    unregisterMessageHandler(onMessageReceived) {
        this.connection.off("ReceiveMessage", onMessageReceived);
    }


    registerSystemMessageHandler(onSystemMessageReceived) {
        this.connection.on("ReceiveSystemMessage", (systemMessage) => {
            onSystemMessageReceived(systemMessage);
        })
    }

    unregisterSystemMessageHandler(onSystemMessageReceived) {
        this.connection.off("ReceiveSystemMessage", onSystemMessageReceived);
    }

}

export const signalRService = new SignalRService();
