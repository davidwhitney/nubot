using System;

namespace Nubot.Adapters
{
    public class ConsoleAdapter : IRobotAdapter
    {
        private Action<IMessageChannel, Envelope> _onEvent;

        public void Listen(Action<IMessageChannel, Envelope> onEvent)
        {
            _onEvent = onEvent;

            string msg = null;
            while (msg != string.Empty)
            {
                msg = Console.ReadLine().Trim();
                _onEvent(this, new Envelope {Room = "Console", User = Environment.UserName, Text = msg});
            }
        }

        public void Send(string response)
        {
            Console.WriteLine(response);
        }
    }
}