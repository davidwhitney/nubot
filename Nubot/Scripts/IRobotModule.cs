namespace Nubot.Scripts
{
    public interface IRobotModule
    {
        Robotics FindMatchingOperation(string text);
    }
}