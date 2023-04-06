/*  Метод для получения данных с сервера.
    В случае, если происходит обращение к защищенным ресурсам,
    HTTP-запрос требует заголовка авторизации. 
*/

export default function authHeader(){

    // Проверка локальное хранилище на наличие пользовательского элемента.
    let user = JSON.parse(localStorage.getItem('user'));

    // Если есть зарегистрированный пользователь с accessToken (JWT)
    if(user && user.accessToken){
        // Возвращается заголовок авторизации HTTP
        return { Authorization: 'Bearer ' + user.accessToken };
    }
    else {
        // Иначе возвращается пустой объект
        return {};
    }
}