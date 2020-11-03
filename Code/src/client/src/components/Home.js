import React, { useContext } from 'react'
import { Redirect } from 'react-router-dom'
import { AuthContext } from '../contexts/AuthContext'
import Footer from './Footer'
import JobList from './JobList'
import UploadFileForm from './UploadFileForm'

const Home = () => {
    const {auth} = useContext(AuthContext);  
    
    if (!auth.userId) {
        return <Redirect to="/signin"/>
    }

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
