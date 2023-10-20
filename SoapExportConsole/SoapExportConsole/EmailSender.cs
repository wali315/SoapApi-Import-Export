using System;
using System.Net;
using System.Net.Mail;

public class EmailSender
{
    public void SendEmail()
    {
        SendEmail("", "");
    }

    public void SendEmail(string subject, string messageBody)
    {
        string smtpServer = System.Configuration.ConfigurationManager.AppSettings["SmtpServer"];
        int smtpPort = int.Parse(System.Configuration.ConfigurationManager.AppSettings["SmtpPort"]);
        string senderEmail = System.Configuration.ConfigurationManager.AppSettings["SenderEmail"];
        string senderPassword = System.Configuration.ConfigurationManager.AppSettings["SenderPassword"];
        string receiverEmail = System.Configuration.ConfigurationManager.AppSettings["ReceiverEmail"];

        using (SmtpClient client = new SmtpClient(smtpServer, smtpPort))
        {
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(senderEmail, senderPassword);

            using (MailMessage message = new MailMessage(senderEmail, receiverEmail))
            {
                message.Subject = subject;
                message.Body = messageBody;

                client.Send(message);
                Console.WriteLine("Email Sent");
            }
        }
    }
}