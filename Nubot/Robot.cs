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

        public void Hear(IMessageChannel adapter, Envelope msg)
        {
            foreach (var response in Modules.Select(module => module.FindMatchingOperation(msg.Text)))
            {
                Respond(response, msg, adapter);
            }
        }

        private static void Respond(Action<IMessageChannel, Envelope> respond, Envelope msg, IMessageChannel adapter)
        {
            try
            {
                respond(adapter, msg);
            }
            catch (Exception ex)
            {
                Console.WriteLine("handler failed " + ex);
            }
        }
    }
}