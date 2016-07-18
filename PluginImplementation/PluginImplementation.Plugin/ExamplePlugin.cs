using PluginImplementation.Utils;
using System.Collections.Generic;

namespace PluginImplementation.Plugin
{
    public class ExamplePlugin : IPlugin
    {
        public ExamplePlugin() { }

        public string MenuTitle { get { return "Human Resources"; } }
        public string ActionName { get { return "Resources"; } }
        public string ControllerName { get { return "Human"; } }
        public List<string> Menu
        {
            get { return new List<string>(new[] { "Employees", "Recruitment" }); }
        }
    }
}
