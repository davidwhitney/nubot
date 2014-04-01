using System;
using Nubot.Adapters;

namespace Nubot.Scripts
{
    public class Robotics
    {
        public static readonly Robotics NoOperation = new Robotics();
        public Action<IMessageChannel, string> Handle { get; set; }

        public Robotics(Action<IMessageChannel, string> handle = null)
        {
            Handle = handle ?? ((x, y) => { });
        }

        public static Robotics Operation(Action<IMessageChannel, string> action)
        {
            return new Robotics(action);
        }
    }
}