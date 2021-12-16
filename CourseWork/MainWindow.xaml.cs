using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Word = Microsoft.Office.Interop.Word;

namespace CourseWork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private bool CheckAlphabet()
        {
            foreach (char c in KeyBox.Text)
                if (!VigenereCipher.RuAlphabet.Contains(c))
                    return false;
            return true;
        }

        private bool Check()
        {
            if (string.IsNullOrWhiteSpace(InputBox.Text) && string.IsNullOrWhiteSpace(KeyBox.Text))
            {
                MessageBox.Show("Введите текст и ключ для шифрования/дешифрования.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(InputBox.Text))
            {
                MessageBox.Show("Введите текст для шифрования/дешифрования.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(KeyBox.Text))
            {
                MessageBox.Show("Введите ключ для шифрования/дешифрования.");
                return false;
            }
            if (!CheckAlphabet())
            {
                MessageBox.Show("Ключ должен состоять только из букв русского алфавита.");
                return false;
            }

            return true;
        }

        private void Encrypt(object sender, RoutedEventArgs e)
        {
            OutputBox.Text = "";
            if (Check())
                OutputBox.Text = VigenereCipher.Encrypt(InputBox.Text, KeyBox.Text);
        }

        private void Decrypt(object sender, RoutedEventArgs e)
        {
            OutputBox.Text = "";
            if (Check())
                OutputBox.Text = VigenereCipher.Decrypt(InputBox.Text, KeyBox.Text);
        }

        private void ReadFile(object sender, RoutedEventArgs e)
        {
            InputBox.Text = "";
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Text & Word documents|*.txt; *.docx";
            if (fileDialog.ShowDialog() == true)
            {
                if (fileDialog.FileName.EndsWith(".txt"))
                    InputBox.Text = File.ReadAllText(fileDialog.FileName);
                else if (fileDialog.FileName.EndsWith(".docx"))
                {
                    Word.Application word = new Word.Application();
                    object fileName = fileDialog.FileName;
                    Word.Document doc = word.Documents.Open(ref fileName);
                    InputBox.Text = doc.Content.Text;
                    doc.Close();
                    word.Quit();
                }
            }
        }

        private void WriteFile(object sender, RoutedEventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "Text & Word documents|*.txt; *.docx";
            if (fileDialog.ShowDialog() == true)
            {
                if (fileDialog.FileName.EndsWith(".txt"))
                    File.WriteAllText(fileDialog.FileName, OutputBox.Text);
                else if (fileDialog.FileName.EndsWith(".docx"))
                {
                    Word.Application word = new Word.Application();
                    Word.Document doc = word.Documents.Add();
                    doc.Content.Text = OutputBox.Text;
                    doc.SaveAs2(fileDialog.FileName);
                    doc.Close();
                    word.Quit();
                }
            }
        }
    }
}
