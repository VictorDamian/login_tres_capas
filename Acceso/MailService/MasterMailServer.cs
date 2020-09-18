using System;
using System.Net;
using System.Collections.Generic;
using System.Net.Mail;

namespace Acceso.MailService
{
    //clase maestra para enviar uno o varios correos
    //es abtracta ya que no deseo que sea intanciada, consumida directamente o mediante la herencia
    public abstract class MasterMailServer
    {
        //indico que es privada porque solo quiero que sea usada por la clase
        //priados ya que solo puden ser accedidos por la clase o las que deriven de ella
        private SmtpClient smtpClient;
        protected string senderMail { get; set; }//remitente
        protected string password { get; set; }
        protected string host { get; set; }
        protected int port { get; set; }
        protected bool ssl { get; set; }

        protected void initializeSmtpClient()
        {
            //configuracion basica para protocolo simple de tranferencia de correo
            smtpClient = new SmtpClient();//intacnia la clase
            smtpClient.Credentials = new NetworkCredential(senderMail,password);//especificamos credenciales
            smtpClient.Host = host;
            smtpClient.Port = port;
            smtpClient.EnableSsl = ssl;
        }

        //metodo publico para enviar msj de correo
        //se necesesita cuerpo del correo
        //quien lo recibe sera de tipo lista ya que las empresas suelen mandar correo a varios remitentes
        public void sendMail(string subject, string body, List<string> recipienMail)
        {
            var mailMessage = new MailMessage();
            try
            {
                //contruccion del mensaje
                mailMessage.From = new MailAddress(senderMail);//cargamos de quien es el correo
                //como el parametro es una lista, necesitamos agregar las direcciones de correo de ls lista mediante un ciclo
                foreach (string mail in recipienMail)
                {
                    mailMessage.To.Add(mail);//quien recibira el mensajes
                }
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.Priority = MailPriority.Normal;
                smtpClient.Send(mailMessage);//enviamos el msj con el protocolo de transferencia de correo
            }
            catch (Exception ex) { }
            finally
            {
                //desechar recursos
                mailMessage.Dispose();
                smtpClient.Dispose();
            }
        }
    }
}
