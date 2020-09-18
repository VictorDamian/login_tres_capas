using System.Data.SqlClient;

namespace Acceso
{
    //clase abtracta, no puede ser intaciada solo como clase base
    public class ConexionSQL
    {
        //privada ya que solo puede ser modificada por la misma clase, solo por el contructor
        private readonly string CadenaConexion;
        public ConexionSQL()
        {
            CadenaConexion = "Server=DANTES\\DANTES;DataBase=MyCompany; Integrated Security = true";
        }
        //protegido ya que solo quiero que sean accedidas de una clase derivada, encapsulado
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(CadenaConexion);
        }
    }
}
