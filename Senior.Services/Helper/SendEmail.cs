using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senior.Services.Helper
{
    public class SendEmail
    {
        public string ToEmail { get; }
        public string BodyHtml { get; }
        public string Subject { get; }

        public SendEmail(string toEmail, string bodyHtml, string subject)
        {
            this.ToEmail = toEmail;
            this.BodyHtml = bodyHtml;
            this.Subject = subject;
        }
    }

    public static class EmailHtml
    {
        public static string GetPurchaseEmailHtml(string productName, string productPrice, string imageUrl)
        {
            string style = @"
            <style>
                body {
                    font-family: Arial, sans-serif;
                    background-color: #f4f4f4;
                    text-align: center;
                }
        
                .container {
                    max-width: 400px;
                    margin: 0 auto;
                    padding: 20px;
                    background-color: #fff;
                    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                }
        
                .logo {
                    max-width: 200px;
                    margin: 0 auto;
                    display: block;
                }
        
                h1 {
                    font-size: 24px;
                    margin-bottom: 20px;
                }
        
                p {
                    font-size: 16px;
                    color: #888;
                    margin-bottom: 20px;
                }
        
                .centered-button {
                    text-align: center;
                }
        
                .button {
                    background-color: #007bff;
                    color: #000 !important;
                    text-decoration: none;
                    padding: 10px 20px;
                    display: inline-block;
                    border-radius: 5px;
                }
        
                .small-text {
                    font-size: 12px;
                    color: #888;
                }
            </style>";

            return $@"<!DOCTYPE html>
            <html lang=""en"">
            <head>
                <meta charset=""UTF-8"">
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                <title>Cantidad de registros del día</title>
                
                {style}
            </head>
            <body>
                <div class=""container"">
                    <div class=""centered-button"">
                        <img class=""logo"" src="" {imageUrl} "" alt=""product"">
                        <h1>Purchase</h1>
                        <p>Congratulations! You have purchased the product {productName} with a value of {productPrice}.</p>
                        <p class=""small-text"">This is an automated email, please do not reply. Contact mail@example.com if you have questions.</p>
                    </div>
                </div>
            </body>
            </html>";
        }
    }
}