using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;

namespace SellerScreen_2022.Languages
{
    public class LanguageResources : ResourceDictionary
    {
        private readonly Languages l = new();
        private readonly Languages langs = new();
        public LanguageResources()
        {
            ApplyLanguage();
        }

        private string _langKey = Thread.CurrentThread.CurrentCulture.Parent.ToString();
        public string LangKey
        {
            get => _langKey;
            set
            {
                if (_langKey != value)
                {
                    _langKey = value;
                    ApplyLanguage();
                }
            }
        }

        private void ApplyLanguage()
        {
            DispatcherHelper.RunOnMainThread(() =>
            {
                if (l.Exists(x => x.Name == LangKey))
                {
                    if (MergedDictionaries.Count > 0)
                    {
                        MergedDictionaries.Clear();
                    }

                    ResourceDictionary dict = new()
                    {
                        Source = langs.Find(x => x.Name == LangKey).Path
                    };
                    MergedDictionaries.Add(dict);
                }
                else
                {
                    if (MergedDictionaries.Count > 0)
                    {
                        MergedDictionaries.Clear();
                    }

                    ResourceDictionary dict = new()
                    {
                        Source = langs.Find(x => x.Name == "English").Path
                    };
                    MergedDictionaries.Add(dict);
                }
            });
        }
    }

    public class Languages : List<Language>
    {
        public Languages()
        {
            Add("System", null);
            Add("Deutsch", "de");
            Add("English", "en");
        }

        private void Add(string name, string key)
        {
            Add(new Language(name, key, new Uri($"..\\Languages\\{key}.xaml", UriKind.Relative)));
        }
    }

    public class Language
    {
        public Language(string name, string key, Uri path)
        {
            Name = name;
            Key = key;
            Path = path;
        }

        public string Name { get; }
        public string Key { get; }
        public Uri Path { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}