import axios from "axios";

var AxiosInstance;

if (process.env.NODE_ENV === 'production'){
    AxiosInstance = axios.create({
        baseURL: 'http://kitchhub.tatar/api/',
    });
} else {
    AxiosInstance = axios.create({
        baseURL: 'http://localhost:5192/api/',
    });
}

export default AxiosInstance;