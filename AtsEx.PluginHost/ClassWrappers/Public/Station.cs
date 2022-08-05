﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Automatic9045.AtsEx.PluginHost.BveTypes;

namespace Automatic9045.AtsEx.PluginHost.ClassWrappers
{
    /// <summary>
    /// 停車場を表します。
    /// </summary>
    public sealed class Station : MapObjectBase
    {
        [InitializeClassWrapper]
        private static void Initialize(BveTypeSet bveTypes)
        {
            ClassMemberSet members = bveTypes.GetClassInfoOf<Station>();

            Constructor = members.GetSourceConstructor();

            NameGetMethod = members.GetSourcePropertyGetterOf(nameof(Name));
            NameSetMethod = members.GetSourcePropertySetterOf(nameof(Name));

            ArrivalTimeGetMethod = members.GetSourcePropertyGetterOf(nameof(ArrivalTimeMilliseconds));
            ArrivalTimeSetMethod = members.GetSourcePropertySetterOf(nameof(ArrivalTimeMilliseconds));

            DepertureTimeGetMethod = members.GetSourcePropertyGetterOf(nameof(DepertureTimeMilliseconds));
            DepertureTimeSetMethod = members.GetSourcePropertySetterOf(nameof(DepertureTimeMilliseconds));

            DoorCloseTimeGetMethod = members.GetSourcePropertyGetterOf(nameof(DoorCloseTimeMilliseconds));
            DoorCloseTimeSetMethod = members.GetSourcePropertySetterOf(nameof(DoorCloseTimeMilliseconds));

            DefaultTimeGetMethod = members.GetSourcePropertyGetterOf(nameof(DefaultTimeMilliseconds));
            DefaultTimeSetMethod = members.GetSourcePropertySetterOf(nameof(DefaultTimeMilliseconds));

            PassGetMethod = members.GetSourcePropertyGetterOf(nameof(Pass));
            PassSetMethod = members.GetSourcePropertySetterOf(nameof(Pass));

            IsTerminalGetMethod = members.GetSourcePropertyGetterOf(nameof(IsTerminal));
            IsTerminalSetMethod = members.GetSourcePropertySetterOf(nameof(IsTerminal));

            StoppageTimeGetMethod = members.GetSourcePropertyGetterOf(nameof(StoppageTimeMilliseconds));
            StoppageTimeSetMethod = members.GetSourcePropertySetterOf(nameof(StoppageTimeMilliseconds));

            DoorSideGetMethod = members.GetSourcePropertyGetterOf(nameof(DoorSide));
            DoorSideSetMethod = members.GetSourcePropertySetterOf(nameof(DoorSide));

            DepertureSoundGetMethod = members.GetSourcePropertyGetterOf(nameof(DepertureSound));
            DepertureSoundSetMethod = members.GetSourcePropertySetterOf(nameof(DepertureSound));

            ArrivalSoundGetMethod = members.GetSourcePropertyGetterOf(nameof(ArrivalSound));
            ArrivalSoundSetMethod = members.GetSourcePropertySetterOf(nameof(ArrivalSound));

            SignalFlagGetMethod = members.GetSourcePropertyGetterOf(nameof(SignalFlag));
            SignalFlagSetMethod = members.GetSourcePropertySetterOf(nameof(SignalFlag));

            MarginMaxGetMethod = members.GetSourcePropertyGetterOf(nameof(MarginMax));
            MarginMaxSetMethod = members.GetSourcePropertySetterOf(nameof(MarginMax));

            MarginMinGetMethod = members.GetSourcePropertyGetterOf(nameof(MarginMin));
            MarginMinSetMethod = members.GetSourcePropertySetterOf(nameof(MarginMin));

            MinStopPositionGetMethod = members.GetSourcePropertyGetterOf(nameof(MinStopPosition));

            MaxStopPositionGetMethod = members.GetSourcePropertyGetterOf(nameof(MaxStopPosition));

            AlightingTimeGetMethod = members.GetSourcePropertyGetterOf(nameof(AlightingTimeMilliseconds));
            AlightingTimeSetMethod = members.GetSourcePropertySetterOf(nameof(AlightingTimeMilliseconds));

            TargetLoadFactorGetMethod = members.GetSourcePropertyGetterOf(nameof(TargetLoadFactor));
            TargetLoadFactorSetMethod = members.GetSourcePropertySetterOf(nameof(TargetLoadFactor));

            CurrentLoadFactorGetMethod = members.GetSourcePropertyGetterOf(nameof(CurrentLoadFactor));
            CurrentLoadFactorSetMethod = members.GetSourcePropertySetterOf(nameof(CurrentLoadFactor));

            DoorReopenGetMethod = members.GetSourcePropertyGetterOf(nameof(DoorReopen));
            DoorReopenSetMethod = members.GetSourcePropertySetterOf(nameof(DoorReopen));

            StuckInDoorGetMethod = members.GetSourcePropertyGetterOf(nameof(StuckInDoorMilliseconds));
            StuckInDoorSetMethod = members.GetSourcePropertySetterOf(nameof(StuckInDoorMilliseconds));
        }

