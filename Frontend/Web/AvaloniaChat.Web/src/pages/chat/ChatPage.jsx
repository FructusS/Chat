import UserGroupsList from "../../components/UserGroupsList";
import axios from "axios";
import { BASE_URL } from "../../constants";
import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";
import Messages from "../../components/Messages";
import { useState, useEffect } from "react";
export const ChatPage = () => {
    const [connection, setConnection] = useState();
    const [messages, setMessages] = useState([]);
    //  const [firstGroupId, setFirstGroupId] = useState([]);
    //  const [groupId, setGroupId] = useState("");
    // useEffect(() => {
    //     const connection = new HubConnectionBuilder()
    //         .withUrl("http://localhost:5000/chatHub", {
    //             accessTokenFactory: () => {
    //                 return sessionStorage.getItem("accessToken");
    //             },
    //         })
    //         .withAutomaticReconnect()
    //         .build();

    //     if (connection) {
    //         connection.start().catch((error) => console.log(error));
    //     }

    //     connection.on("ReceiveMessage", function (message) {
    //         setMessages((messages) => [...messages,message]);
    //     });
    // }, []);
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
        await loadMessages(groupId);
        await joinRoom(groupId);
    }
    async function loadMessages(groupId) {
        axios
            .get(`${BASE_URL}Messages/${groupId}`)
            .then(function (response) {
                if (response.status === 200) {
                    console.log(response.data.data);
                    setMessages(response.data.data);
                    return;
                } else {
                    return;
                }
            })
            .catch(function (error) {
                console.log(error);
                return;
            });
    }
    return (
        <div className="row">
            <div
                className="col-2"
                data-mdb-perfect-scrollbar="true"
                data-mdb-suppress-scroll-x="true"
                style={{ height: "400px" }}
            >
                <UserGroupsList onGroupClick={onGroupClick}></UserGroupsList>
            </div>

            <div className="col-10">
                <Messages messages={messages}></Messages>
            </div>
        </div>
    );
};
