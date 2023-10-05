import {useRef, useState} from "react";
import {Button} from "reactstrap";
import {useUploadFileMutation} from "../api/fileUploadApi";

export const FileUpload = () => {
    let isSuccess = true;
    let errorMsg = "";
    const fileInputRef = useRef();  
    const [file, setFile] = useState();
    const [uploadFile] = useUploadFileMutation();
    
    const handleFileChange = (e) => {
        setFile(e.target.files[0]);
    }
    
    
    const handleUpload = async () => {
        if (!file)
            return;

        const formData = new FormData();
        formData.append('file', file);
        const response = await uploadFile(formData);
        
        if (response.error) {
            isSuccess = false;
            errorMsg = response.error.data.errorMessage;
        } else {
            isSuccess = true;
            if (fileInputRef.current)
                fileInputRef.current.value = null;
        }
    }
    
    return (
        <div>
            <div>
                <input className="mt-4" type="file" onChange={handleFileChange} ref={fileInputRef}/>
                <Button onClick={handleUpload}>Upload</Button>
            </div>
        </div>
    );
}