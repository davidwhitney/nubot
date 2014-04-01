using System;

namespace Nubot.Adapters
{
    public class ConsoleAdapter : IRobotAdapter
    {
        private Action<IMessageChannel, string> _onEvent;

        public void Open(Action<IMessageChannel, string> onEvent)
        {
            _onEvent = onEvent;

            string msg = null;
            while (msg != string.Empty)
            {
                msg = Console.ReadLine().Trim();
                _onEvent(this, msg);
            }

        }

        public void Send(string response)
        {
            Console.WriteLine(response);
        }
    }
}