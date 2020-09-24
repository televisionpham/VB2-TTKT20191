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

    render() {
        const { job } = this.props;
        let jobState = '';
        switch (job.State) {
            case 0:
                jobState = <span style={{color: 'orange'}}><i className="fa fa-clock-o fa-lg"></i></span>;
                break;
            case 1:
                jobState = <span style={{color: 'blue'}}><i className="fa fa-cog fa-spin fa-lg"></i></span>;
                break;
            case 2:
                jobState = <span style={{color: 'green'}}><i className="fa fa-check-circle fa-lg"></i></span>;
                break;
            case -1:
                jobState = <span style={{color: 'red'}}><i className="fa fa-frown-o fa-lg"></i></span>;
                break;
            default:
                break;
        }
        return (
            <tr>
                <td>{job.Name}</td>                
                <td>{moment(job.CreatedDate).format('DD/MM/YYYY h:mm:ss')}</td>
                <td>{job.DocumentPages}</td>
                <td>{job.Duration}</td>
                <td>
                    {job.ProcessedDate ? moment(job.ProcessedDate).format('DD/MM/YYYY h:mm:ss') : ''}
                </td>
                <td>{jobState}</td>
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
                    {job.Notes}
                </td>
            </tr>
        );
    }
}

export default JobDetails;