using System;

namespace Nubot.Adapters
{
    public interface IRobotAdapter : IMessageChannel
    {
        void Listen(Action<IMessageChannel, Envelope> onEvent);
    }
}