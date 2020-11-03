import Axios from "axios";
import { MD5 } from "crypto-js";
import React, { useContext, useState } from "react";
import { Link, Redirect } from "react-router-dom";
import { BASE_ADDRESS, SIGNUP_SUCCESS } from "../constants";
import { AuthContext } from "../contexts/AuthContext";
import history from "../history";

const SignUp = (props) => {
  const { auth, dispatch } = useContext(AuthContext);
  const [email, setEmail] = useState();
  const [password, setPassword] = useState();
  const [errorMsg, setErrorMsg] = useState();

  if (auth.userId) {
    return <Redirect to="/" />;
  }

  const handleSubmit = async (e) => {
    e.preventDefault();
    let passwordHash = MD5(password).toString();
    let data = {
      email,
      passwordHash,
    };
    try {
      const res = await Axios.post(`${BASE_ADDRESS}/api/User/Add`, data, {});

      if (res.status === 200) {
        dispatch({ type: SIGNUP_SUCCESS, cred: {
            userId: res.data.Id,
            email: res.data.Email
        } });
        history.push("/");
      }
      setErrorMsg("");
    } catch (error) {
      setErrorMsg(error.message);
    }
  };

  return (
    <div className="container">
      <br />
      <h4 className="text-center">Đăng ký</h4>
      <br />
      <div className="row">
        <div className="col"></div>
        <div className="col-sm-12 col-md-6">
          <form onSubmit={handleSubmit}>
            <div className="form-group">
              <label htmlFor="email">Email address:</label>
              <input
                type="email"
                className="form-control"
                id="email"
                required="true"
                onChange={(e) => setEmail(e.target.value)}
              />
            </div>
            <div className="form-group">
              <label htmlFor="pwd">Password:</label>
              <input
                type="password"
                className="form-control"
                id="pwd"
                required="true"
                onChange={(e) => setPassword(e.target.value)}
              />
            </div>
            <div>
              {errorMsg !== "" ? (
                <p className="text-danger">{errorMsg}</p>
              ) : null}
            </div>
            <span>
              <button type="submit" className="btn btn-primary">
                Đăng ký
              </button>{" "}
              Đã có tài khoản? <Link to="/signin">Đăng nhập</Link>
            </span>
          </form>
        </div>
        <div className="col"></div>
      </div>
    </div>
  );
};

export default SignUp;
