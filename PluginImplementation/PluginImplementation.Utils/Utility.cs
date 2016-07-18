using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace PluginImplementation.Utils
{
    public class Utility
    {
        public static List<T> GetPlugins<T>(string folder)
        {
            string[] files = Directory.GetFiles(folder, "*.dll");
            List<T> tList = new List<T>();

            foreach (string file in files)
            {
                try
                {
                    Assembly assembly = Assembly.LoadFile(file);
                    foreach (Type type in assembly.GetTypes())
                    {
                        if (!type.IsClass || type.IsNotPublic)
                            continue;

                        Type[] interfaces = type.GetInterfaces();
                        if (((IList)interfaces).Contains(typeof(T)))
                        {
                            object obj = Activator.CreateInstance(type);
                            T t = (T)obj;
                            tList.Add(t);
                        }
                    }
                }
                catch { }
            }

            return tList;
        }
    }
}
