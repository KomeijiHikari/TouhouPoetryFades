namespace RenaissanceRestart
{
    [System.Serializable]
    public class TextDecorater
    {
        public string Before = "";
        public string After = "";
        public string[] Params = null;
        public bool useColor = true;

        public string GetFinal(string raw)
        {
            if(Params != null)
            {
                for (int i = 0; i < Params.Length; i++)
                {
                    if(useColor)
                        raw = raw.Replace($"{{{i}}}", $"<color=#FF843C>{Params[i]}</color>");
                    else
                        raw = raw.Replace($"{{{i}}}", $"{Params[i]}");
                }
            }
            return Before + raw + After;
        }
    }
}
