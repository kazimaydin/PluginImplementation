using System.Collections.Generic;

namespace PluginImplementation.Utils
{
    public interface IPlugin
    {
        string MenuTitle { get; }
        string ActionName { get; }
        string ControllerName { get; }
        List<string> Menu { get; }
    }
}
