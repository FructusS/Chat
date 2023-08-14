import "./App.css";
import "bootstrap/dist/css/bootstrap.min.css";
import { Navigate } from "react-router-dom";
import { LoginPage, ChatPage } from "./pages";
import { Routes, Route, BrowserRouter as Router } from "react-router-dom";
function App() {
    return (
        <div className="App">
            <Router>
                <div className="container-md py-5">
                    <Routes>
                        <Route path="/login" element={<LoginPage />} />
                        <Route path="/chat" element={<ChatPage />} />

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
