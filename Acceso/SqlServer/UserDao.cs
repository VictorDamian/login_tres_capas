using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CapaTransversal.Cache;

namespace Acceso
{
    //objeto de acceso de datos del usuario
    //hereda de la clase conexion
    public class UserDao:ConexionSQL
    {

        private ConexionSQL con = new ConexionSQL();//variable encapsulada
        SqlDataReader leer;//leer fila de la tabla productos
        DataTable tabla = new DataTable();//para almacenar las filas de la consulta que realizara el reader
        SqlCommand comando = new SqlCommand();//para ejecutar transacciones 

        public void editProfile(int id, string userName, string password, string name, string lastName, string mail)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "update Users set LoginName=@userName, Password=@pass, FirstName=@name, LastName=@lastName, Email=@mail where UserID=@id";
                    command.Parameters.AddWithValue("@userName", userName);
                    command.Parameters.AddWithValue("@pass", password);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@lastName", lastName);
                    command.Parameters.AddWithValue("@mail", mail);
                    command.Parameters.AddWithValue("@id", id);
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }

        public bool login(string user, string pass)
        {
            /*no es necesario cerrar conexion, using garantiza llamar los metodos desechabeles, es decir el objeto
            el objeto de la conexion existira hasta que las filas dentro del bloque using terminen de ejecutarse
            al temrinar simplemente desechara y liberara los recursos utilizados*/
            using (var conexion = GetConnection())
            {
                conexion.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexion;
                    comando.CommandText = "select * from Users where LoginName=@user and Password =@pass";
                    comando.Parameters.AddWithValue("@user", user);
                    comando.Parameters.AddWithValue("@pass", pass);
                    comando.CommandType = CommandType.Text;
                    SqlDataReader reader = comando.ExecuteReader();

                    //si la consulta existe
                    if (reader.HasRows)
                    {
                        //mientras lea las filas agregamos los valores de la columna a los campos de la clase estatica
                        while (reader.Read())
                        {
                            //valores en memoria para uso en cualquier momento
                            UserLoginCache.idUser = reader.GetInt32(0);
                            UserLoginCache.firstName = reader.GetString(3);
                            UserLoginCache.lastName = reader.GetString(4);
                            UserLoginCache.position = reader.GetString(5);
                            UserLoginCache.email = reader.GetString(6);
                        }return true;
                    }else return false;
                }
            }
        }
        //metodo para obtener el dato del usuario solicitante
        public string recoverPassword(string userRequestion)
        {
            using (var connection= GetConnection())//obetener conexion
            {
                connection.Open();
                using (var command = new SqlCommand())//intancia
                {
                    command.Connection = connection;//especificamos conexio al comando
                    command.CommandText = "select*from Users where LoginName=@user or Email=@mail";//igual a esto
                    command.Parameters.AddWithValue("@user", userRequestion);
                    command.Parameters.AddWithValue("@mail", userRequestion);
                    command.CommandType = CommandType.Text;
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read() == true)
                    {
                        string userName = reader.GetString(3) + ", " + reader.GetString(4);
                        string userMail = reader.GetString(6);
                        string accountPassword = reader.GetString(2);

                        var mailService = new MailService.SystemSupportMail();
                        mailService.sendMail(
                            subject: "System: Password recovery request",
                            body: "Hi, "+userName+"\nYou request to recovery your password.\n"+
                            "Your current password is:"+accountPassword+
                            "\nHowever, we ask that you change password inmediately once you enter the system.",
                            recipienMail: new List<string> { userMail });
                        return "Hi, " + userName + "\nYou request to recovery your password.\n" +
                            "Please check your email: "+userMail+
                            "\nHowever, we ask that you change password inmediately once you enter the system.";
                    }else{return "Sorry, you dont have an account with this mail or username";
                    }
                }
            }
        }

        private void metodoValidarUsr()
        {
            if (UserLoginCache.position==Positions.Administrador || UserLoginCache.position==Positions.Contador)
            {
                //codigo
            }
            if (UserLoginCache.position==Positions.Ingeniero)
            {
                //codigo
            }
        }

        public DataTable MostrarTrabajos()
        {
            //transact sql
            using (var conexion = GetConnection())
            {
                conexion.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexion;
                    comando.CommandText = "select*from trabajos";
                    comando.CommandType = CommandType.Text;
                    SqlDataReader reader = comando.ExecuteReader();//para devolver filas
                    tabla.Load(reader);
                    return tabla;
                }
            }
        }

        public void insertarTrabajos(string nombre, string numero, string direccion, string trabajo, string anticipo, string saldo, int total, string fecha)
        {
            using (var conexion = GetConnection())
            {
                conexion.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexion;
                    comando.CommandText = "insert into trabajos values ('"+nombre+"','"+numero+"','"+direccion+"','"+trabajo+"','"+anticipo+"','"+ saldo +"','" + total +"','"+fecha+"')";
                    comando.ExecuteNonQuery();//solo para instrucciones insert, update, delate
                }
            }
        }

        public void editarTrabajos(string nombre, string numero, string direccion, string trabajo, string anticipo, string saldo, string total, string fecha, string id)
        {
            using (var conexion = GetConnection())
            {
                conexion.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexion;
                    comando.CommandText = "update trabajos set nom= '" + nombre + "', num='" + numero + "', dire='" + direccion + "', tra='" + trabajo + "', anti='" + anticipo + "', sal='" + saldo + "', total='" + total + "', fecha='" + fecha + "' WHERE id = '" + id + "'";
                    comando.ExecuteNonQuery();//solo para instrucciones insert, update, delate
                }
            }
        }
        public void eliminarTrabajos(string id)
        {
            using (var conexion = GetConnection())
            {
                conexion.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexion;
                    comando.CommandText = "DELETE FROM trabajos WHERE id = '" + id + "'";
                    comando.ExecuteNonQuery();//solo para instrucciones insert, update, delate
                }
            }
        }
    }
}

