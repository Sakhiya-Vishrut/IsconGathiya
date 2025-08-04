using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace IsconGathiya.Common
{
    public static class EmailHelper
    {
        public static bool SendMail(string to, string subject, string body, string? cc = null)
        {
            using (var client = new SmtpClient(ConfigItems.SMTPHost, ConfigItems.SMTPPort))
            {
                try
                {
                    client.Credentials = new NetworkCredential(ConfigItems.EmailSender, ConfigItems.EmailPassword);
                    client.EnableSsl = true;

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(ConfigItems.EmailSender),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true
                    };

                    mailMessage.To.Add(to);
                    
                    // If CC is provided, split the string and add each CC email
                    if (!string.IsNullOrEmpty(cc))
                    {
                        foreach (var ccEmail in cc.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            mailMessage.CC.Add(ccEmail.Trim());
                        }
                    }

                    client.Send(mailMessage);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public static bool SendMail(string to, string subject, string body, byte[] qrCodeBytes)
        {
            using (var client = new SmtpClient(ConfigItems.SMTPHost, ConfigItems.SMTPPort))
            {
                try
                {
                    // Set the credentials and enable SSL
                    client.Credentials = new NetworkCredential(ConfigItems.EmailSender, ConfigItems.EmailPassword);
                    client.EnableSsl = true;

                    // Create the mail message
                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(ConfigItems.EmailSender),
                        Subject = subject,
                        IsBodyHtml = true // Set HTML support for the body
                    };
                    mailMessage.To.Add(to);

                    // Create an alternate view for the HTML email body
                    var htmlView = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);

                    // Add the QR code image as a LinkedResource
                    LinkedResource qrImage = new LinkedResource(new MemoryStream(qrCodeBytes), MediaTypeNames.Image.Png)
                    {
                        ContentId = "qrcode", // The Content-ID for the image
                        TransferEncoding = TransferEncoding.Base64
                    };
                    htmlView.LinkedResources.Add(qrImage);

                    // Attach the alternate view to the email message
                    mailMessage.AlternateViews.Add(htmlView);

                    // Send the email
                    client.Send(mailMessage);

                    return true;
                }
                catch (Exception ex)
                {
                    // Handle/log exception as necessary
                    return false;
                }
            }
        }
        //public static void SendMail(string to, string subject, string body)
        //{
        //    try
        //    {
        //        MailMessage message = new MailMessage();
        //        message.From = new MailAddress(ConfigItems.EmailSender);
        //        message.Subject = subject;
        //        message.To.Add(new MailAddress(to));
        //        message.Body = body;
        //        message.IsBodyHtml = true;

        //        using (var smtpClient = new SmtpClient(ConfigItems.SMTPHost))
        //        {
        //            smtpClient.Port = 587;
        //            smtpClient.Credentials = new NetworkCredential(ConfigItems.EmailSender, ConfigItems.EmailPassword);
        //            smtpClient.EnableSsl = true;

        //            // Add the certificate validation callback
        //            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) =>
        //            {
        //                // Bypass certificate validation errors (use with caution!)
        //                return true;
        //            };

        //            smtpClient.Send(message);
        //        }

        //    }
        //    catch (Exception) // ex)
        //    {
        //        // throw;
        //    }
        //}
    }
}
