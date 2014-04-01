using System.Collections.Generic;
using System.Linq;
using Nubot.Adapters;
using Nubot.Scripts;

namespace Nubot
{
    public class Robot : IRobot
    {
        public List<IRobotModule> Modules { get; private set; }

        public Robot()
        {
            Modules = new List<IRobotModule>();
        }

        public IRobot RegisterModule(params IRobotModule[] module)
        {
            Modules.AddRange(module);
            return this;
        }

        public IRobot Listen(IRobotAdapter adapter)
        {
            adapter.Open(OnEvent);
            return this;
        }

        public void OnEvent(IMessageChannel adapter, string text)
        {
            foreach (var handler in Modules.Select(module => module.FindMatchingOperation(text)))
            {
                handler.Handle(adapter, text);
            }
        }
    }
}