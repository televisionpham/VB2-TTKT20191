import React, { Component } from 'react';
import axios from 'axios'
import JobDetails from './JobDetails'

class JobList extends Component {    
    state = {
        jobs: [],
        totalJobs: 0,
        pageIndex: 1,
        pageSize: 10
    }

    componentDidMount() {
        this.refresh();
        setInterval(() => {
            this.getJobPage(this.state.pageIndex, this.state.pageSize);
        }, 5000);
    };

    refresh() {
        axios.get(`http://localhost:24151/api/Job/Total`)
            .then(res => {
                this.setState({ totalJobs: res.data });
            });
        this.setState({
            pageIndex: 1,
        }, () => {
            this.getJobPage(this.state.pageIndex, this.state.pageSize);
        })
    }

    getJobPage(pageIndex, pageSize) {
        axios.get(`http://localhost:24151/api/Job?pageIndex=${pageIndex}&pageSize=${pageSize}`)
            .then(res => {
                this.setState({ jobs: res.data });
            });
    }

    changePageIndex(pageIndex) {
        this.setState({ pageIndex }, () => {
            this.getJobPage(this.state.pageIndex, this.state.pageSize);
        });
    }

    handleChangePageSize(e) {
        this.setState({ pageSize: e.target.value }, () => {
            this.refresh();
        });
    }    

    render() {
        const totalPages = Math.ceil(this.state.totalJobs / this.state.pageSize);
        let pages = [];
        for (let index = 0; index < totalPages; index++) {
            if (index + 1 === this.state.pageIndex) {
                pages.push(<li className="page-item active"><span className="page-link">{index + 1}</span></li>)
            } else {
                pages.push(<li className="page-item" style={{ cursor: 'pointer' }}><span className="page-link" onClick={() => this.changePageIndex(index + 1)}>{index + 1}</span></li>)
            }
        }

        return (
            <div id="jobList">
                <h4>Danh sách tài liệu ({this.state.totalJobs})</h4>
                <div style={{ marginBottom: '8px' }}>
                    <span>Hiển thị </span>
                    <select id="pageSize" defaultValue="10" onChange={(e) => this.handleChangePageSize(e)}>
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
                        {
                            this.state.jobs.map(job => <JobDetails job={job} key={job.Id}                                
                            />)
                        }
                    </tbody>
                </table>
                <nav>
                    <ul className="pagination">
                        {this.state.pageIndex <= totalPages && this.state.pageIndex > 1 ?
                            <li className="page-item" style={{ cursor: 'pointer' }}><span className="page-link" onClick={() => this.changePageIndex(this.state.pageIndex - 1)}>&laquo;</span></li> :
                            <li className="page-item disabled"><span className="page-link">&laquo;</span></li>
                        }
                        {pages}
                        {this.state.pageIndex < totalPages ?
                            <li className="page-item" style={{ cursor: 'pointer' }}><span className="page-link" onClick={() => this.changePageIndex(this.state.pageIndex + 1)}>&raquo;</span></li> :
                            <li className="page-item disabled"><span className="page-link">&raquo;</span></li>
                        }
                    </ul>
                </nav>
            </div>
        );
    }
}

export default JobList;