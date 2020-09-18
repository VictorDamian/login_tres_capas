namespace Acceso.MailService
{
    //herencia masterMailServer
    //aqui van los datos del servidor
    class SystemSupportMail:MasterMailServer
    {
        //constructor
        public SystemSupportMail()
        {
            senderMail = "youremail@gmail.com";
            password = "yourpass";
            host = "smtp.gmail.com";
            port = 587;
            ssl = true;
            initializeSmtpClient();
        }
    }
}
