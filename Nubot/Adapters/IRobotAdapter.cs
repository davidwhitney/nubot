using System;

namespace Nubot.Adapters
{
    public interface IRobotAdapter : IMessageChannel
    {
        void Open(Action<IMessageChannel, string> onEvent);
    }
}