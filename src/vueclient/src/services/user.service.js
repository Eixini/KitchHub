/* Сервис для доступа к данным */

import AxiosInstance from '@/api_instance';
//import authHeader from './auth-header';

class UserService {
    getPublicContent() {
        return AxiosInstance.get();
    }

    getUserContent() {
      return AxiosInstance.get('user', { headers:  authHeader()  });
    }

    getModeratorPanel() {
      return AxiosInstance.get('moder', { headers:  authHeader()  });
    }

    getAdministratorPanel() {
      return AxiosInstance.get('admin', { headers:  authHeader()  });
    }
}