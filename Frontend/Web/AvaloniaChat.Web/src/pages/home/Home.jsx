import { useState, useEffect } from "react";
import { Login,Chat } from "../../pages";
export const Home = () => {


    const [userId, setUserId] = useState();
    
    useEffect(() => {
        setUserId(sessionStorage.getItem("userId"))
    });

    return (
        <Login></Login>
    );
};
