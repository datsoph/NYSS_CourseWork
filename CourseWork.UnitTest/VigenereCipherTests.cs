using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CourseWork.UnitTest
{
    [TestClass]
    public class VigenereCipherTests
    {
        [TestMethod]
        public void RuAlphabet_ValueIsCorrect()
        {
            string result = VigenereCipher.RuAlphabet.ToLower();
            Assert.AreEqual(result, "абвгдеёжзийклмнопрстуфхцчшщъыьэюя");
        }

        [TestMethod]
        public void Encrypt_NoRussianLetters_NoChanges()
        {
            string text = "Hello World!";
            string key = "ключ";
            string result = VigenereCipher.Encrypt(text, key);
            Assert.AreEqual(result, text);
        }

        [TestMethod]
        public void Encrypt_OnlyRussianLetters_IsEncrypted()
        {
            string text = "интервьюер интервента интервьюировал";
            string key = "история";
            string result = VigenereCipher.Encrypt(text, key);
            Assert.AreEqual(result, "сяеубкыжцг чюыдщучьги зцдчятеэсвбррф");
        }

        [TestMethod]
        public void Encrypt_RussianLettersAndOtherSymbols_OnlyRussianLettersAreEncrypted()
        {
            string text = "Привет, мир!";
            string key = "кларнет";
            string result = VigenereCipher.Encrypt(text, key);
            Assert.AreEqual(result, "Ъьиттч, яуь!");
        }

        [TestMethod]
        public void Decrypt_NoRussianLetters_NoChanges()
        {
            string text = "Hello World!";
            string key = "ключ";
            string result = VigenereCipher.Decrypt(text, key);
            Assert.AreEqual(result, text);
        }

        [TestMethod]
        public void Decrypt_OnlyRussianLetters_IsChanged()
        {
            string text = "сяеубкыжцг чюыдщучьги зцдчятеэсвбррф";
            string key = "история";
            string result = VigenereCipher.Decrypt(text, key);
            Assert.AreEqual(result, "интервьюер интервента интервьюировал");
        }

        [TestMethod]
        public void Decrypt_RussianLettersAndOtherSymbols_OnlyRussianLettersAreChanged()
        {
            string text = "Ъьиттч, яуь!";
            string key = "кларнет";
            string result = VigenereCipher.Decrypt(text, key);
            Assert.AreEqual(result, "Привет, мир!");
        }
    }
}
