import "./App.css";
import "bootstrap/dist/css/bootstrap.min.css";
import { Navigate } from "react-router-dom";
import { Chat, Home } from "./pages";
import { Routes, Route, BrowserRouter as Router } from "react-router-dom";
import { useState } from "react";

function App() {
    // const [user,setUser] = useState()
    // setUser(sessionStorage.getItem("accessToken"))
    // console.log(user)
    return (
        <div className="App">
            <Router>
                <div className="container-md py-5">
                    <Routes>
                        <Route path="/" element={<Home />} />
                        <Route path="/chat" element={<Chat />} />
                        <Route path="*" element={ <div>Not found</div>}></Route>
                    </Routes>
                </div>
            </Router>
        </div>
    );
}

export default App;
