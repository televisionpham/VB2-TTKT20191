import React, { useContext, useState } from 'react';
import axios from 'axios';
import Select from 'react-select'
import { BASE_ADDRESS } from "../constants";
import { AuthContext } from '../contexts/AuthContext';

const UploadFileForm = () => {
    const {auth} = useContext(AuthContext);
    const options = [
        { value: 'vie', label: 'Tiếng Việt' },
        { value: 'en', label: 'Tiếng Anh' },
        { value: 'fra', label: 'Tiếng Pháp' },
        { value: 'deu', label: 'Tiếng Đức' },
        { value: 'rus', label: 'Tiếng Nga' },
    ]

    const [jobFiles, setJobFile] = useState([]);
    const [languages, setLanguages] = useState('vie');

    const handleSubmit = async (e) => {
        e.preventDefault();
        let count = 0;
        for (let i = 0; i < jobFiles.length; i++) {
            const jobFile = jobFiles[i];
            let data = new FormData();
            data.append('jobFile', jobFile);
            data.append('languages', languages);
            data.append('userId', auth.userId);
            try {
                await axios.post(BASE_ADDRESS + '/api/Job/Create', data, {});
                count += 1;
            } catch (err) {
                console.log(err);
                alert(err);
            }                            
        }        
        if (count === jobFiles.length && count > 0) {
            alert('Upload file thành công.');
        }
    }

    return (
        <div id="uploadFileForm">
            <div className="card">
                <div className="card-body">
                    <h4 className="card-title">Số hóa</h4>
                    <form onSubmit={handleSubmit}>
                        <div className="row">
                            <div className="col-sm-12 col-md-6">
                                <Select options={options}
                                    defaultValue={[options[0]]}
                                    isMulti
                                    className="basic-multi-select"
                                    placeholder="Ngôn ngữ nhận dạng"
                                    onChange={(e) => setLanguages(e.map(x => x.value).join('+'))}
                                />
                            </div>
                        </div>
                        <div className="row">
                            <div className="col-sm-12 col-md-6">
                                <input id="jobFile" type="file" multiple className="form-control-file" name="jobFile"
                                    accept=".tif, .tiff, .bmp, .png, .jpg"
                                    onChange={(e) => setJobFile(e.target.files)}
                                />
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