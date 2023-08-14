import axios from "axios";
import { BASE_URL } from "../../constants";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
export const LoginPage = () => {
    axios.defaults.baseURL = BASE_URL;
    const navigate = useNavigate();
    const [username, setUsername] = useState("string");
    const [password, setPassword] = useState("string");

    const [isValidUsername, setIsValidUsername] = useState(false);
    const [isValidPassword, setIsValidPassword] = useState(false);
   
    // setUsername("")
    // setPassword("string")
    const onLogin = async (e) => {
        e.preventDefault();
        if (username.trim().length === 0 && password.trim().length === 0) {
            setIsValidUsername(false);
            return;
        }

        axios
            .post("/Auth/login", {
                username: username,
                password: password,
            })
            .then(function (response) {
                if (response.status === 200) {
                    sessionStorage.setItem(
                        "userId",
                        response.data.data.userId
                    );
                    sessionStorage.setItem(
                        "accessToken",
                        response.data.data.accessToken
                    );
                    navigate("/chat");
                } else {
                }
            })
            .catch(function (error) {
                console.log(error);
            });

        // else if(){
        //     setIsValidPassword(false);
        // }
        // if(isValidPassword && isValidUsername == false){
        //     return;
        // }
    };

    return (
        <form>
            <div className="container col-3">
                <h3>Authorization</h3>
                <div className="form-floating mb-3">
                    <input
                        className={`form-control ${
                            !isValidUsername ? "is-invalid" : ""
                        }`}
                        type="text"
                        placeholder="Enter username"
                        id="username"
                        onChange={(e) => {
                            setUsername(e.target.value);
                        }}
                        value={username}
                    ></input>
                    <label htmlFor="username">Username</label>
                </div>
                <div className="form-floating">
                    <input
                        className={`form-control ${
                            !isValidPassword ? "is-invalid" : ""
                        }`}
                        type="password"
                        placeholder="Enter password"
                        id="password"
                        onChange={(e) => setPassword(e.target.value)}
                        value={password}
                        required
                    ></input>
                    <label htmlFor="password">Password</label>
                </div>
                <div>
                    <button className="btn" onClick={onLogin}>
                        Login
                    </button>
                </div>
            </div>
        </form>
    );
};
