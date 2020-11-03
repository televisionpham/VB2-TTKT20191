import React, { Component } from "react";
import axios from "axios";
import moment from "moment";
import JobFileItem from "./JobFileItem";
import { confirmAlert } from "react-confirm-alert";
import "react-confirm-alert/src/react-confirm-alert.css";
import { BASE_ADDRESS } from "../constants";

class JobDetails extends Component {
  _isMounted = false;
  state = {
    jobFiles: [],
  };

  componentDidMount() {
    this._isMounted = true;
    this.getJobFiles();
    setInterval(() => {
      if (this._isMounted) {
        this.getJobFiles();
      }
    }, 5000);
  }

  componentWillUnmount() {
    this._isMounted = false;
  }

  async getJobFiles() {
    const res = await axios.get(
      `${BASE_ADDRESS}/api/Job/GetFiles?id=${this.props.job.Id}&type=-1`
    );
    this.setState({ jobFiles: [...res.data] });
  }

  redoJob() {
    const options = {
      title: "Số hóa lại",
      message: "Bạn chắc chắn muốn số hóa lại tài liệu?",
      buttons: [
        {
          label: "Có",
          onClick: async () => {
            this.props.job.State = 0;
            try {
              await axios.put(`${BASE_ADDRESS}/api/Job/`, this.props.job);
            } catch (err) {
              console.log(err);
              alert(err);
            }
          },
        },
        {
          label: "Không",
          onClick: () => {},
        },
      ],
    };

    confirmAlert(options);
  }

  deleteJob(jobId) {
    const options = {
      title: "Xóa tài liệu",
      message: "Bạn chắc chắn muốn xóa tài liệu?",
      buttons: [
        {
          label: "Có",
          onClick: async () => {
            try {
              axios.delete(`${BASE_ADDRESS}/api/Job/${jobId}`);
            } catch (err) {
              console.log(err);
              alert(err);
            }
          },
        },
        {
          label: "Không",
          onClick: () => {},
        },
      ],
    };

    confirmAlert(options);
  }

  render() {
    const { job } = this.props;
    let jobState = "";
    switch (job.State) {
      case 0:
        jobState = (
          <span className="text-warning">
            <i className="fa fa-clock-o fa-lg"></i>
          </span>
        );
        break;
      case 1:
        jobState = (
          <span className="text-primary">
            <i className="fa fa-cog fa-spin fa-lg"></i>
          </span>
        );
        break;
      case 2:
        jobState = (
          <span className="text-success">
            <i className="fa fa-check-circle fa-lg"></i>
          </span>
        );
        break;
      case -1:
        jobState = (
          <span className="text-danger">
            <i className="fa fa-frown-o fa-lg"></i>
          </span>
        );
        break;
      default:
        break;
    }
    return (
      <tr>
        <td>
          {jobState} {job.Name}
        </td>
        <td>{moment(job.CreatedDate).format("DD/MM/YYYY h:mm:ss")}</td>
        <td>{job.DocumentPages}</td>
        <td>{job.Duration}</td>
        <td>
          {job.ProcessedDate
            ? moment(job.ProcessedDate).format("DD/MM/YYYY h:mm:ss")
            : ""}
        </td>
        <td>
          <ul>
            {this.state.jobFiles.map((jobFile) => {
              return <JobFileItem key={jobFile.Id} jobFile={jobFile} />;
            })}
          </ul>
        </td>
        <td>
          <span className="text-primary" onClick={() => this.redoJob()}>
            <i className="fa fa-repeat fa-lg"></i>{" "}
          </span>
          <span className="text-danger" onClick={() => this.deleteJob(job.Id)}>
            <i className="fa fa-trash fa-lg"></i>
          </span>
        </td>
        <td>{job.Notes}</td>
      </tr>
    );
  }
}

export default JobDetails;
