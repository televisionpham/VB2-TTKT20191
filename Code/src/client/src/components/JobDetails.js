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
                jobState = 'Đang chờ';
                break;
            case 1:
                jobState = 'Đang xử lý';
                break;
            case 2:
                jobState = 'Đã xử lý';
                break;
            default:
                break;
        }
        return (
            <tr>
                <td>{job.Name}</td>
                <td>{job.Languages}</td>
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
                                <JobFileItem key={jobFile.Id} jobFile={jobFile}/>
                            )
                        })}
                    </ul>
                </td>
            </tr>            
        );
    }
}

export default JobDetails;