import {useState} from "react";
import {Button, Form} from "reactstrap";

export const FileUpload = () => {
    const [file, setFile] = useState();
    
    const handleFileChange = (e) => {
        setFile(e.target.files[0]);
    }
    
    const handleUpload = () => {
        if (!file)
            return;
        
        const formData = new FormData();
        formData.append('file', file);
            
        console.log(formData);
    }
    
    return (
        <div>
            <input className="mt-4" type="file" onChange={handleFileChange}/>
            <Button onClick={handleUpload}>Upload</Button>
        </div>);
}