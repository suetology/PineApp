import React, {useState} from "react";
import {Button, Input} from "reactstrap";

const InputBox = ({initialValue, type, className}) => {
    
    const [isEditing, setEditing] = useState(false);
    const [value, setValue] = useState(initialValue);

    const text = 
        isEditing
        ? <div>
            <Input type={type} 
                   value={value} 
                   onChange={(e) => setValue(e.target.value)}/>
            <Button className="btn-light m-1"
                    onClick={() => setEditing(false)}>
                <img src="/check.svg" alt="save"/>
            </Button>
          </div>
        : <p className={className} 
             style={{cursor: 'pointer'}} 
             onClick={() => setEditing(true)}>
             {value}
          </p>;

    return (
        text
    );
}

export default InputBox;