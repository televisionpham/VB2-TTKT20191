import React, { Component } from 'react';
import axios from 'axios'
import moment from 'moment'
import JobFileItem from './JobFileItem';

class JobDetails extends Component {
    state = {
        jobFiles: [],
        job: this.props
    };

    componentDidMount() {
        this.getJobFiles();
        setInterval(() => {
            this.getJobFiles();
        }, 5000);
    }

    getJobFiles() {
        axios.get(`http://localhost/VYT.ApplicationService/api/Job/GetFiles?id=${this.props.job.Id}&type=-1`)
            .then(res => {
                this.setState({ jobFiles: [...res.data] })
            });
    }

    deleteJob(jobId) {
        console.log(jobId);
    }

    redoJob(jobId) {

    }     

    render() {
        const { job } = this.props;
        let jobState = '';
        switch (job.State) {
            case 0:
                jobState = <span className="text-warning"><i className="fa fa-clock-o fa-lg"></i></span>;
                break;
            case 1:
                jobState = <span className="text-primary"><i className="fa fa-cog fa-spin fa-lg"></i></span>;
                break;
            case 2:
                jobState = <span className="text-success"><i className="fa fa-check-circle fa-lg"></i></span>;
                break;
            case -1:
                jobState = <span className="text-danger"><i className="fa fa-frown-o fa-lg"></i></span>;
                break;
            default:
                break;
        }
        return (
            <tr>
                <td>{jobState} {job.Name}</td>
                <td>{moment(job.CreatedDate).format('DD/MM/YYYY h:mm:ss')}</td>
                <td>{job.DocumentPages}</td>
                <td>{job.Duration}</td>
                <td>
                    {job.ProcessedDate ? moment(job.ProcessedDate).format('DD/MM/YYYY h:mm:ss') : ''}
                </td>
                <td>
                    <ul>
                        {this.state.jobFiles.map(jobFile => {
                            return (
                                <JobFileItem key={jobFile.Id} jobFile={jobFile} />
                            )
                        })}
                    </ul>
                </td>
                <td>
                    <span className="text-primary" onClick={() => this.redoJob(job.Id)}><i className="fa fa-repeat fa-lg"></i> </span>
                    <span className="text-danger" onClick={() => this.deleteJob(job.Id)}><i className="fa fa-trash fa-lg"></i></span>
                </td>
                <td>
                    {job.Notes}
                </td>
            </tr>
        );
    }
}

export default JobDetails;