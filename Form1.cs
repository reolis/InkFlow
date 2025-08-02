using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InkFlow
{
    public partial class Form1 : Form
    {
        public string currentFilePath = Path.Combine(Application.StartupPath, "autoSave.txt");
        ThemesManager themesManager = new ThemesManager();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            saverTimer.Interval = 5 * 60 * 1000;
            saverTimer.Enabled = true;
            saverTimer.Start();
        }

        private void saverTimer_Tick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentFilePath))
            {
                File.WriteAllText(currentFilePath, richTextBox1.Text);
            }
        }

        private void UpdateStats()
        {
            int wordsCount = richTextBox1.Text.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Length;
            int charsCount = richTextBox1.Text.Length;

            label1.Text = $"Words: {wordsCount}";
            label2.Text = $"Symbols: {charsCount}";
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            UpdateStats();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Text Files|*.txt";

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                currentFilePath = openDialog.FileName;
                richTextBox1.Text = File.ReadAllText(currentFilePath);
            }
        }

        private void lightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            themesManager.CurrentTheme = Theme.Light;
            themesManager.ApplyTheme(this, Theme.Light);
        }

        private void darkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            themesManager.CurrentTheme = Theme.Dark;
            themesManager.ApplyTheme(this, Theme.Dark);
        }

        private void tXTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs("Text Files|*.txt", ".txt");
        }

        private void dOCXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs("Word Documents|*.docx", ".docx");
        }

        private void pDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs("PDF Files|*.pdf", ".pdf");
        }

        private void SaveAs(string filter, string extension)
        {
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Filter = filter,
                DefaultExt = extension
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                string path = saveDialog.FileName;

                if (path != Path.Combine(Application.StartupPath, "autoSave.txt"))
                {
                    currentFilePath = path;

                    if (extension == ".txt")
                    {
                        File.WriteAllText(path, richTextBox1.Text);
                    }
                    else if (extension == ".docx")
                    {
                        string[] paragraphs = richTextBox1.Text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
                        foreach (var parph in paragraphs)
                        {
                            Saver.SaveAsDocx(path, parph);
                        }
                    }
                    else if (extension == ".pdf")
                    {
                        Saver.SaveAsPdf(path, richTextBox1.Text);
                    }
                }
            }
        }

    }
}
