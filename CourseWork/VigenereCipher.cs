using System.Linq;

namespace CourseWork
{
    public static class VigenereCipher
    {
        public static string RuAlphabet { get; private set; } = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";

        static char EncryptSymbol(char symbol, char key) => RuAlphabet[(RuAlphabet.Length + RuAlphabet.IndexOf(symbol) + RuAlphabet.IndexOf(key)) % RuAlphabet.Length];
        static char DecryptSymbol(char symbol, char key) => RuAlphabet[(RuAlphabet.Length + RuAlphabet.IndexOf(symbol) - RuAlphabet.IndexOf(key)) % RuAlphabet.Length];

        public static string Encrypt(string text, string key)
        {
            return Crypt(text, key, true);
        }
        public static string Decrypt(string text, string key)
        {
            return Crypt(text, key, false);
        }

        public static string Crypt(string text, string key, bool encrypt)
        {
            char[] chars = text.ToLower().ToCharArray();
            key = key.ToLower();
            int keyIndex = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (!RuAlphabet.Contains(chars[i]))
                    continue;
                if (encrypt)
                    chars[i] = EncryptSymbol(chars[i], key[keyIndex++]);
                else
                    chars[i] = DecryptSymbol(chars[i], key[keyIndex++]);
                keyIndex %= key.Length;
            }
            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]))
                    chars[i] = char.ToUpper(chars[i]);
            }
            return new string(chars);
        }
    }
}
