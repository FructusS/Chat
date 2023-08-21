import axios from "axios";
import { BASE_URL } from "../constants";
export const login = async (username, password) => {
    return axios.post(`${BASE_URL}/Auth`, {
        username,
        password,
    });
};
