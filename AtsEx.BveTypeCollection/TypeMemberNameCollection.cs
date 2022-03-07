﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automatic9045.AtsEx.BveTypeCollection
{
    internal struct TypeMemberNameCollection
    {
        public string WrapperTypeName { get; }
        public string OriginalTypeName { get; }
        public List<PropertyAccessorInfo> PropertyGetters { get; }
        public List<PropertyAccessorInfo> PropertySetters { get; }
        public List<FieldInfo> Fields { get; }
        public List<MethodInfo> Methods { get; }

        public TypeMemberNameCollection(string wrapperTypeName, string originalTypeName)
        {
            WrapperTypeName = wrapperTypeName;
            OriginalTypeName = originalTypeName;

            PropertyGetters = new List<PropertyAccessorInfo>();
            PropertySetters = new List<PropertyAccessorInfo>();
            Fields = new List<FieldInfo>();
            Methods = new List<MethodInfo>();
        }

        public override string ToString() => WrapperTypeName;


        public class MemberInfo : IComparable<MemberInfo>
        {
            public string WrapperName { get; }
            public string OriginalName { get; }

            public bool IsOriginalPrivate { get; }
            public bool IsOriginalStatic { get; }

            public bool IsWrapperPrivate { get; }
            public bool IsWrapperStatic { get; }
            public string StaticWrapperContainer { get; }

            protected MemberInfo(string wrapperName, string originalName, bool isOriginalStatic = false, bool isOriginalPrivate = false, bool isWrapperStatic = false, bool isWrapperPrivate = false, string staticWrapperContainer = null)
            {
                WrapperName = wrapperName;
                OriginalName = originalName;

                IsOriginalStatic = isOriginalStatic;
                IsOriginalPrivate = isOriginalPrivate;

                IsWrapperStatic = isWrapperStatic;
                IsWrapperPrivate = isWrapperPrivate;
                StaticWrapperContainer = staticWrapperContainer;
            }

            public int CompareTo(MemberInfo other) => WrapperName.CompareTo(other.WrapperName);
        }

        public class PropertyAccessorInfo : MemberInfo
        {
            public PropertyAccessorInfo(string wrapperName, string originalName, bool isOriginalStatic = false, bool isOriginalPrivate = false, bool isWrapperStatic = false, bool isWrapperPrivate = false, string staticWrapperContainer = null)
                : base(wrapperName, originalName, isOriginalStatic, isOriginalPrivate, isWrapperStatic, isWrapperPrivate, staticWrapperContainer)
            {
            }

            public override string ToString() => $"Property: {WrapperName}";
        }

        public class FieldInfo : MemberInfo
        {
            public FieldInfo(string wrapperPropertyName, string originalName, bool isOriginalStatic = false, bool isOriginalPrivate = false, bool isWrapperStatic = false, bool isWrapperPrivate = false, string staticWrapperContainer = null)
                : base(wrapperPropertyName, originalName, isOriginalStatic, isOriginalPrivate, isWrapperStatic, isWrapperPrivate, staticWrapperContainer)
            {
            }

            public override string ToString() => $"Field: {WrapperName}";
        }

        public class MethodInfo : MemberInfo
        {
            public IEnumerable<TypeInfoBase> WrapperParamNames { get; set; }

            public MethodInfo(string wrapperName, string originalName, bool isOriginalStatic = false, bool isOriginalPrivate = false, bool isWrapperStatic = false, bool isWrapperPrivate = false, string staticWrapperContainer = null)
                : this(wrapperName, originalName, null, isOriginalStatic, isOriginalPrivate, isWrapperStatic, isWrapperPrivate, staticWrapperContainer)
            {
            }

            public MethodInfo(string wrapperName, string originalName, IEnumerable<TypeInfoBase> wrapperParamNames, bool isOriginalStatic = false, bool isOriginalPrivate = false, bool isWrapperStatic = false, bool isWrapperPrivate = false, string staticWrapperContainer = null)
                : base(wrapperName, originalName, isOriginalStatic, isOriginalPrivate, isWrapperStatic, isWrapperPrivate, staticWrapperContainer)
            {
                WrapperParamNames = wrapperParamNames;
            }

            public override string ToString() => $"Method: {WrapperName}";
        }

        public abstract class TypeInfoBase
        {
            public abstract override string ToString();
        }

        public class TypeInfo : TypeInfoBase
        {
            public string TypeName { get; }

            public TypeInfo(string typeName)
            {
                TypeName = typeName;
            }

            public override string ToString() => TypeName;
        }

        public class GenericTypeInfo : TypeInfo
        {
            public IEnumerable<TypeInfoBase> TypeParams { get; }

            public GenericTypeInfo(string typeName, IEnumerable<TypeInfoBase> typeParams) : base(typeName)
            {
                TypeParams = typeParams;
            }

            public override string ToString() => $"{TypeName}`{TypeParams.Count()}[{string.Join(",", TypeParams.Select(p => p.ToString()))}]";
        }

        public class ArrayTypeInfo : TypeInfoBase
        {
            public TypeInfoBase BaseType { get; }
            public int DimensionCount { get; }

            public ArrayTypeInfo(TypeInfoBase baseType, int dimensionCount)
            {
                BaseType = baseType;
                DimensionCount = dimensionCount;
            }

            public override string ToString() => $"{BaseType}[{new string(',', DimensionCount - 1)}]";
        }
    }
}
