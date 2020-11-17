import React from "react";
import "./App.css";
import Home from "./components/Home";
import NavBar from "./components/NavBar";
import { Router, Switch, Route } from "react-router-dom";
import SignUp from "./components/SignUp";
import SignIn from "./components/SignIn";
import AuthProvider from './contexts/AuthContext';
import history from "./history";
import "antd/dist/antd.css";

function App() {
  return (
    <Router history={history}>
      <AuthProvider>
        <div className="App">
          <NavBar />
          <Switch>
            <Route exact path="/" component={Home} />
            <Route path="/signin" component={SignIn} />
            <Route path="/signup" component={SignUp} />
          </Switch>
        </div>
      </AuthProvider>
    </Router>
  );
}

export default App;
