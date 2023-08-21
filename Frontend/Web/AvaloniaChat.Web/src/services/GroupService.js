import axios from "axios";
import { BASE_URL } from "../constants";

export const getGroups = async() => {
    return axios.get(
        `${BASE_URL}/UserGroup/${sessionStorage.getItem("userId")}`
    );
};
