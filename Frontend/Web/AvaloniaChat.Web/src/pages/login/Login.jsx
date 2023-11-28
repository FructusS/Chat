import { useState } from "react";
import "../../App.css";
import { useNavigate } from "react-router-dom";
import { login } from "../../services/AuthService";
export const Login = () => {
    const navigate = useNavigate();
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState();

    const [isValidUsername, setIsValidUsername] = useState(false);
    const [isValidPassword, setIsValidPassword] = useState(false);

    const onLogin = async (e) => {
        e.preventDefault();

        if (isValidPassword === false || isValidUsername === false) {
            return;
        }
        try {
            const result = await login(username, password);
            sessionStorage.setItem("accessToken", result.data.data.accessToken);
            sessionStorage.setItem("userId", result.data.data.userId);
            navigate("/home");
        } catch (e) {
            setError(e?.response?.data?.error?.message || e?.message);
        }
    };

    function validateUsername(username) {
        setUsername(username);
        setIsValidUsername(username.trim().length >= 5);
    }

    function validatePassword(password) {
        setPassword(password);
        setIsValidPassword(password.trim().length >= 5);
    }

    return (
        <div className="login-container">
            <span className="errorMessage">{error}</span>
            <form className="login-form">
                <div className="input-container">
                    <label>Username</label>
                    <input
            
                        type="text"
                        placeholder="Enter username"
                        id="username"
                        onChange={(e) => {
                            validateUsername(e.target.value);
                        }}
                        value={username}
                    ></input>
                </div>
                <div className="input-container">
                    <label>Password</label>
                    <input
        
                        type="password"
                        placeholder="Enter password"
                        id="password"
                        onChange={(e) => {
                            validatePassword(e.target.value);
                        }}
                        value={password}
                        required
                    ></input>
                </div>
                <button className="btn" onClick={onLogin}>
                    Login
                </button>
            </form>
        </div>
    );
};
