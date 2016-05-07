using System;

namespace EventsAndDelegates
{
    public class MailService
    {
        public void OnSendMailToGeek(object source, GeekEventArgs e)
        {
            Console.WriteLine($"MailService: Sendind an email to {e.Geek.Name} ({e.Geek.WebSite}), {e.Geek.Mail}");
        }
    }
}