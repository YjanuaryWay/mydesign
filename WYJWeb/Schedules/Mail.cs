using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using WYJData;

namespace WYJWeb
{
    public class Mail
    {

        public string From { get; set; } = "iloview@qq.com";
        public string To { get; set; }
        public string Subject { get; set; } = "验证你的邮箱(请勿回复)-@WYJ Lmt.Co.";
        public string Body { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public void Send()
        {
            MailMessage mailMessage = new MailMessage(From, To, Subject, Body)
            {
                IsBodyHtml = true,
            };

            SmtpClient smtpClient = new SmtpClient()
            {
                Credentials = new NetworkCredential(From, "rorbhtwqeeqxedic"),
                Port = 587,
                Host = "smtp.qq.com",
                EnableSsl = true,
            };
            smtpClient.Send(mailMessage);
        }
    }
}
