using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace redactor
{
    public partial class Form1 : Form
    {
        private string[] csharp = { "using", "namespace", "class", "public", "private", "static", "void", "int", "string" };
        private string[] java = { "import", "package", "class", "public", "private", "static", "void", "int", "String" };
        private string[] pascal = { "unit", "interface", "implementation", "uses", "type", "class", "procedure", "function", "var", "begin", "end" };
        public Form1()
        {
            InitializeComponent();
            saveFileDialog1.Filter = "Text File(*.txt)|*.txt";
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string fn = saveFileDialog1.FileName;
            File.WriteAllText(fn,fastColoredTextBox1.Text);
            MessageBox.Show("Файл успешно сохранен!");

        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string fn = openFileDialog1.FileName;
            string tf = File.ReadAllText(fn);
            fastColoredTextBox1.Text = tf;
            MessageBox.Show("Файл открыт!");
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(fastColoredTextBox1.TextLength > 0)
            {
                fastColoredTextBox1.Copy();
            }   
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (fastColoredTextBox1.TextLength > 0)
            {
                fastColoredTextBox1.Paste();
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            HighlightSyntax();
        }

        private void жирныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
            fastColoredTextBox1.Font = fontDialog1.Font;
        }

        private void HighlightSyntax()
        {   
            string code = fastColoredTextBox1.Text;

            // Сброс цвета текста
            fastColoredTextBox1.SelectionLength = code.Length;
            fastColoredTextBox1.SelectionColor = SystemColors.WindowText;

            HighlightKeywords(csharp, Color.Blue);
            HighlightKeywords(java, Color.DarkGreen);
            HighlightKeywords(pascal, Color.DarkRed);
        }

        private void HighlightKeywords(string[] keywords, Color color)
        {
            foreach (string keyword in keywords)
            {
                int index = 0;
                while (index < fastColoredTextBox1.Text.Length && (index = fastColoredTextBox1.Text.IndexOf(keyword, index, StringComparison.OrdinalIgnoreCase)) != -1)
                {
                    fastColoredTextBox1.SelectionStart = index;
                    fastColoredTextBox1.SelectionLength = keyword.Length;
                    fastColoredTextBox1.SelectionColor = color; 
                    index += keyword.Length;
                }
            }
        }

        private void btnFindReplace_Click(object sender, EventArgs e)
        {
            string searchText = textBoxSearch.Text;

            if (searchText.Length == 0)
            {
                MessageBox.Show("Введите текст для поиска", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            StringComparison comparison = StringComparison.OrdinalIgnoreCase; // Устанавливаем игнорирование регистра

            int index = fastColoredTextBox1.Text.IndexOf(searchText, comparison);

            if (index != -1)
            {
                fastColoredTextBox1.SelectionStart = index;
                fastColoredTextBox1.SelectionLength = searchText.Length;
                fastColoredTextBox1.Focus(); // Устанавливаем фокус на FastColoredTextBox, чтобы выделение было видимым
            }
            else
            {
                MessageBox.Show("Текст не найден", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void replacebtn_Click(object sender, EventArgs e)
        {
            string searchText = textBoxSearch.Text;
            string replaceText = textBoxReplace.Text;

            if (searchText.Length == 0)
            {
                MessageBox.Show("Введите текст для поиска", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            StringComparison comparison = StringComparison.OrdinalIgnoreCase; // Устанавливаем игнорирование регистра

            int index = fastColoredTextBox1.Text.IndexOf(searchText, comparison);

            if (index != -1)
            {
                fastColoredTextBox1.SelectionStart = index;
                fastColoredTextBox1.SelectionLength = searchText.Length;
                fastColoredTextBox1.Focus(); // Устанавливаем фокус на FastColoredTextBox, чтобы выделение было видимым

                if (!string.IsNullOrEmpty(replaceText))
                {
                    // Если есть текст для замены, заменяем выделенный текст
                    fastColoredTextBox1.SelectedText = replaceText;
                }
            }
            else
            {
                MessageBox.Show("Текст не найден", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void fastColoredTextBox1_Load(object sender, EventArgs e)
        {

        }
    }
}
