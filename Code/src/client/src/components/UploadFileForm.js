import React, { useState } from 'react';
import axios from 'axios';
import Select from 'react-select'

const UploadFileForm = () => {
    const options = [
        { value: 'vie', label: 'Tiếng Việt' },
        { value: 'en', label: 'Tiếng Anh' },
        { value: 'fra', label: 'Tiếng Pháp' },
        { value: 'deu', label: 'Tiếng Đức' },
        { value: 'rus', label: 'Tiếng Nga' },
    ]

    const [jobFiles, setJobFile] = useState([]);
    const [languages, setLanguages] = useState('vie');
    const [message, setMessage] = useState('');

    const handleSubmit = (e) => {
        e.preventDefault();
        console.log(jobFiles);
        for (let i = 0; i < jobFiles.length; i++) {
            const jobFile = jobFiles[i];
            let data = new FormData();
            data.append('jobFile', jobFile);
            data.append('languages', languages);
            axios.post('http://localhost/VYT.ApplicationService/api/Job/Create', data, {})
                .then(res => {
                    if (res.status === 200) {
                        setMessage('Tải thành công file: ' + jobFile.name);
                        console.log(message);
                    } else {
                        alert(res.statusText);
                    }
                })
                .catch(err => {
                    alert(err);
                });
        }        
    }

    return (
        <div id="uploadFileForm">
            <div className="card">
                <div className="card-body">
                    <h4 className="card-title">Số hóa</h4>
                    <form onSubmit={handleSubmit}>
                        <div className="row">
                            <div className="col-sm-12 col-md-5">
                                <Select options={options}
                                    defaultValue={[options[0]]}
                                    isMulti
                                    className="basic-multi-select"
                                    placeholder="Ngôn ngữ nhận dạng"
                                    onChange={(e) => setLanguages(e.map(x => x.value).join('+'))}
                                />
                            </div>
                            <div className="col-sm-12 col-md-3">
                                <input id="jobFile" type="file" multiple className="form-control-file" name="jobFile"
                                    accept=".tif, .tiff, .bmp, .png, .jpg"
                                    onChange={(e) => setJobFile(e.target.files)}
                                />
                            </div>
                            <div className="col-sm-12 col-md-4">
                                {message}
                            </div>
                        </div>
                        <button type="submit" className="btn btn-primary">Tải lên</button>
                    </form>
                </div>
            </div>
        </div>
    );
}

export default UploadFileForm;