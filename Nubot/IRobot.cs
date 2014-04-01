using Nubot.Adapters;
using Nubot.Scripts;

namespace Nubot
{
    public interface IRobot
    {
        IRobot RegisterModule(params IRobotModule[] module);
        IRobot Listen(IRobotAdapter adapter);
    }
}