import React, { useState } from 'react';
import { Link, Redirect } from 'react-router-dom';
import axios from 'axios';
import { BASE_ADDRESS } from '../constants';
import { MD5 } from 'crypto-js';

const SignIn = (props) => {

    const [email, setEmail] = useState();
    const [password, setPassword] = useState();
    const [errorMsg, setErrorMsg] = useState();
    const handleSubmit = (e) => {
        e.preventDefault();
        let passwordHash = MD5(password).toString();
        let data = new FormData();
        data.append('email', email);
        data.append('passwordHash', passwordHash);
        console.log(email, passwordHash);
        axios.post(`${BASE_ADDRESS}/api/User/Auth?email=${email}&passwordHash=${passwordHash}`, data, {})
        .then((res) => {
            console.log("res:" + res);
            if (res.status === 200) {
                props.history.push("/");
            } else {
                setErrorMsg(res.statusText);
            }
        })
        .catch((e) => {
            setErrorMsg(e);
        })
    }

    return (
        <div className="container">
            <br />
            <h4 className="text-center">Đăng nhập</h4>
            <br />
            <form onSubmit={handleSubmit}>
                <div className="form-group">
                    <label htmlFor="email">Email address:</label>
                    <input type="email" className="form-control" id="email" required="true" onChange={(e) => setEmail(e.target.value)} />
                </div>
                <div className="form-group">
                    <label htmlFor="pwd">Password:</label>
                    <input type="password" className="form-control" id="pwd" required="true" onChange={(e) => setPassword(e.target.value)} />
                </div>
                <div>
                    {errorMsg !== '' ? <p className="text-danger">{errorMsg}</p> : null}
                </div>
                <span>
                    <button type="submit" className="btn btn-primary">Đăng nhập</button> Chưa có tài khoản? <Link to="/signup">Đăng ký</Link>
                </span>
            </form>
        </div>
    );
}

export default SignIn;