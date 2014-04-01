using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Nubot
{
    class Program
    {
        static void Main(string[] args)
        {
            new Robot().RegisterModule(new MapModule()).Listen(new ConsoleAdapter());
        }
    }

    public interface IRobotAdapter
    {
    }

    public class ConsoleAdapter : IRobotAdapter
    {
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

        public void Listen(IRobotAdapter adapter)
        {
        }
    }

    public interface IRobot
    {
        IRobot RegisterModule(params IRobotModule[] module);
        IRobot Listen(IRobotAdapter adapter);
    }

    public interface IRobotModule
    {
        
    }

    public abstract class RobotModule : IRobotModule
    {
        public Dictionary<string, Action<MessageChannel>> Respond { get; set; }
        
        protected RobotModule()
        {
            Respond = new Dictionary<string, Action<MessageChannel>>();
        }
    }

    public class MapModule : RobotModule
    {
        public MapModule()
        {
            Respond["some regex"] = msg => { };
        }

    }

    public class MessageChannel
    {
        public void Send(string response)
        {
            
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
