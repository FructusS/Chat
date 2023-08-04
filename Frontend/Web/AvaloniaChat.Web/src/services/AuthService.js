

class AuthService{
    login(username, password) {
        return axios
          .post(BASE_URL + "/Auth/login", {
            username,
            password
          })
          .then(response => {
            if (response.data.accessToken) {
              localStorage.setItem("accessToken", response.data.data.accessToken);
            }
            return response.data;
          });
      }

}   
export default new AuthService();