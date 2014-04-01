using Nubot.Adapters;
using Nubot.Scripts;

namespace Nubot
{
    class Program
    {
        static void Main(string[] args)
        {
            var robot = new Robot()
                .RegisterModule(
                    new GoogleImagesModule(),
                    new MapModule()
                )
                .Listen(new ConsoleAdapter());
        }
    }
}
