using System.Collections.Generic;
using System.IO;
using TMPro;
using UniRx;
using UnityEngine;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace RenaissanceRestart
{
    public class LocalizationManager : DCLSingletonBase<LocalizationManager>, ILocalizationManager
    {
        private Dictionary<string, Dictionary<string, string>> localizationData;
        private string currentLanguage = "zh";

        public ReactiveCommand<string> OnLanguageChanged;

        public override void OnAwake()
        {
            if (I != this)
                return;
            OnLanguageChanged = new();
            LoadLocalizationFiles();
        }

        public TMP_FontAsset GetCurrentFont()
        {
            throw new System.Exception("请根据 currentLanguage 返回对应字体, 实现此方法!");
            //return GameRes.I.GetFont(this.currentLanguage);
        }

        private void LoadLocalizationFiles()
        {
            localizationData = new Dictionary<string, Dictionary<string, string>>();

            string[] languageCodes = { "zh", "jp", "en" };

            foreach (string lang in languageCodes)
            {
                string filePath = Path.Combine(Application.streamingAssetsPath, $"Localization_{lang}.yaml");

                if (File.Exists(filePath))
                {
                    string yamlContent = File.ReadAllText(filePath);
                    var deserializer = new DeserializerBuilder()
                        .WithNamingConvention(CamelCaseNamingConvention.Instance)
                        .Build();

                    LocalizationData data = deserializer.Deserialize<LocalizationData>(yamlContent);
                    localizationData[lang] = data.translations;

                    Debug.Log($"已加载 {data.translations.Count} 条翻译内容，语言: {lang}");
                }
                else
                {
                    Debug.LogWarning($"本地化文件未找到: {filePath}");
                }
            }

            // 设置默认语言
            SetLanguage(GetSystemLanguage());
        }


        /// <summary>
        /// 请使用 StaticBind 来绑定本地化!
        /// </summary>
        /// <param name="key"></param>
        /// <param name="before"></param>
        /// <param name="append"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public string GetText(string key)
        {
            if (localizationData.ContainsKey(currentLanguage) &&
                localizationData[currentLanguage].ContainsKey(key))
            {
                return localizationData[currentLanguage][key];
            }

            Debug.LogError($"翻译键未找到: {key}，语言: {currentLanguage}");
            return key;
        }
        public void StaticBindClear<T>(T text) where T : Component
        {
            var obj = text.gameObject;
            var c = obj.GetComponent<LocalizedText>();
            if (c == null)
            {
                c = obj.AddComponent<LocalizedText>();
            }
            c.SetKey("", null);
        }
        public void StaticBind<T>(T text, string static_key, TextDecorater decorater = null) where T : Component
        {
            var obj = text.gameObject;
            var c = obj.GetComponent<LocalizedText>();
            if (c == null)
            {
                c = obj.AddComponent<LocalizedText>();
            }
            c.SetKey(static_key, decorater);
        }


        // 检查键是否存在
        public bool HasKey(string key)
        {
            return localizationData.ContainsKey(currentLanguage) &&
                   localizationData[currentLanguage].ContainsKey(key);
        }


        // 获取当前语言
        public string GetCurrentLanguage()
        {
            return currentLanguage;
        }
        // 设置当前语言
        public void SetLanguage(string languageCode)
        {
            if (localizationData != null && localizationData.ContainsKey(languageCode))
            {
                currentLanguage = languageCode;
                PlayerPrefs.SetString("SelectedLanguage", languageCode);
                OnLanguageChanged?.Execute(languageCode);

                Debug.Log($"语言选择至: {languageCode}");
            }
            else
            {
                Debug.LogWarning($"Language not available: {languageCode}");
            }
        }
        // 获取系统语言
        private string GetSystemLanguage()
        {
            // 从PlayerPrefs读取用户选择的语言
            string savedLanguage = PlayerPrefs.GetString("SelectedLanguage", "");
            if (!string.IsNullOrEmpty(savedLanguage) && localizationData.ContainsKey(savedLanguage))
            {
                return savedLanguage;
            }

            // 根据系统语言自动选择
            SystemLanguage systemLang = Application.systemLanguage;
            switch (systemLang)
            {
                case SystemLanguage.Chinese:
                case SystemLanguage.ChineseSimplified:
                case SystemLanguage.ChineseTraditional:
                    return "zh";
                case SystemLanguage.Japanese:
                    return "jp";
                default:
                    return "en";
            }
        }
        // 获取支持的语言列表
        public List<string> GetAvailableLanguages()
        {
            return localizationData != null ? new List<string>(localizationData.Keys) : new List<string>() { "none" };
        }
    }
}

