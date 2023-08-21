import axios from "axios";
import { BASE_URL } from "../constants";
export const getMessages = async (groupId) => {
    return axios.get(`${BASE_URL}/Messages/${groupId}`);
 };
 