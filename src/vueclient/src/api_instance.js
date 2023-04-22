import axios from "axios";

const AxiosInstance = axios.create({
    baseURL: 'https://localhost:5192/',
});

export default AxiosInstance;