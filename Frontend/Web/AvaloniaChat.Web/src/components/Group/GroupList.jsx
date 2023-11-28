import { useEffect, useState } from "react";
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
        getUserGroups();
    }, []);

    return (
        <div className="group-list">
            {groups?.map((group) => (
                <GroupItem
                    onGroupClick={onGroupClick}
                    key={group.groupId}
                    group={group}
                />
            ))}
        </div>
    );
};
