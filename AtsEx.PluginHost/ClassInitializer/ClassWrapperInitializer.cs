﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Automatic9045.AtsEx.PluginHost.BveTypes;

namespace Automatic9045.AtsEx.PluginHost.ClassWrappers
{
    public sealed class ClassWrapperInitializer : ClassInitializerBase
    {
        private static event LazyInitializationRequestedEventHandler LazyInitializationRequested;

        public static BveTypeSet LazyInitialize() => LazyInitializationRequested.Invoke(EventArgs.Empty);


        public ClassWrapperInitializer(IApp app, IBveHacker bveHacker) : base(app, bveHacker)
        {
            if (LazyInitializationRequested is null) LazyInitializationRequested = _ => BveTypeSet.Instance;
        }

        public override void InitializeAll()
        {
            Type[] allTypes = App.AtsExPluginHostAssembly.GetTypes();
            IEnumerable<Type> classWrapperTypes = allTypes.Where(t => t == typeof(ClassWrapperBase) || t.IsSubclassOf(typeof(ClassWrapperBase)));

            Initialize<InitializeClassWrapperAttribute>(classWrapperTypes, method =>
            {
                ParameterInfo[] parameters = method.GetParameters();
                return parameters.Length == 1 && parameters[0].ParameterType == typeof(BveTypeSet);
            }, new object[] { BveTypeSet.Instance });
        }


        private delegate BveTypeSet LazyInitializationRequestedEventHandler(EventArgs e);
    }
}
