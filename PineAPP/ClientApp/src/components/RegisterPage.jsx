import {useGetAllUsersQuery, useGetUserByIdQuery} from "../api/usersApi";
import {useGetAllDecksQuery, useGetCommunityDecksQuery, useGetDeckByIdQuery} from "../api/decksApi";
import {useEffect} from "react";

const RegisterPage = () => {

    const {data, isLoading, isError} = useGetUserByIdQuery(2); // Replace '1' with the desired user ID
    console.log("RESPONSE:")
    console.log({data, isLoading, isError});
    console.log("DATA: ");
    console.log(data);
    let user;
    if(!isLoading){
        const {statusCode, isSuccess, errorMessage, result} = data;
        console.log("DATA LOADED: ");
        console.log(result);

        user = result;
        console.log(result.userId);
    }
    
    
    if(isLoading) {
        return (
            <div>Loading...</div>
        );
    }
    return (
        <div>
            <h1></h1>
            <p>User ID: {user.userId}</p>
            <p>Email: {user.email}</p>
            <p>Username: {user.userName}</p>
            <p>Password: {user.password}</p>
        </div>
    );
}

export default RegisterPage;