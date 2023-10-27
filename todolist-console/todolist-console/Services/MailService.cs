using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;
using System.IO;

namespace todolist_console.Services
{
    public class MailService
    {
        public static async Task SendMessage(string reciver, string filePath)
        {
            const string Mail = "rmarushkevych@gmail.com";
            const string Code = "qfiembgstzbvhumb";

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("To-Do-List", Mail));
            message.To.Add(new MailboxAddress("User", reciver));
            message.Subject = "To-Do-List";

            var builder = new BodyBuilder();
            builder.HtmlBody = $@"<html>
                <head>
                    <style>
                        @import url('https://fonts.googleapis.com/css2?family=Montserrat:wght@400;700&display=swap');
                        .container {{
                            display: block;
                            width: 400px;
                            height: 230px;
                            margin: 0 auto;
                            text-align: center;
                            background-color: #284359;
                            padding: 20px;
                            font-family: 'Montserrat', sans-serif;
                        }}

                        h1 {{
                            color: #F0F5FE;
                            font-family: 'Montserrat Bold', sans-serif;
                            font-size: 24px;
                            margin-bottom: 10px;
                        }}

                        p {{
                            color: #F0F5FE;
                            font-size: 16px;
                        }}
                    </style>
                </head>
                <body>
                    <div class=""container"">
                        <h1>Hello!</h1>
                        <p>Attached to the message below is a file with your cases!</p>
                        <p>Thank you for choosing our app!!<br>
                            Have a good day!</p>
                    </div>
                </body>
            </html>";


            var attachment = new MimePart()
            {
                Content = new MimeContent(File.OpenRead(filePath), ContentEncoding.Default),
                ContentDisposition = new MimeKit.ContentDisposition(MimeKit.ContentDisposition.Attachment)
                {
                    FileName = filePath
                }
            };

            var multipart = new Multipart();
            multipart.Add(builder.ToMessageBody());
            multipart.Add(attachment);

            message.Body = multipart;

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(Mail, Code);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }

            attachment.Content.Stream.Dispose();
            File.Delete(filePath);
        }

    }
}
