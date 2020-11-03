import React, { useContext } from "react";
import { SIGNOUT } from "../constants";
import { AuthContext } from "../contexts/AuthContext";
import history from "../history";

const NavBar = (props) => {
    const { auth, dispatch } = useContext(AuthContext);
    if (!auth.userId) {
        return null;
    }
  const signOut = (e) => {
      e.preventDefault();
      dispatch({type: SIGNOUT});
      history.push("/");
  };

  return (
    <header>
      <div>
        <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
          <a className="navbar-brand" href="/">
            VYT - Số hóa tài liệu
          </a>
          <button
            className="navbar-toggler"
            type="button"
            data-toggle="collapse"
            data-target="#navbarSupportedContent"
            aria-controls="navbarSupportedContent"
            aria-expanded="false"
            aria-label="Toggle navigation"
          >
            <span className="navbar-toggler-icon"></span>
          </button>

          <div className="collapse navbar-collapse" id="navbarSupportedContent">
            <ul className="navbar-nav ml-auto">
              <li className="nav-item"></li>
              <li className="nav-item">
                <span className="text-light">
                  <i className="fa fa-user-circle-o fa-lg"></i> {auth.email}
                </span>
                <span> </span>
                <span>
                  <a
                    className="btn btn-primary btn-sm"
                    href="/"
                    onClick={signOut}
                    id="lnk-signout"
                  >
                    Sign out
                  </a>
                </span>
              </li>
            </ul>
          </div>
        </nav>
      </div>
    </header>
  );
};

export default NavBar;
