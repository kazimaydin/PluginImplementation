using PluginImplementation.Utils.Cache;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace PluginImplementation.Utils
{
    public class Utility
    {
        private static IEnumerable<IPlugin> GetOrCreateCacheForActivator<IPlugin>(IQueryable<IPlugin> query, string folder)
        {
            EFCacheExtensions.SetCacheProvider(MemoryCacheProvider.GetInstance());
            MemoryCacheProvider target = MemoryCacheProvider.GetInstance();

            return target.GetOrCreateCacheForActivator<IPlugin>(query, folder);
        }

        public static bool RemoveFromCacheForActivator()
        {
            EFCacheExtensions.SetCacheProvider(MemoryCacheProvider.GetInstance());
            MemoryCacheProvider target = MemoryCacheProvider.GetInstance();

            return target.RemoveFromCacheForActivator();
        }

        public static List<IPlugin> GetPluginsFromCache(string folder)
        {
            try
            {
                IQueryable<IPlugin> iq = null;
                return GetOrCreateCacheForActivator<IPlugin>(iq, folder).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<T> GetPlugins<T>(string folder)
        {
            try
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
            catch (Exception)
            {
                throw;
            }
        }
    }
}