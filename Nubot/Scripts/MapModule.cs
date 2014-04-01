namespace Nubot.Scripts
{
    public class MapModule : RobotModule
    {
        public MapModule()
        {
            Respond["some regex"] = (channel, text) => { };
        }
    }
}