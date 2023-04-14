/* Сервис для доступа к данным */

import axios from 'axios';
//import authHeader from './auth-header';

const API_URL = 'https://localhost:5192/';

class UserService {
    getPublicContent() {
        return axios.get(API_URL);
    }

    getUserContent() {
      return axios.get(API_URL + 'user', { headers:  authHeader()  });
    }

    getModeratorPanel() {
      return axios.get(API_URL + 'moder', { headers:  authHeader()  });
    }

    getAdministratorPanel() {
      return axios.get(API_URL + 'admin', { headers:  authHeader()  });
    }
}