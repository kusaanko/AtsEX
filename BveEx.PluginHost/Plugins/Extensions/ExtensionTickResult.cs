﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BveEx.PluginHost.Plugins.Extensions
{
    /// <summary>
    /// 拡張機能の <see cref="PluginBase.Tick(TimeSpan)"/> メソッドの実行結果を表します。
    /// </summary>
    public class ExtensionTickResult : IPluginTickResult
    {
        /// <summary>
        /// <see cref="ExtensionTickResult"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public ExtensionTickResult()
        {
        }
    }
}
