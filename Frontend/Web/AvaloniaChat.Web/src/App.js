import logo from "./logo.svg";
import "./App.css";
import "bootstrap/dist/css/bootstrap.min.css";
import { Navigate } from "react-router-dom";
import Chat from "./components/chat/Chat";
import Login from "./components/login/Login";
import { Routes, Route, BrowserRouter as Router } from "react-router-dom";
function App() {
    return (
        <div className="App">
            <Router>
                <div>
                    <Routes>
                        <Route path="/login" element={<Login />} />
                        <Route path="/chat" element={<Chat />} />

                        <Route
                            path="*"
                            element={<Navigate to="/login" replace={true} />}
                        />
                    </Routes>
                </div>
            </Router>
        </div>
    );
}

export default App;
