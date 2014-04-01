using System;
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
            adapter.Open(CallbackOnNewMessage);
            return this;
        }

        public void CallbackOnNewMessage(IMessageChannel adapter, Envelope msg)
        {
            foreach (var handler in Modules.Select(module => module.FindMatchingOperation(msg.Text)))
            {
                try
                {
                    handler(adapter, msg);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("handler failed " + ex);
                }
            }
        }
    }
}