using Nubot.Adapters;
using Nubot.Scripts;

namespace Nubot
{
    public interface IRobot
    {
        IRobot RegisterModule(params IRobotModule[] module);
        void Hear(IMessageChannel adapter, Envelope msg);
    }
}