        private Station(object src) : base(src)
        {
        }

        /// <summary>
        /// オリジナル オブジェクトからラッパーのインスタンスを生成します。
        /// </summary>
        /// <param name="src">ラップするオリジナル オブジェクト。</param>
        /// <returns>オリジナル オブジェクトをラップした <see cref="Station"/> クラスのインスタンス。</returns>
        [CreateClassWrapperFromSource]
        public static Station FromSource(object src)
        {
            if (src is null) return null;
            return new Station(src);
        }

        private static ConstructorInfo Constructor;
        /// <summary>
        /// 停車場の名前を指定して <see cref="Station"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <remarks>
        /// <paramref name="name"/> には BVE 上に表示される名前を指定します。停車場名 (停車場リストファイルで定義した文字列) とは異なります。
        /// </remarks>
        /// <param name="name">停車場の名前。</param>
        public Station(string name) : this(Constructor.Invoke(new object[] { name }))
        {
        }

        private static MethodInfo NameGetMethod;
        private static MethodInfo NameSetMethod;
        /// <summary>
        /// 停車場の名前を取得・設定します。
        /// </summary>
        /// <remarks>
        /// BVE 上に表示される名前です。停車場名 (停車場リストファイルで定義した文字列) とは異なります。
        /// </remarks>
        public string Name
        {
            get => NameGetMethod.Invoke(Src, null);
            set => NameSetMethod.Invoke(Src, new object[] { value });
        }

        private static MethodInfo ArrivalTimeGetMethod;
        private static MethodInfo ArrivalTimeSetMethod;
        /// <summary>
        /// 到着時刻をミリ秒単位で取得・設定します。
        /// </summary>
        /// <value>0 時丁度から到着時刻までに経過したミリ秒数 [ms]。</value>
        public int ArrivalTimeMilliseconds
        {
            get => ArrivalTimeGetMethod.Invoke(Src, null);
            set => ArrivalTimeSetMethod.Invoke(Src, new object[] { value });
        }

        /// <summary>
        /// 到着時刻を取得・設定します。
        /// </summary>
        public TimeSpan ArrivalTime
        {
            get => TimeSpan.FromMilliseconds(ArrivalTimeMilliseconds);
            set => ArrivalTimeMilliseconds = (int)value.TotalMilliseconds;
        }

        private static MethodInfo DepertureTimeGetMethod;
        private static MethodInfo DepertureTimeSetMethod;
        /// <summary>
        /// 発車時刻または通過時刻をミリ秒単位で取得・設定します。
        /// </summary>
        /// <value>0 時丁度から発車時刻または通過時刻までに経過したミリ秒数 [ms]。</value>
        public int DepertureTimeMilliseconds
        {
            get => DepertureTimeGetMethod.Invoke(Src, null);
            set => DepertureTimeSetMethod.Invoke(Src, new object[] { value });
        }

        /// <summary>
        /// 発車時刻または通過時刻を取得・設定します。
        /// </summary>
        public TimeSpan DepertureTime
        {
            get => TimeSpan.FromMilliseconds(DepertureTimeMilliseconds);
            set => DepertureTimeMilliseconds = (int)value.TotalMilliseconds;
        }

        private static MethodInfo DoorCloseTimeGetMethod;
        private static MethodInfo DoorCloseTimeSetMethod;
        /// <summary>
        /// ドアが閉まるのに要する時間をミリ秒単位で取得・設定します。
        /// </summary>
        public int DoorCloseTimeMilliseconds
        {
            get => DoorCloseTimeGetMethod.Invoke(Src, null);
            set => DoorCloseTimeSetMethod.Invoke(Src, new object[] { value });
        }

