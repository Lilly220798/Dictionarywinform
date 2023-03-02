using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Dictionarywinform
{
    public partial class Form1 : Form
    {

        string word = string.Empty;
        string languageFrom = string.Empty;
        string languageTo = string.Empty;
        string translatedWord = string.Empty;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/dictionary/");


            // Translate from English to French
            HttpResponseMessage response1 = client.GetAsync($"{word}/{languageFrom}/{languageTo}").Result;
            if (response1.IsSuccessStatusCode)
            {
                translatedWord = response1.Content.ReadAsStringAsync().Result;
                textBox2.Text = $"{translatedWord}";
            }

            
        }

        private void comboBoxFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCode = comboBoxFrom.SelectedValue.ToString();
            languageFrom = selectedCode; 
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var fromTranslationOptions = new List<TranslationKey>
            {
                new TranslationKey() { Language = "English", keylang = "en" },
                new TranslationKey() { Language = "French", keylang = "fr" },
                new TranslationKey() { Language = "German", keylang = "de" },
                new TranslationKey() { Language = "Romanian", keylang = "ro" }
            };

            var toTranslationOptions = new List<TranslationKey>
            {
                new TranslationKey() { Language = "English", keylang = "en" },
                new TranslationKey() { Language = "French", keylang = "fr" },
                new TranslationKey() { Language = "German", keylang = "de" },
                new TranslationKey() { Language = "Romanian", keylang = "ro" }
            };

            //binding data to comboboxfrom
            comboBoxFrom.DisplayMember = "Language";
            comboBoxFrom.ValueMember = "keylang";
            comboBoxFrom.DataSource = fromTranslationOptions;

            //binding data to comboboxto
            comboBoxTo.DisplayMember = "Language";
            comboBoxTo.ValueMember = "keylang";
            comboBoxTo.DataSource = toTranslationOptions;

        }

        public class TranslationKey
        {
            public string Language { get; set; }
            public string keylang { get; set; }
        }

        private void comboBoxTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCode = comboBoxTo.SelectedValue.ToString();
            languageTo = selectedCode;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            word = textBox1.Text;
        }
    }
}


