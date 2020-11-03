import React, { useContext, useEffect, useState } from "react";
import axios from "axios";
import JobDetails from "./JobDetails";
import { BASE_ADDRESS } from "../constants";
import { AuthContext } from "../contexts/AuthContext";

const JobList = (props) => {
  const { auth } = useContext(AuthContext);
  const [jobs, setJobs] = useState([]);
  const [totalJobs, setTotalJobs] = useState(0);
  const [pageIndex, setPageIndex] = useState(1);
  const [pageSize, setPageSize] = useState(10);  
  const refreshInterval = 5000;  

  useEffect(() => {            
      const interval = setInterval(() => {
          refresh();
      }, refreshInterval);
      return () => clearInterval(interval);
  })

  const refresh = async () => {
    try {
      const res = await axios.get(`${BASE_ADDRESS}/api/Job/Total?userId=${auth.userId}`);
      setTotalJobs(res.data);
      setPageIndex(1);
      getJobPage(auth.userId, pageIndex, pageSize);
    } catch (err) {
      console.log(err);
    }
  };

  const getJobPage = async (userId, pageIndex, pageSize) => {
    try {
      const res = await axios.get(
        `${BASE_ADDRESS}/api/Job?userId=${userId}&pageIndex=${pageIndex}&pageSize=${pageSize}`
      );
      setJobs(res.data);
    } catch (err) {
      console.log(err);
    }
  };

  const changePageIndex = (pageIndex) => {
    setPageIndex(pageIndex);
    getJobPage(pageIndex, pageSize);
  };

  const handleChangePageSize = (e) => {
    setPageSize(e.target.value);
    refresh();
  };

  const generatePagesSequence = () => {
    const totalPages = Math.ceil(totalJobs / pageSize);
    let pages = [];
    for (let index = 0; index < totalPages; index++) {
      if (index + 1 === pageIndex) {
        pages.push(
          <li className="page-item active">
            <span className="page-link">{index + 1}</span>
          </li>
        );
      } else {
        pages.push(
          <li className="page-item" style={{ cursor: "pointer" }}>
            <span
              className="page-link"
              onClick={() => changePageIndex(index + 1)}
            >
              {index + 1}
            </span>
          </li>
        );
      }
    }
    return {totalPages, pages};
  };

  return (
    <div id="jobList">
      <h4>Danh sách tài liệu ({totalJobs})</h4>
      <div style={{ marginBottom: "8px" }}>
        <span>Hiển thị </span>
        <select id="pageSize" defaultValue="10" onChange={handleChangePageSize}>
          <option value="5">5</option>
          <option value="10">10</option>
          <option value="25">25</option>
          <option value="50">50</option>
          <option value="100">100</option>
        </select>
        <span> kết quả / 1 trang</span>
      </div>
      <table className="table">
        <thead>
          <tr>
            <th scope="col">Tên</th>
            <th scope="col">Thời gian tạo</th>
            <th scope="col">Số trang</th>
            <th scope="col">Thời gian xử lý</th>
            <th scope="col">Hoàn thành</th>
            <th scope="col">Files</th>
            <th scope="col">Thao tác</th>
            <th scope="col">Ghi chú</th>
          </tr>
        </thead>
        <tbody>
          {jobs.map((job) => (
            <JobDetails job={job} key={job.Id} />
          ))}
        </tbody>
      </table>
      <nav>
        <ul className="pagination">
          {pageIndex <= generatePagesSequence().totalPages && pageIndex > 1 ? (
            <li className="page-item" style={{ cursor: "pointer" }}>
              <span
                className="page-link"
                onClick={() => changePageIndex(pageIndex - 1)}
              >
                &laquo;
              </span>
            </li>
          ) : (
            <li className="page-item disabled">
              <span className="page-link">&laquo;</span>
            </li>
          )}
          {generatePagesSequence().pages}
          {pageIndex < generatePagesSequence().totalPages ? (
            <li className="page-item" style={{ cursor: "pointer" }}>
              <span
                className="page-link"
                onClick={() => changePageIndex(pageIndex + 1)}
              >
                &raquo;
              </span>
            </li>
          ) : (
            <li className="page-item disabled">
              <span className="page-link">&raquo;</span>
            </li>
          )}
        </ul>
      </nav>
    </div>
  );
};

export default JobList;
