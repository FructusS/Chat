import { useEffect } from "react";
import axios from "axios";
import { useState } from "react";
import { BASE_URL } from "../constants";
import Group from "./Group";
export default function UserGroupsList() {
  const [groups, setGroups] = useState([]);
  useEffect(() => {
    axios
      .get(`/UserGroup/${localStorage.getItem("userId")}`)
      .then(function (response) {
        if (response.status == 200) {
          setGroups(response.data);
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
        <Group key={group.id} group={group} />
      ))}
    </div>
  );
}
