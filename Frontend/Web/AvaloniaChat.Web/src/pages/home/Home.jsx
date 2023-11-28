import { useState, useEffect } from "react";
import "../../App.css";
import { getMessages } from "../../services/MessageService";
import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";
import { MessageList, GroupList } from "../../components";
export const Home = () => {
    const [connection, setConnection] = useState();
    const [messages, setMessages] = useState([]);
    const joinRoom = async () => {
        try {
            const connection = new HubConnectionBuilder()
                .withUrl("http://localhost:5000/chatHub", {
                    accessTokenFactory: () => {
                        return sessionStorage.getItem("accessToken");
                    },
                })
                .configureLogging(LogLevel.Information)
                .withAutomaticReconnect()
                .build();
            await connection.start().catch((e) => {
                console.log(e);
            });
            connection.on("ReceiveMessage", (message) => {
                setMessages((messages) => [...messages, message]);
            });

            connection.onclose((e) => {
                console.log(e);
                setConnection();
                setMessages([]);
            });

            setConnection(connection);
        } catch (e) {
            console.log(e);
        }
    };

    async function onGroupClick(groupId) {
        try {
            console.log(groupId);

            const result = await getMessages(groupId);
            setMessages(result.data.data);
        } catch (e) {
            //  setError(e?.response?.data?.error?.message || e?.message);
            console.log(e?.response?.data?.error?.message || e?.message);
        }
        await joinRoom();
    }

    return (
        <div className="flex-wrapper ">
            <div className="group-list-container">
                <GroupList onGroupClick={onGroupClick}></GroupList>
            </div>

            <div className="message-list-container">
                <MessageList messages={messages}></MessageList>
            </div>
        </div>
    );
};
