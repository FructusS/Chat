import "./App.css";
import { Navigate } from "react-router-dom";
import { Chat, Home, Login } from "./pages";
import { Routes, Route, BrowserRouter as Router } from "react-router-dom";
import { useState, useEffect } from "react";

function App() {
    const [userId, setUserId] = useState();

    useEffect(() => {
        setUserId(sessionStorage.getItem("userId"));
    }, []);

    return (
        <div className="App">
            <Router>
                <section>
                    <Routes>
                        if(userId != null)
                        {<Route path="/home" element={<Home />} />}
                        else
                        {<Route path="/" element={<Login></Login>}></Route>}
                        <Route path="*" element={<Login></Login>}></Route>
                    </Routes>
                </section>
            </Router>
        </div>
    );
}

export default App;
