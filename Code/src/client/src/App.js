import React from 'react';
import './App.css';
import Home from './components/Home';
import NavBar from './components/NavBar'
import { BrowserRouter, Switch, Route } from 'react-router-dom'
import SignUp from './components/SignUp';
import SignIn from './components/SignIn';

function App() {
  return (
    <BrowserRouter>
      <div className="App">
        <NavBar />
        <Switch>
          <Route exact path="/" component={Home} />
          <Route path="/signin" component={SignIn} />
          <Route path="/signup" component={SignUp}/>
        </Switch>
      </div>
    </BrowserRouter>
  );
}

export default App;