        /// <summary>
        /// ドアが閉まるのに要する時間を取得・設定します。
        /// </summary>
        public TimeSpan DoorCloseTime
        {
            get => TimeSpan.FromMilliseconds(DoorCloseTimeMilliseconds);
            set => DoorCloseTimeMilliseconds = (int)value.TotalMilliseconds;
        }

        private static MethodInfo DefaultTimeGetMethod;
        private static MethodInfo DefaultTimeSetMethod;
        /// <summary>
        /// 駅にジャンプしたときの時刻をミリ秒単位で取得・設定します。
        /// </summary>
        /// <value>0 時丁度から駅にジャンプしたときの時刻までに経過したミリ秒数 [ms]。</value>
        public int DefaultTimeMilliseconds
        {
            get => DefaultTimeGetMethod.Invoke(Src, null);
            set => DefaultTimeSetMethod.Invoke(Src, new object[] { value });
        }

        /// <summary>
        /// 駅にジャンプしたときの時刻を取得・設定します。
        /// </summary>
        public TimeSpan DefaultTime
        {
            get => TimeSpan.FromMilliseconds(DefaultTimeMilliseconds);
            set => DefaultTimeMilliseconds = (int)value.TotalMilliseconds;
        }

        private static MethodInfo PassGetMethod;
        private static MethodInfo PassSetMethod;
        /// <summary>
        /// この停車場を通過するかどうかを取得・設定します。
        /// </summary>
        public bool Pass
        {
            get => PassGetMethod.Invoke(Src, null);
            set => PassSetMethod.Invoke(Src, new object[] { value });
        }

        private static MethodInfo IsTerminalGetMethod;
        private static MethodInfo IsTerminalSetMethod;
        /// <summary>
        /// この停車場が終点かどうかを取得・設定します。
        /// </summary>
        public bool IsTerminal
        {
            get => IsTerminalGetMethod.Invoke(Src, null);
            set => IsTerminalSetMethod.Invoke(Src, new object[] { value });
        }

        private static MethodInfo StoppageTimeGetMethod;
        private static MethodInfo StoppageTimeSetMethod;
        /// <summary>
        /// 標準停車時間をミリ秒単位で取得・設定します。
        /// </summary>
        public int StoppageTimeMilliseconds
        {
            get => StoppageTimeGetMethod.Invoke(Src, null);
            set => StoppageTimeSetMethod.Invoke(Src, new object[] { value });
        }

        /// <summary>
        /// 標準停車時間を取得・設定します。
        /// </summary>
        public TimeSpan StoppageTime
        {
            get => TimeSpan.FromMilliseconds(StoppageTimeMilliseconds);
            set => StoppageTimeMilliseconds = (int)value.TotalMilliseconds;
        }

        private static MethodInfo DoorSideGetMethod;
        private static MethodInfo DoorSideSetMethod;
        /// <summary>
        /// 開くドアの方向を取得・設定します。
        /// </summary>
        public int DoorSide
        {
            get => DoorSideGetMethod.Invoke(Src, null);
            set => DoorSideSetMethod.Invoke(Src, new object[] { value });
        }

        private static MethodInfo DepertureSoundGetMethod;
        private static MethodInfo DepertureSoundSetMethod;
        /// <summary>
        /// <see cref="DepertureTime"/> の <see cref="StoppageTime"/> 前の時刻に再生されるサウンドを取得・設定します。
        /// </summary>
        public Sound DepertureSound
        {
            get => Sound.FromSource(DepertureSoundGetMethod.Invoke(Src, null));
            set => DepertureSoundSetMethod.Invoke(Src, new object[] { value.Src });
        }

        private static MethodInfo ArrivalSoundGetMethod;
        private static MethodInfo ArrivalSoundSetMethod;
        /// <summary>
        /// ドアが開いたときに再生されるサウンドを取得・設定します。
        /// </summary>
        public Sound ArrivalSound
        {
            get => Sound.FromSource(ArrivalSoundGetMethod.Invoke(Src, null));
            set => ArrivalSoundSetMethod.Invoke(Src, new object[] { value.Src });
        }

        private static MethodInfo SignalFlagGetMethod;
        private static MethodInfo SignalFlagSetMethod;
        /// <summary>
        /// <see cref="DepertureTime"/> の <see cref="StoppageTime"/> 前の時刻まで出発信号が停止を現示するかどうかを取得・設定します。
        /// </summary>
        public bool SignalFlag
        {
            get => SignalFlagGetMethod.Invoke(Src, null);
            set => SignalFlagSetMethod.Invoke(Src, new object[] { value });
        }

