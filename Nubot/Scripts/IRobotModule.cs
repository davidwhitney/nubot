using System;
using Nubot.Adapters;

namespace Nubot.Scripts
{
    public interface IRobotModule
    {
        Action<IMessageChannel, string> FindMatchingOperation(string text);
    }
}