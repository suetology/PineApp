import {useRef, useState} from "react";
import {Button} from "reactstrap";
import {useUploadFileMutation} from "../api/fileUploadApi";

export const FileUpload = () => {
    const fileInputRef = useRef();
    const [file, setFile] = useState();
    const [isSuccess, setIsSuccess] = useState(true);
    const [errorMsg, setErrorMsg] = useState("");
    const [uploadFile] = useUploadFileMutation();

    const handleFileChange = (e) => {
        setIsSuccess(true);
        setFile(e.target.files[0]);
    }

    const handleUpload = async () => {
        if (!file) {
            setErrorMsg("Error uploading file");
            setIsSuccess(false);
            return;
        }

        const formData = new FormData();
        formData.append('file', file);
        const response = await uploadFile(formData);

        if (response.error) {
            setErrorMsg(response.error.data.errorMessage);
            setIsSuccess(false);
        } else {
            setFile(null);
            setIsSuccess(true);
            if (fileInputRef.current)
                fileInputRef.current.value = null;
        }
    }

    return (
        <div>
            <div>
                <input className="mt-4" type="file" onChange={handleFileChange} ref={fileInputRef} data-testid="file-input"/>
                <Button onClick={handleUpload}>Upload</Button>
            </div>
            <div>{isSuccess ? "" : errorMsg}</div>
        </div>
    );
}