import React from 'react'
import Footer from './Footer'
import JobList from './JobList'
import UploadFileForm from './UploadFileForm'

const Home = () => {
    
    return (
        <div className="container-fluid">
            <br/>
            <UploadFileForm/>
            <br/>
            <JobList/>
            <Footer/>
        </div>
    )
}

export default Home
