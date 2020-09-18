using System;
using Acceso;
using CapaTransversal.Cache;

namespace Dominio
{
    public class UserModel
    {
        //instacia
        UserDao userDao = new UserDao();

        //Attributes
        private int idUser;
        private string loginName;
        private string password;
        private string firstName;
        private string lastName;
        private string position;
        private string email;
        //Constructors
        
        public UserModel()
        {
        }

        public UserModel(int idUser, string loginName, string password, string firstName, string lastName, string position, string email)
        {
            this.idUser = idUser;
            this.loginName = loginName;
            this.password = password;
            this.firstName = firstName;
            this.lastName = lastName;
            this.position = position;
            this.email = email;
        }

        //Methods
        public string editUserProfile()
        {
            try
            {
                userDao.editProfile(idUser, loginName, password, firstName, lastName, email);
                LoginUsers(loginName, password);
                return "Your profile has been successfully updated";
            }
            catch (Exception ex)
            {
                return "Username is already registered, try another";
            }
        }

        public bool LoginUsers(string user, string pass)
        {
            return userDao.login(user, pass);
        }

        public string recoverPassword(string userRequesting)
        {
            return userDao.recoverPassword(userRequesting);
        }

        //seguridad y permisos se aplican en la capa de dominio o negocio donde tiene todas las validaciones y logica
        private void metodoValidarUsr()
        {
            if (UserLoginCache.position == Positions.Administrador || UserLoginCache.position == Positions.Contador)
            {
                //codigo
            }
            if (UserLoginCache.position == Positions.Ingeniero)
            {
                //codigo
            }
        }
    }
}