        private static MethodInfo MarginMaxGetMethod;
        private static MethodInfo MarginMaxSetMethod;
        /// <summary>
        /// 停止位置誤差の前方許容範囲 [m] を取得・設定します。
        /// </summary>
        public double MarginMax
        {
            get => MarginMaxGetMethod.Invoke(Src, null);
            set => MarginMaxSetMethod.Invoke(Src, new object[] { value });
        }

        private static MethodInfo MarginMinGetMethod;
        private static MethodInfo MarginMinSetMethod;
        /// <summary>
        /// 停止位置誤差の後方許容範囲 [m] を負の値で取得・設定します。
        /// </summary>
        public double MarginMin
        {
            get => MarginMinGetMethod.Invoke(Src, null);
            set => MarginMinSetMethod.Invoke(Src, new object[] { value });
        }

        private static MethodInfo MinStopPositionGetMethod;
        /// <summary>
        /// 停止位置誤差の下限の距離程 [m] を取得します。
        /// </summary>
        public double MinStopPosition
        {
            get => MinStopPositionGetMethod.Invoke(Src, null);
        }

        private static MethodInfo MaxStopPositionGetMethod;
        /// <summary>
        /// 停止位置誤差の上限の距離程 [m] を取得します。
        /// </summary>
        public double MaxStopPosition
        {
            get => MaxStopPositionGetMethod.Invoke(Src, null);
        }

        private static MethodInfo AlightingTimeGetMethod;
        private static MethodInfo AlightingTimeSetMethod;
        /// <summary>
        /// 降車時間をミリ秒単位で取得・設定します。
        /// </summary>
        public int AlightingTimeMilliseconds
        {
            get => AlightingTimeGetMethod.Invoke(Src, null);
            set => AlightingTimeSetMethod.Invoke(Src, new object[] { value });
        }

        /// <summary>
        /// 降車時間を取得・設定します。
        /// </summary>
        public TimeSpan AlightingTime
        {
            get => TimeSpan.FromMilliseconds(AlightingTimeMilliseconds);
            set => AlightingTimeMilliseconds = (int)value.TotalMilliseconds;
        }

        private static MethodInfo TargetLoadFactorGetMethod;
        private static MethodInfo TargetLoadFactorSetMethod;
        /// <summary>
        /// 出発時の乗車率を取得・設定します。
        /// </summary>
        public double TargetLoadFactor
        {
            get => TargetLoadFactorGetMethod.Invoke(Src, null);
            set => TargetLoadFactorSetMethod.Invoke(Src, new object[] { value });
        }

        private static MethodInfo CurrentLoadFactorGetMethod;
        private static MethodInfo CurrentLoadFactorSetMethod;
        /// <summary>
        /// 到着時の乗車率を取得・設定します。
        /// </summary>
        public double CurrentLoadFactor
        {
            get => CurrentLoadFactorGetMethod.Invoke(Src, null);
            set => CurrentLoadFactorSetMethod.Invoke(Src, new object[] { value });
        }

        private static MethodInfo DoorReopenGetMethod;
        private static MethodInfo DoorReopenSetMethod;
        /// <summary>
        /// ドアが再開閉される確率を取得・設定します。
        /// </summary>
        public double DoorReopen
        {
            get => DoorReopenGetMethod.Invoke(Src, null);
            set => DoorReopenSetMethod.Invoke(Src, new object[] { value });
        }

        private static MethodInfo StuckInDoorGetMethod;
        private static MethodInfo StuckInDoorSetMethod;
        /// <summary>
        /// 旅客がドアに挟まる時間をミリ秒単位で取得・設定します。
        /// </summary>
        public int StuckInDoorMilliseconds
        {
            get => StuckInDoorGetMethod.Invoke(Src, null);
            set => StuckInDoorSetMethod.Invoke(Src, new object[] { value });
        }

        /// <summary>
        /// 旅客がドアに挟まる時間を取得・設定します。
        /// </summary>
        public TimeSpan StuckInDoor
        {

            get => TimeSpan.FromMilliseconds(StoppageTimeMilliseconds);
            set => StoppageTimeMilliseconds = (int)value.TotalMilliseconds;
        }
    }
}
