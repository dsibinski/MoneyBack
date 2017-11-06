using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Text;
using MvvmCross.Localization;

namespace MoneyBack.Core.Utility
{
    public class ResxTextProvider : IMvxTextProvider
    {
        private readonly ResourceManager _resourceManager;

        public ResxTextProvider(ResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
            CurrentLanguage = new CultureInfo("en-US");
        }

        public CultureInfo CurrentLanguage { get; set; }

        public string GetText(string namespaceKey,
            string typeKey, string name)
        {
            string resolvedKey = name;

            if (!string.IsNullOrEmpty(typeKey))
            {
                resolvedKey = $"{typeKey}.{resolvedKey}";
            }

            if (!string.IsNullOrEmpty(namespaceKey))
            {
                resolvedKey = $"{namespaceKey}.{resolvedKey}";
            }

            return _resourceManager.GetString(resolvedKey, CurrentLanguage);
        }

        public string GetText(string namespaceKey, string typeKey, string name, params object[] formatArgs)
        {
            string baseText = GetText(namespaceKey, typeKey, name);

            if (string.IsNullOrEmpty(baseText))
            {
                return baseText;
            }

            return string.Format(baseText, formatArgs);
        }

        public bool TryGetText(out string textValue, string namespaceKey, string typeKey, string name)
        {
            try
            {
                textValue = GetText(namespaceKey, typeKey, name);
            }
            catch (Exception e)
            {
                textValue = null;
                Debug.Write(e);
                return false;
            }

            return true;
        }

        public bool TryGetText(out string textValue, string namespaceKey, string typeKey, string name, params object[] formatArgs)
        {
            try
            {
                textValue = GetText(namespaceKey, typeKey, name, formatArgs);
            }
            catch (Exception e)
            {
                textValue = null;
                Debug.Write(e);
                return false;
            }

            return true;
        }
    }
}
