using System;

namespace EventsAndDelegates
{
    class Program
    {
        static void Main(string[] args)
        {
            var geek = new Geek()
            {
                Name = "Dachi",
                Mail = "dachi.gogotchuri@outlook.com",
                Phone = "123456789",
                WebSite = "soydachi.com"
            };

            var contacts = new Contacts(); // publisher
            var mailService = new MailService(); // subscriber
            var messageService = new MessageService(); // suscriber

            contacts.ContactEventHandler += mailService.OnSendMailToGeek;
            contacts.ContactEventHandler += messageService.OnSendMessageToGeek;

            contacts.GetAGeek(geek);

            Console.ReadLine();
        }
    }
}
