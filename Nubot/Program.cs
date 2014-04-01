using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Nubot
{
    class Program
    {
        static void Main(string[] args)
        {
            new Robot().RegisterModule(new MapModule()).Listen(new ConsoleAdapter());
        }
    }

    public interface IRobotAdapter : IMessageChannel
    {
        void Open(Action<IMessageChannel, string> onEvent);
    }

    public interface IMessageChannel
    {
        void Send(string response);
        
    }

    public class ConsoleAdapter : IRobotAdapter
    {
        private Action<IMessageChannel, string> _onEvent;

        public void Open(Action<IMessageChannel, string> onEvent)
        {
            _onEvent = onEvent;
        }

        public void Send(string response)
        {
            Console.WriteLine(response);
        }
    }

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

    public interface IRobot
    {
        IRobot RegisterModule(params IRobotModule[] module);
        IRobot Listen(IRobotAdapter adapter);
    }

    public interface IRobotModule
    {
        Robotics FindMatchingOperation(string text);
    }

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

    public class MapModule : RobotModule
    {
        public MapModule()
        {
            Respond["some regex"] = Robotics.Operation((channel, text) => { });
        }
    }

    public enum Messages
    {
        Message,
        TextMessage,
        EnterMessage,
        LeaveMessage,
        TopicMessage,
        CatchAllMessage
    }
}
