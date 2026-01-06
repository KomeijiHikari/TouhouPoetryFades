using TMPro;

namespace RenaissanceRestart
{
    public interface ILocalizationManager
    {
        public TMP_FontAsset GetCurrentFont();
        public string GetText(string key);

    }
}

