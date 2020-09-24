import React from 'react';

const JobFileItem = (props) => {    
    const {jobFile} = props;
    let icon = <i className="fa fa-file-image-o"></i>;

    if (jobFile.FileName.toLowerCase().endsWith(".pdf")) {
        icon = <i className="fa fa-file-pdf-o"></i>;
    }

    return (         
        <li id={jobFile.id} key={jobFile.id}><a href={jobFile.FilePath} target="_blank" rel="noopener noreferrer">{icon} {jobFile.FileName}</a></li>
     );
}
 
export default JobFileItem;