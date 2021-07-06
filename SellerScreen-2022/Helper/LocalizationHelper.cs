using SellerScreen_2022.Data;
using SellerScreen_2022.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using Windows.Storage;

namespace SellerScreen_2022.Helper
{
    public class LocalizationHelper
    {
        private static CultureInfo systemUICulture;

        private static readonly Dictionary<Enum, string> EnumRedirectionMap = new()
        {
            { ModernWpf.ElementTheme.Default, "SystemDefault" }
        };

        #region Properties

        private static LanguageInfo _currentLanguage;

        public static LanguageInfo CurrentLanguage
        {
            get => _currentLanguage;
            set
            {
                if (_currentLanguage != value)
                {
                    _currentLanguage = value;
                    Properties.Settings.Default.Language = _currentLanguage.LanguageName;
                    Properties.Settings.Default.Save();
                    OnStaticPropertyChanged();
                }
            }
        }

        public static ObservableCollection<LanguageInfo> SupportedLanguages { get; private set; }

        #endregion

        public static void Initialize()
        {
            systemUICulture = CultureInfo.CurrentCulture;
            SupportedLanguages = GetAllSupportedLanguages();
            string language = Properties.Settings.Default.Language;
            CurrentLanguage = SupportedLanguages.First(x => x.LanguageName == language);

            Thread.CurrentThread.CurrentUICulture = CurrentLanguage.CultureInfo ?? systemUICulture;
        }

        private static ObservableCollection<LanguageInfo> GetAllSupportedLanguages()
        {
            ObservableCollection<LanguageInfo> supportedLanguages = new()
            {
                new LanguageInfo(string.Empty)
            };

            System.Resources.ResourceManager resourceManager = Properties.Strings.Lang.ResourceManager;
            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.NeutralCultures);
            foreach (CultureInfo culture in cultures)
            {
                try
                {
                    if (culture.Equals(CultureInfo.InvariantCulture))
                    {
                        continue;
                    }

                    System.Resources.ResourceSet resourceSet = resourceManager.GetResourceSet(culture, true, false);
                    if (resourceSet != null)
                    {
                        supportedLanguages.Add(new LanguageInfo(culture));
                    }
                }
                catch (CultureNotFoundException) { }
            }

            return supportedLanguages;
        }

        public static string GetLocalisedEnumValue(Enum value)
        {
            string resourceId = $"Enums.{value.GetType().Name}.{value}";

            if (EnumRedirectionMap.TryGetValue(value, out string result))
            {
                resourceId = result;
            }

            Debug.WriteLine(resourceId);
            try
            {
                return Properties.Strings.Lang.ResourceManager.GetString(resourceId, Properties.Strings.Lang.Culture);
            }
            catch
            {
                return value.ToString();
            }
        }

        public static event PropertyChangedEventHandler StaticPropertyChanged;

        private static void OnStaticPropertyChanged([CallerMemberName] string propertyName = "")
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));

            if (CurrentLanguage.CultureInfo != null && Thread.CurrentThread.CurrentCulture.Name != CurrentLanguage.CultureInfo.Name)
            {
                DispatcherHelper.RunOnMainThread(() =>
                {
                    MainWindow w = (MainWindow)Application.Current.MainWindow;
                    if (w.IsLoaded)
                    {
                        w.RestartRequired = true;
                    }
                });
            }
        }
    }

    public class LanguageInfo
    {
        public LanguageInfo(CultureInfo cultureInfo)
        {
            CultureInfo = cultureInfo;
            Initialize();
        }

        public LanguageInfo(string cultureName)
        {
            if (!string.IsNullOrEmpty(cultureName))
            {
                CultureInfo = new CultureInfo(cultureName);
            }
            Initialize();
        }

        public CultureInfo CultureInfo { get; private set; }

        public string DisplayName { get; private set; }

        public string LanguageName { get; private set; }

        private void Initialize()
        {
            if (CultureInfo != null)
            {
                DisplayName = CultureInfo.NativeName;
                LanguageName = CultureInfo.Name;
            }
            else
            {
                DisplayName = Properties.Strings.Lang.SystemDefault;
                LanguageName = string.Empty;
            }
        }

        public static bool TryParse(string value, out LanguageInfo result)
        {
            result = new(value);
            return true;
        }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
