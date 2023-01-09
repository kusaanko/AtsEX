﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BveTypes.ClassWrappers;
using FastMember;
using ObjectiveHarmonyPatch;

namespace AtsEx.Extensions.SignalPatch
{
    /// <summary>
    /// 特定の閉塞の信号現示を自由に変更できるようにするパッチを表します。
    /// </summary>
    public sealed class SignalPatch : IDisposable
    {
        private readonly Section Target;
        private readonly HarmonyPatch HarmonyPatch;

        internal SignalPatch(FastMethod getCurrentSignalIndexMethod, SectionManager sectionManager, Section target, Converter<int, int> factory)
        {
            Target = target;

            HarmonyPatch = HarmonyPatch.Patch(getCurrentSignalIndexMethod.Source, PatchTypes.Prefix);
            HarmonyPatch.Prefix += Prefix;


            PatchInvokationResult Prefix(object sender, PatchInvokedEventArgs e)
            {
                if (e.Instance != Target.Src) return new PatchInvokationResult();

                int sectionIndexDifference;
                for (int i = 0; true; i++)
                {
                    if (i >= target.SectionIndexesTrainOn.Count)
                    {
                        sectionIndexDifference = target.SignalIndexes.Length - 1;
                        break;
                    }

                    if (target.SectionIndexesTrainOn[i] >= target.SectionCount)
                    {
                        sectionIndexDifference = Math.Min(target.SectionIndexesTrainOn[i] - target.SectionCount, target.SignalIndexes.Length - 1);
                        break;
                    }
                }

                int source = target.SignalIndexes[sectionIndexDifference];
                int converted = factory(source);

                return new PatchInvokationResult(converted, true);
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            HarmonyPatch.Dispose();
        }
    }
}
