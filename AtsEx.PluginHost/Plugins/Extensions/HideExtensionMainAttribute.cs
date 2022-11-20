﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtsEx.PluginHost.Plugins.Extensions
{
    /// <summary>
    /// <see cref="IExtensionSet.GetExtension{TExtension}"/> メソッドからこの AtsEX 拡張機能のメインクラスを取得できないように指定します。
    /// </summary>
    public class HideExtensionMainAttribute : Attribute
    {
        /// <summary>
        /// <see cref="HideExtensionMainAttribute"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public HideExtensionMainAttribute()
        {
        }
    }
}
