using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template
{
    class Program
    {
        static void Main(string[] args)
        {
            var c1= new Contact {FullName = "Aram Zhamkochyan", CompanyName = "BetConstruct", Email = "aram532@yandex.ru"};
            var text =
                "Dear {{Name}}, soon you'll get a job in {{CompanyName}}, don't forget to check your mentioned email: {{Email}}";
            Console.WriteLine(MailCreator(text, c1));

            Console.ReadKey();
        }

        private static string MailCreator(string text, Contact contact)
        {
            return text.Replace("{{Name}}",contact.FullName).Replace("{{CompanyName}}",contact.CompanyName ).Replace("{{Email}}",contact.Email);
        }
    }

    class Contact
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
    }
}
