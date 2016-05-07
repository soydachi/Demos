using System;

namespace EventsAndDelegates
{
    public class MessageService
    {
        public void OnSendMessageToGeek(object source, GeekEventArgs e)
        {
            Console.WriteLine($"MessageService: Sending an message to {e.Geek.Name} ({e.Geek.WebSite}), {e.Geek.Phone}");
        }
    }
}