namespace CapaTransversal.Cache
{
    //en una clase estatica los valores son permanentes mientras que la aplicacion exista
    //es decir permanece en la memoria mientras la aplicacion esta abierta y puede ser accedida desde cualquier momento
    public static class UserLoginCache
    {
        //pueden ser accedidos desde cualquier capa y realizar operaciones o validaciones
        public static int idUser { get; set; }//id para saber quien registro algo o quien vendio algo
        public static string LoginName { get; set; }
        public static string Password { get; set; }
        public static string firstName { get; set; }
        public static string lastName { get; set; }
        public static string position { get; set; }//el cargo para realizar los permisos de usuario en la capa de presentacion
        public static string email { get; set; }//correo para enviar algun tipo de documento

    }
}
