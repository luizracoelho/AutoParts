using AutoParts.BL.Tools;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AutoParts.BLL.Tools
{
    public class Mail
    {
        private string MensagemHtml { get; set; }

        public Mail(string mensagemHtml)
        {
            MensagemHtml = mensagemHtml;
        }

        public void Enviar(Email email)
        {
            //Valores Smtp
            var emailNome = ConfigurationManager.AppSettings["EmailNome"].ToString();
            var emailEndereco = ConfigurationManager.AppSettings["EmailEndereco"].ToString();
            var emailSenha = ConfigurationManager.AppSettings["EmailSenha"].ToString();
            var smtpHost = ConfigurationManager.AppSettings["SmtpHost"].ToString();
            var smtpPort = ConfigurationManager.AppSettings["SmtpPort"].ToString();
            var smtpEnableSsl = ConfigurationManager.AppSettings["SmtpEnableSsl"].ToString();

            var smtpPortInt = 0;
            int.TryParse(smtpPort, out smtpPortInt);

            var smtpEnableSslBool = Convert.ToBoolean(smtpEnableSsl);

            using (var smtp = new SmtpClient())
            {
                smtp.Host = smtpHost;
                smtp.Port = smtpPortInt;
                smtp.EnableSsl = smtpEnableSslBool;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(emailEndereco, emailSenha);

                using (var mail = new MailMessage())
                {
                    mail.Sender = new MailAddress(emailEndereco, emailNome);
                    mail.From = new MailAddress(emailEndereco, emailNome);
                    mail.To.Add(new MailAddress(email.EnderecoEmail));
                    mail.Subject = email.Assunto;
                    mail.IsBodyHtml = true;
                    mail.Body = MensagemHtml ?? email.Mensagem;

                    smtp.Send(mail);
                }
            }
        }
    }
}
