﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AtsEx.PluginHost;
using AtsEx.PluginHost.Plugins;
using AtsEx.PluginHost.Plugins.Extensions;

namespace AtsEx.Plugins
{
    internal sealed class PluginBuilder : PluginHost.Plugins.PluginBuilder
    {
        public PluginLoader CreatedBy { get; }

        protected override event AllExtensionsLoadedEventHandler AllExtensionsLoaded;
        protected override event AllPluginsLoadedEventHandler AllPluginsLoaded;

        public PluginBuilder(INative native, PluginHost.IBveHacker bveHacker, IExtensionSet extensions, string identifier, PluginLoader createdBy)
            : base(native, bveHacker, extensions, identifier)
        {
            CreatedBy = createdBy;
            CreatedBy.AllExtensionsLoaded += e => AllExtensionsLoaded?.Invoke(e);
            CreatedBy.AllPluginsLoaded += e => AllPluginsLoaded?.Invoke(e);
        }

        public AllExtensionsLoadedEventHandler GetAllExtensionsLoaded() => AllExtensionsLoaded;
        public AllPluginsLoadedEventHandler GetAllPluginsLoaded() => AllPluginsLoaded;
    }
}
