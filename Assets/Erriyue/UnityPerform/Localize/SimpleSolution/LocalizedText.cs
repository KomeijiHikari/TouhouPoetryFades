using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace RenaissanceRestart
{
    public class LocalizedText : MonoBehaviour
    {
        [SerializeField] private string textKey;

        private Text textComponent;
        private TextMeshPro TextMeshPro;
        private TextMeshProUGUI TextMeshProUGUI;
        public TextDecorater Decorater;

        private void Awake()
        {
            FlashComponent();
            this.UpdateFont();
            LocalizationManager.I.OnLanguageChanged.Subscribe((str) =>
            {
                this.UpdateFont();
                this.UpdateText();
            }).AddTo(this);
        }
        void Start()
        {
            UpdateText();
        }
        private void UpdateFont()
        {
            if (this.TextMeshPro != null)
            {
                this.TextMeshPro.font = LocalizationManager.I.GetCurrentFont();
            }
            if (this.TextMeshProUGUI != null)
            {
                this.TextMeshProUGUI.font = LocalizationManager.I.GetCurrentFont();
            }
        }

        private void FlashComponent()
        {
            if (textComponent == null)
                textComponent = GetComponent<Text>();
            if (TextMeshPro == null)
                TextMeshPro = GetComponent<TextMeshPro>();
            if (TextMeshProUGUI == null)
                TextMeshProUGUI = GetComponent<TextMeshProUGUI>();
        }
        public void UpdateText()
        {
            if (textComponent != null)
            {
                if (!string.IsNullOrEmpty(textKey))
                {
                    if (Decorater == null)
                        Decorater = new();
                    textComponent.text = Decorater.GetFinal(LocalizationManager.I.GetText(textKey));
                }
                else
                {
                    textComponent.text = "";
                }
            }
            if (TextMeshPro != null)
            {
                if (!string.IsNullOrEmpty(textKey))
                {
                    if (Decorater == null)
                        Decorater = new();
                    TextMeshPro.text = Decorater.GetFinal(LocalizationManager.I.GetText(textKey));
                }
                else
                {
                    TextMeshPro.text = "";
                }
            }
            if (TextMeshProUGUI != null)
            {
                if (!string.IsNullOrEmpty(textKey))
                {
                    if (Decorater == null)
                        Decorater = new();
                    TextMeshProUGUI.text = Decorater.GetFinal(LocalizationManager.I.GetText(textKey));
                }
                else
                {
                    TextMeshProUGUI.text = "";
                }
            }
        }

        // 动态改变文本键
        public void SetKey(string newKey, TextDecorater decorater)
        {
            this.Decorater = decorater;
            textKey = newKey;
            FlashComponent();
            UpdateText();
        }


    }
}
