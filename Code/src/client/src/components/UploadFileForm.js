import React, { useState } from 'react';
import axios from 'axios';
import Select from 'react-select'

const UploadFileForm = () => {
    const options = [
        { value: 'vie', label: 'Tiếng Việt' },
        { value: 'en', label: 'English' }
    ]

    const [jobFile, setJobFile] = useState('');
    const [languages, setLanguages] = useState('vie');

    const handleSubmit = (e) => {
        e.preventDefault();
        const data = new FormData();
        data.append('jobFile', jobFile);
        data.append('languages', languages);
        axios.post('http://localhost/VYT.ApplicationService/api/Job/Create', data, {})
            .then(res => {
                if (res.status === 200) {
                    alert('Tải file thành công');
                } else {
                    alert(res.statusText);
                }
            })
            .catch(err => {
                alert(err);
            });
    }

    return (
        <div id="uploadFileForm">
            <div className="card">
                <div className="card-body">
                    <h4 className="card-title">Số hóa file</h4>
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
                            <div className="col-sm-12 col-md-4">
                                <input id="jobFile" type="file" className="form-control-file" name="jobFile"
                                    accept=".tif, .tiff, .bmp, .png, .jpg"
                                    onChange={(e) => setJobFile(e.target.files[0])}
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