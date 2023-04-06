import axios from 'axios';

const API_URL = 'https://localhost:5192/Account/';

class AuthService {

    login(user) {
        return axios.post(API_URL + 'Authenticate', {
            email: user.email,
            password: user.password
        })
        .then(response => {
            console.log(response.data.accessToken);
            if (response.data.accessToken) {
              localStorage.setItem('user', JSON.stringify(response.data))
            }
              return response.data;
        });
        
    }

    logout() {
        localStorage.removeItem('user');
    }

    register(user) {

        console.log('WebClient: email:' + user.email + ' nickname: ' 
                                        + user.nickname + ' password: ' 
                                        + user.password);

        return axios.post('https://localhost:5192/Account/Registration', {
            email: user.email,
            nickname: user.nickname,
            password: user.password         // В дальнейшем захэшировать !!!
        }, {"Content-Type" : "application/json"})
        .then(response => {
            console.log(response.data);
        })
        .catch(function (error) {
            console.log(error.response.data);
        });
    }
}

export default new AuthService();