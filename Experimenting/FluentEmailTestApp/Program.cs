using System;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;

namespace FluentEmailTestApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var sender = new SmtpSender(()=> new SmtpClient("localhost")
            {
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Port = 25

                // use these configs to send mails to a specific folder
                //DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
                //PickupDirectoryLocation = @"E:\Test"
            });

            var template = new StringBuilder();
            template.AppendLine("Dear @Model.FirstName,");
            template.AppendLine("<p>you bought @Model.ProductName by @Model.Price$</p>");
            template.AppendLine("- Aram Zhamkochyan");

            Email.DefaultSender = sender;
            Email.DefaultRenderer = new RazorRenderer();

            var email = await Email
                .From("aram.j90@gmail.com")
                .To("test@test.com", "Sue")
                .Subject("Thanks!")
                .UsingTemplate(template.ToString(), new{FirstName = "Aram", ProductName = "Horchata", Price = 4})
                //.Body("thanks for buying our product")
                .SendAsync();

            Console.WriteLine($"email with messageId = {email.MessageId} was sent successfully? - {email.Successful}");
            Console.WriteLine("Errors");
            email.ErrorMessages.ForEach(Console.WriteLine);
        }
    }
}
