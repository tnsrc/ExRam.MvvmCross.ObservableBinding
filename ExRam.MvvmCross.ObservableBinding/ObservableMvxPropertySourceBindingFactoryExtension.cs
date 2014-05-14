﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cirrious.MvvmCross.Binding.Bindings.Source;
using Cirrious.MvvmCross.Binding.Bindings.Source.Construction;
using Cirrious.MvvmCross.Binding.Parse.PropertyPath.PropertyTokens;

namespace ExRam.MvvmCross.ObservableBinding
{
    public class ObservableMvxPropertySourceBindingFactoryExtension : IMvxSourceBindingFactoryExtension
    {
        public bool TryCreateBinding(object source, MvxPropertyToken currentToken,
                                     List<MvxPropertyToken> remainingTokens, out IMvxSourceBinding result)
        {
            if (source == null)
            {
                result = null;
                return false;
            }

            var propertyNameToken = currentToken as MvxPropertyNamePropertyToken;
            if (propertyNameToken == null)
            {
                result = null;
                return false;
            }

            var propertyInfo = FindPropertyInfo(source, propertyNameToken.PropertyName);

            if (propertyInfo == null)
            {
                result = null;
                return false;
            }

            var value = propertyInfo.GetValue(source, new object[0]) as IObservable<object>;
            if (value == null)
            {
                result = null;
                return false;
            }

            var propertyTypeInfo = propertyInfo.PropertyType.GetTypeInfo();
            var elementType = (((propertyTypeInfo.IsGenericType) && (propertyTypeInfo.GetGenericTypeDefinition() == typeof(IObservable<>))) ? (propertyTypeInfo.GenericTypeArguments[0]) : (typeof(object)));

            result = new ObservableMvxSourceBinding(value, elementType, remainingTokens);
            return true;
        }

        protected PropertyInfo FindPropertyInfo(object source, string name)
        {
            var propertyInfo = source.GetType()
                .GetRuntimeProperty(name);

            return propertyInfo;
        }
    }
}