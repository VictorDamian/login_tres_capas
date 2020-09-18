using System;
using System.Data;
using Acceso;

namespace Dominio
{
    public class CN_Trabajos
    {
        private UserDao tra = new UserDao();
        public DataTable Mostrar()
        {
            DataTable tabla = new DataTable();
            tabla = tra.MostrarTrabajos();
            return tabla;
        }

        //son de tipo strin por los texbox, la capa de negocio o dominio se encarga de convertirlos
        public void insertarTra(string nombre, string numero, string direccion, string trabajo, string anticipo, string saldo, string total, string fecha)
        {
            //en caso de utilizar valores aqui mismo se convierten, no en la de presetntacion
            tra.insertarTrabajos(nombre, numero, direccion, trabajo, anticipo, saldo,Convert.ToInt32(total),fecha);
        }

        public void editarTra(string nombre, string numero, string direccion, string trabajo, string anticipo, string saldo, string total, string fecha, string id)
        {
            tra.editarTrabajos(nombre, numero, direccion, trabajo, anticipo, saldo, total, fecha,id);

        }

        public void eliminarTra(string id)
        {
            tra.eliminarTrabajos(id);
        }

    }
}
