﻿using MailKit.Security;
using MimeKit;
using todolist_console.Models;

namespace todolist_console.Services
{
    public class MailService
    {
        public static async Task SendMessage(string reciver, string filePath)
        {
            var mailKey = JsonService.LoadData<MailKey>("KeyData.json");
            string password = null;
            if ( VerifyUserPassword(mailKey, out password))
            {
                Console.WriteLine("Your password is correct!");
            }
            else
            {
                Console.WriteLine("Password is incorrect, try again later!");
                File.Delete(filePath);
                return;
            }

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("To-Do-List", mailKey.Email));
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
                            background: radial-gradient(at 18% 99%, rgb(223, 229, 113) 0px, transparent 50%) 
                            repeat scroll 0% 0%, radial-gradient(at 97% 8%, rgb(118, 249, 173) 0px, transparent 50%) 
                            repeat scroll 0% 0%, radial-gradient(at 79% 82%, rgb(233, 109, 131) 0px, transparent 50%) 
                            repeat scroll 0% 0%, radial-gradient(at 96% 10%, rgb(222, 81, 251) 0px, transparent 50%) 
                            repeat scroll 0% 0%, radial-gradient(at 42% 20%, rgb(116, 240, 251) 0px, transparent 50%) 
                            repeat scroll 0% 0%, radial-gradient(at 4% 49%, rgb(203, 88, 218) 0px, transparent 50%) 
                            repeat scroll 0% 0%, rgba(0, 0, 0, 0) radial-gradient(at 57% 33%, rgb(218, 83, 228) 0px, #a299ff 50%) 
                            repeat scroll 0% 0%;
                        }}

                        h1 {{
                            color: #FFF;
                            font-family: 'Montserrat Bold', sans-serif;
                            font-size: 24px;
                            margin-bottom: 10px;
                        }}

                        p {{
                            color: #FFF;
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
                await client.AuthenticateAsync(mailKey.Email, password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            await attachment.Content.Stream.DisposeAsync();
            File.Delete(filePath);
            Console.WriteLine("Message is delivered! Check your email.");
        }

        private static bool VerifyUserPassword(MailKey mailKey, out string password)
        {
            Console.Write("Input a password for email: ");
            password = Console.ReadLine();
            return mailKey.VerifyPassword(password) ? true : false;
        }
    }
}
