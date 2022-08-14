﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Automatic9045.AtsEx.PluginHost;
using Automatic9045.AtsEx.PluginHost.BveTypes;
using Automatic9045.AtsEx.PluginHost.ClassWrappers;
using Automatic9045.AtsEx.PluginHost.Resources;

namespace Automatic9045.AtsEx
{
    internal abstract partial class AtsEx : IDisposable
    {
        private static readonly ResourceLocalizer Resources = ResourceLocalizer.FromResXOfType<AtsEx>("Core");

        public BveHacker BveHacker { get; }

        protected AtsEx(Process targetProcess, AppDomain targetAppDomain, Assembly targetAssembly)
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();

            string pluginHostAssemblyPath = Path.Combine(Path.GetDirectoryName(executingAssembly.Location), "AtsEx.PluginHost.dll");
            Assembly pluginHostAssembly = Assembly.LoadFrom(pluginHostAssemblyPath);

            AppDomain.CurrentDomain.AssemblyResolve += (sender, e) =>
            {
                AssemblyName assemblyName = new AssemblyName(e.Name);
                string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), assemblyName.Name + ".dll");
                return File.Exists(path) ? Assembly.LoadFrom(path) : null;
            };

            App.CreateInstance(targetProcess, targetAssembly, pluginHostAssembly);
            BveHacker = new BveHacker(ProfileForDifferentBveVersionLoaded);

            //DXDynamicTextureHost = new DXDynamicTextureHost();
        }

        protected abstract void ProfileForDifferentBveVersionLoaded(Version profileVersion);

        public void Dispose()
        {
            BveHacker.Dispose();
        }
    }
}