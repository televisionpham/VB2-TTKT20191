import React, { useState } from 'react';
import { Link } from 'react-router-dom';

const SignUp = (props) => {
    const [email, setEmail] = useState();
    const [password, setPassword] = useState();
    const [errorMsg, setErrorMsg] = useState();

    const handleSubmit = (e) => {
        e.preventDefault();
    }

    return (
        <div className="container">
            <br />
            <h4 className="text-center">Đăng ký</h4>
            <br />
            <form onSubmit={handleSubmit}>
                <div className="form-group">
                    <label htmlFor="email">Email address:</label>
                    <input type="email" className="form-control" id="email" required="true"/>
                </div>
                <div className="form-group">
                    <label htmlFor="pwd">Password:</label>
                    <input type="password" className="form-control" id="pwd" required="true"/>
                </div>
                <div>
                    {errorMsg !== '' ? <p className="text-danger">{errorMsg}</p> : null}
                </div>
                <span>
                    <button type="submit" className="btn btn-primary">Đăng ký</button> Đã có tài khoản? <Link to="/signin">Đăng nhập</Link>
                </span>
            </form>
        </div>
    );
}

export default SignUp;