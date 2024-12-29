import { useEffect, useRef } from 'react';
import {signalRService} from "../SignalRService";

const useSignalRHandlers = (roomName, userName, setMessages) => {
    const handlersInitialized = useRef(false); // Prevent duplicate initialization
    const messageHandlerRef = useRef();
    const systemMessageHandlerRef = useRef();

    useEffect(() => {
        if (handlersInitialized.current) return;

        handlersInitialized.current = true; // Mark handlers as initialized

        messageHandlerRef.current = (userName, message) => {
            console.log('Message received:', userName, message);
            setMessages(prevMessages => [
                ...prevMessages,
                { type: 'chat', content: `${userName}: ${message}` }
            ]);
        };

        systemMessageHandlerRef.current = (systemMessage) => {
            console.log('System message received:', systemMessage);
            setMessages(prevMessages => [
                ...prevMessages,
                { type: 'system', content: `${systemMessage}` }
            ]);
        };

        console.log('Registering handlers');
        signalRService.registerMessageHandler(messageHandlerRef.current);
        signalRService.registerSystemMessageHandler(systemMessageHandlerRef.current);

        return () => {
            console.log('Unregistering handlers');
            signalRService.unregisterMessageHandler(messageHandlerRef.current);
            signalRService.unregisterSystemMessageHandler(systemMessageHandlerRef.current);
        };
    }, [setMessages]); // Dependencies include only what’s truly necessary
};

export default useSignalRHandlers;
