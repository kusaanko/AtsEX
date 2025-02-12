﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

using BveEx.Launching;
using BveEx.Plugins.Native;
using BveEx.Plugins.Scripting;
using BveEx.PluginHost;
using BveEx.PluginHost.Plugins;

namespace BveEx.Plugins
{
    internal sealed partial class PluginSourceSet
    {
        public static PluginSourceSet ResolvePluginUsingToLoad(PluginType pluginType, bool allowNonPluginAssembly, string vehiclePath)
        {
            string directory = Path.GetDirectoryName(vehiclePath);
            PluginSourceSet plugins = TryLoad(
                Path.Combine(directory, Path.GetFileNameWithoutExtension(vehiclePath) + ".VehiclePluginUsing.xml"),
                Path.Combine(directory, "VehiclePluginUsing.xml"));

            return plugins;


            PluginSourceSet TryLoad(params string[] pathArray)
            {
                foreach (string filePath in pathArray)
                {
                    if (File.Exists(filePath))
                    {
                        return FromPluginUsing(pluginType, allowNonPluginAssembly, filePath);
                    }
                }

                return Empty(pluginType);
            }
        }

        public static PluginSourceSet FromPluginUsing(PluginType pluginType, bool allowNonPluginAssembly, string listPath)
        {
            XDocument doc = XDocument.Load(listPath, LoadOptions.SetLineInfo);

            if (doc.Root.Name.LocalName == "AtsExPluginUsing")
            {
                throw new LaunchModeException();
            }

            doc.Validate(SchemaSet, DocumentValidation);
            XElement root = doc.Element("BveExPluginUsing");

            List<IPluginPackage> pluginPackages = root.Elements().Select<XElement, IPluginPackage>(element =>
            {
                switch (element.Name.LocalName)
                {
                    case "Assembly":
                        return LoadAssemblyPluginPackage(element, listPath);

                    case "CSharpScript":
                        return LoadScriptPluginPackage(ScriptLanguage.CSharpScript, element, Path.GetDirectoryName(listPath));

                    case "IronPython2":
                        return LoadScriptPluginPackage(ScriptLanguage.IronPython2, element, Path.GetDirectoryName(listPath));

                    case "Native":
                        if (pluginType != PluginType.VehiclePlugin)
                        {
                            IXmlLineInfo lineInfo = element;
                            throw new BveFileLoadException(
                                string.Format(Resources.Value.NativeIsOnlyForVehicle.Value, PluginType.VehiclePlugin.GetTypeString()),
                                Path.GetFileName(listPath), lineInfo.LineNumber, lineInfo.LinePosition);
                        }

                        return LoadNativePluginPackage(element, Path.GetDirectoryName(listPath));

                    default:
                        throw new NotImplementedException();
                }
            }).ToList();

            return new PluginSourceSet(Path.GetFileName(listPath), pluginType, allowNonPluginAssembly, pluginPackages);
        }

        private static AssemblyPluginPackage LoadAssemblyPluginPackage(XElement element, string listPath)
        {
            IXmlLineInfo lineInfo = element;

            string assemblyRelativePath = element.Attribute("Path").Value;
            try
            {
                string assemblyPath = Path.Combine(Path.GetDirectoryName(listPath), assemblyRelativePath);
                Assembly assembly = Assembly.LoadFrom(assemblyPath);
                return new AssemblyPluginPackage(GetIdentifier(element), assemblyPath, assembly);
            }
            catch (BadImageFormatException)
            {
                int currentBveVersion = App.Instance.BveAssembly.GetName().Version.Major;
                int otherBveVersion = currentBveVersion == 6 ? 5 : 6;
                throw new BveFileLoadException(
                    string.Format(Resources.Value.BadImageFormat.Value, Path.GetDirectoryName(assemblyRelativePath), otherBveVersion, App.Instance.ProductShortName, currentBveVersion),
                    Path.GetFileName(listPath), lineInfo.LineNumber, lineInfo.LinePosition);;
            }
        }

        private static ScriptPluginPackage LoadScriptPluginPackage(ScriptLanguage scriptLanguage, XElement element, string baseDirectory)
        {
            string packageManifestPath = element.Attribute("PackageManifestPath").Value;
            return ScriptPluginPackage.Load(GetIdentifier(element), scriptLanguage, Path.Combine(baseDirectory, packageManifestPath));
        }

        private static NativePluginPackage LoadNativePluginPackage(XElement element, string baseDirectory)
        {
            string libraryPath = element.Attribute($"Path{(Environment.Is64BitProcess ? "64" : "32")}").Value;
            return new NativePluginPackage(GetIdentifier(element), Path.Combine(baseDirectory, libraryPath));
        }

        private static Identifier GetIdentifier(XElement element)
        {
            string identifierText = (string)element.Attribute("Identifier");
            return identifierText is null ? new RandomIdentifier() : new Identifier(identifierText);
        }

        private static void SchemaValidation(object sender, ValidationEventArgs e) => throw new FormatException(Resources.Value.XmlSchemaValidation.Value, e.Exception);

        private static void DocumentValidation(object sender, ValidationEventArgs e) => throw e.Exception;
    }
}
