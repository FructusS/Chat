import { useEffect } from "react";
import axios from "axios";
import { useState } from "react";
import { BASE_URL } from "../../constants";
import { GroupItem } from "./GroupItem";

export const GroupList = ({ onGroupClick }) => {
    const [groups, setGroups] = useState([]);
    useEffect(() => {
        axios
            .get(`${BASE_URL}UserGroup/${sessionStorage.getItem("userId")}`)
            .then(function (response) {
                if (response.status === 200) {
                    setGroups(response.data.data);
                    return;
                } else {
                    return;
                }
            })
            .catch(function (error) {
                console.log(error);
                return;
            });
    }, []);

    return (
        <div className="messages-wrapper">
            {groups?.map((group) => (
                <GroupItem
                    onGroupClick={onGroupClick}
                    key={group.id}
                    group={group}
                />
            ))}
        </div>
    );
};
