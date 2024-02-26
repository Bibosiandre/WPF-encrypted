using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace BitEncryptionApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {
            string text = txtPlainText.Text;
            string key = txtKey.Text;

            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(key))
            {
                MessageBox.Show("Please enter text and key.");
                return;
            }

            string encryptedText = EncryptString(text, key);
            txtEncryptedText.Text = encryptedText;
        }

        private void Decrypt_Click(object sender, RoutedEventArgs e)
        {
            string encryptedText = txtEncryptedTextDecrypt.Text;
            string key = txtKeyDecrypt.Text;

            if (string.IsNullOrEmpty(encryptedText) || string.IsNullOrEmpty(key))
            {
                MessageBox.Show("Please enter encrypted text and key.");
                return;
            }

            string decryptedText = DecryptString(encryptedText, key);
            txtDecryptedText.Text = decryptedText;
        }

        private string EncryptString(string text, string key)
        {
            byte[] textBytes = Encoding.UTF8.GetBytes(text);
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] encryptedBytes = new byte[textBytes.Length];

            for (int i = 0; i < textBytes.Length; i++)
            {
                encryptedBytes[i] = (byte)(textBytes[i] ^ keyBytes[i % keyBytes.Length]);
            }

            return Convert.ToBase64String(encryptedBytes);
        }

        private string DecryptString(string encryptedText, string key)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] decryptedBytes = new byte[encryptedBytes.Length];

            for (int i = 0; i < encryptedBytes.Length; i++)
            {
                decryptedBytes[i] = (byte)(encryptedBytes[i] ^ keyBytes[i % keyBytes.Length]);
            }


            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }
}
