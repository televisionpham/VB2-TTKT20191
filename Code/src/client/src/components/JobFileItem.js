import React from 'react';

const JobFileItem = (props) => {    
    const {jobFile} = props;

    return (         
        <li id={jobFile.id} key={jobFile.id}><a href={jobFile.FilePath} target="_blank">{jobFile.FileName}</a></li>
     );
}
 
export default JobFileItem;