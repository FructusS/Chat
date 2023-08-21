import { useEffect } from "react";
import axios from "axios";
import { useState } from "react";
import { BASE_URL } from "../../constants";
import { GroupItem } from "./GroupItem";
import { getGroups } from "../../services/GroupService";

export const GroupList = ({ onGroupClick }) => {
    const [groups, setGroups] = useState([]);
    useEffect(() => {
        async function getUserGroups() {
            try {
                const result = await getGroups();
                setGroups(result.data.data);
            } catch (e) {}
        }
        getUserGroups()
    }, [groups]);

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
