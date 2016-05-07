using System;
using System.Threading;

namespace EventsAndDelegates
{
    public class GeekEventArgs : EventArgs
    {
        public Geek Geek { get; set; }
    }

    public class Contacts
    {
        // 1- Define a delegate
        // 2- Define an event based on that delegate
        // 3- Raise the event

        // EventHandler
        // EventHandler<TEventArgs>

        public event EventHandler<GeekEventArgs> ContactEventHandler;

        public void GetAGeek(Geek geek)
        {
            Console.WriteLine("Searching for a Geek in your contacts...");
            Thread.Sleep(3000);

            OnContacts(geek);
        }

        protected virtual void OnContacts(Geek geek)
        {
            ContactEventHandler?.Invoke(this, new GeekEventArgs() {Geek = geek});
        }
    }
}
