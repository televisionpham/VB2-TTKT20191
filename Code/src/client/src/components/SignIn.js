import React, { useContext, useState } from "react";
import { Link, Redirect } from "react-router-dom";
import axios from "axios";
import { BASE_ADDRESS, SIGNIN_SUCCESS } from "../constants";
import { MD5 } from "crypto-js";
import { AuthContext } from "../contexts/AuthContext";

const SignIn = (props) => {
  const { auth, dispatch } = useContext(AuthContext);  
  const [email, setEmail] = useState();
  const [password, setPassword] = useState();
  const [errorMsg, setErrorMsg] = useState();

  if (auth.userId) {
      return <Redirect to="/"/>
  }

  const handleSubmit = async (e) => {
    e.preventDefault();
    let passwordHash = MD5(password).toString();
    let data = new FormData();
    data.append("email", email);
    data.append("passwordHash", passwordHash);
    try {
      const res = await axios.post(
        `${BASE_ADDRESS}/api/User/Auth?email=${email}&passwordHash=${passwordHash}`,
        data,
        {}
      );

      console.log(res);

      if (res.status === 200) {
          dispatch({
              type: SIGNIN_SUCCESS,
              cred: {
                  userId: res.data.Id,
                  email: res.data.Email
              }
          });
        props.history.push("/");
      }
      setErrorMsg("");
    } catch (error) {
        console.log(error.response, error.message);
      setErrorMsg(error.response.data.ExceptionMessage);
    }
  };

  return (
    <div className="container">
      <br />
      <h4 className="text-center">Đăng nhập</h4>
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
                Đăng nhập
              </button>{" "}
              Chưa có tài khoản? <Link to="/signup">Đăng ký</Link>
            </span>
          </form>
        </div>
        <div className="col"></div>
      </div>
    </div>
  );
};

export default SignIn;
