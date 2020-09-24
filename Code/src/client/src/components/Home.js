import React from 'react'
import JobList from './JobList'
import UploadFileForm from './UploadFileForm'

const Home = () => {
    
    return (
        <div className="container">
            <br/>
            <UploadFileForm/>
            <br/>
            <JobList/>
        </div>
    )
}

export default Home