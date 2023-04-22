import AxiosInstance from '@/api_instance';

class AuthService {

    login(user) {
        return AxiosInstance.post('Account/Authenticate', {
            email: user.email,
            password: user.password
        })
        .then(response => {
            console.log("accessToken " + response.data.accessToken);
            console.log("data " + response);
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

        return AxiosInstance.post('Account/Registration', {
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