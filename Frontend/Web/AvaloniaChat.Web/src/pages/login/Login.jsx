import { useState } from "react";
import "./login.css";
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
            sessionStorage.setItem(
                "accessToken",
                result.data.data.accessToken
            );
            sessionStorage.setItem("userId", result.data.data.userId);
            navigate("/chat");
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
        <form>
            <div className="container col-3">
                <h3>Sign in</h3>
                <span className="errorMessage">{error}</span>
                <div className="form-floating mb-3">
                    <input
                        className={`form-control ${
                            !isValidUsername ? "is-invalid" : ""
                        }`}
                        type="text"
                        placeholder="Enter username"
                        id="username"
                        onChange={(e) => {
                            validateUsername(e.target.value);
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
                        onChange={(e) => {
                            validatePassword(e.target.value);
                        }}
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
