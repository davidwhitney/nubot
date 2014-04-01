using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Nubot.Adapters;

namespace Nubot.Scripts
{
    public abstract class RobotModule : IRobotModule
    {
        public Dictionary<string, Action<IMessageChannel, Envelope>> Respond { get; set; }
        
        protected RobotModule()
        {
            Respond = new Dictionary<string, Action<IMessageChannel, Envelope>>();
        }

        public Action<IMessageChannel, Envelope> FindMatchingOperation(string text)
        {
            foreach (var action in Respond)
            {
                var regex = new Regex(action.Key);

                if (regex.IsMatch(text))
                {
                    return action.Value;
                }
            }

            return ((x, y) => { });
        }
    }
}