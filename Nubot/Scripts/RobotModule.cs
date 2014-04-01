using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Nubot.Scripts
{
    public abstract class RobotModule : IRobotModule
    {
        public Dictionary<string, Robotics> Respond { get; set; }
        
        protected RobotModule()
        {
            Respond = new Dictionary<string, Robotics>();
        }

        public Robotics FindMatchingOperation(string text)
        {
            foreach (var action in Respond)
            {
                var regex = new Regex(action.Key);

                if (regex.IsMatch(text))
                {
                    return action.Value;
                }
            }

            return Robotics.NoOperation;
        }
    }
}