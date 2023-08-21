import { getMessages } from "../../services/MessageService";
import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";
import { MessageList, GroupList } from "../../components";
import { useState, useEffect } from "react";
export const Chat = () => {
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
        try {
            const result = await getMessages(groupId);
            console.log(result)
            setMessages(result.data.data);
        } catch (e) {
            //  setError(e?.response?.data?.error?.message || e?.message);
            console.log(e?.response?.data?.error?.message || e?.message);
        }
        await joinRoom();
    }

    return (
        <div className="row">
            <div
                className="col-2"
                data-mdb-perfect-scrollbar="true"
                data-mdb-suppress-scroll-x="true"
                style={{ height: "400px" }}
            >
                <GroupList onGroupClick={onGroupClick}></GroupList>
            </div>

            <div className="col-10">
                <MessageList messages={messages}></MessageList>
            </div>
        </div>
    );
};
