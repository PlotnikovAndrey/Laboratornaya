using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace Lab1ISIS
{
    public partial class Child : Form
    {
        public Child()
        {
            InitializeComponent();
        }
        string fileload = "";
        string fileload1 = "";
        public string SaveStroki;
        public string SaveStroki1;
        public string SaveStroki2;

        private void button3_Click(object sender, EventArgs e)
        {
            int s1 = 0;
            int s2 = 0;
            int numberOfLines1 = richTextBox1.Lines.Length;
            int numberOfLines2 = richTextBox2.Lines.Length;
            string stroki = "";
            string stroki1 = "";
            string stroki2 = "";
            if (numberOfLines1 == numberOfLines2)
            {
                s1 = numberOfLines1; s2 = 0;
            }
            else
            if (numberOfLines1 < numberOfLines2)
            {
                s1 = numberOfLines1; s2 = numberOfLines2;
            }
            else
            {
                s1 = numberOfLines2; s2 = numberOfLines1;
            }
            for (int i = 0; i < s1; i++)
                if (Equals(richTextBox1.Lines[i], richTextBox2.Lines[i]) == false)
                {
                    richTextBox1.SelectionStart = richTextBox1.GetFirstCharIndexFromLine(i);
                    richTextBox1.SelectionLength = richTextBox1.Lines[i].Length;
                    richTextBox1.SelectionColor = Color.Red;
                    stroki += richTextBox1.Lines[i].ToString() + Environment.NewLine;
                    stroki1 += richTextBox1.Lines[i].ToString() + Environment.NewLine;
                    richTextBox2.SelectionStart = richTextBox2.GetFirstCharIndexFromLine(i);
                    richTextBox2.SelectionLength = richTextBox2.Lines[i].Length;
                    richTextBox2.SelectionColor = Color.Red;
                    stroki += richTextBox2.Lines[i].ToString() + Environment.NewLine;
                    stroki2 += richTextBox2.Lines[i].ToString() + Environment.NewLine;
                }
            for (int i = s1; i < s2; i++)
            {
                if (s2 == numberOfLines2)
                {
                    richTextBox2.SelectionStart = richTextBox2.GetFirstCharIndexFromLine(i);
                    richTextBox2.SelectionLength = richTextBox2.Lines[i].Length;
                    richTextBox2.SelectionColor = Color.Red;
                    stroki += richTextBox2.Lines[i].ToString() + Environment.NewLine;
                    stroki2 += richTextBox2.Lines[i].ToString() + Environment.NewLine;
                }
                else
                {
                    richTextBox1.SelectionStart = richTextBox1.GetFirstCharIndexFromLine(i);
                    richTextBox1.SelectionLength = richTextBox1.Lines[i].Length;
                    richTextBox1.SelectionColor = Color.Red;
                    stroki += richTextBox1.Lines[i].ToString() + Environment.NewLine;
                    stroki1 += richTextBox1.Lines[i].ToString() + Environment.NewLine;
                }
            }
            SaveStroki = stroki;
            SaveStroki1 = stroki1;
            SaveStroki2 = stroki2;
            DBClass classDB = new DBClass();
            classDB.Information("online", "string comparsion", DateTime.Now);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile1 = new SaveFileDialog();
            saveFile1.DefaultExt = "*.rtf";
            saveFile1.Filter = "RTF Files|*.rtf";

            if (saveFile1.ShowDialog() == DialogResult.OK && saveFile1.FileName.Length > 0)
            {
                File.WriteAllText(saveFile1.FileName, SaveStroki, Encoding.GetEncoding(1251));
                DBClass classDB = new DBClass();
                classDB.Information("online", "saving mismatched lines", DateTime.Now);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile1 = new SaveFileDialog();
            saveFile1.DefaultExt = "*.rtf | *.txt";
            saveFile1.Filter = "RTF Files|*.rtf | TXT Files|*.txt";

            if (saveFile1.ShowDialog() == DialogResult.OK && saveFile1.FileName.Length > 0)
            {
                richTextBox1.SaveFile(saveFile1.FileName, RichTextBoxStreamType.PlainText);
            }
            richTextBox1.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile1 = new SaveFileDialog();
            saveFile1.DefaultExt = "*.rtf | *.txt";
            saveFile1.Filter = "RTF Files|*.rtf | TXT Files|*.txt";

            if (saveFile1.ShowDialog() == DialogResult.OK && saveFile1.FileName.Length > 0)
            {
                richTextBox1.SaveFile(saveFile1.FileName, RichTextBoxStreamType.PlainText);
            }
            richTextBox2.Text = "";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Multiselect = true;
            openFile.Filter = "Text files|*.txt";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                string[] filenames = openFile.FileNames;
                try
                {
                    fileload = filenames[0];
                    fileload1 = filenames[1];
                    richTextBox1.Text = "";
                    richTextBox2.Text = "";
                    richTextBox1.Text = File.ReadAllText(fileload);
                    richTextBox2.Text = File.ReadAllText(fileload1);
                    DBClass classDB = new DBClass();
                    classDB.Information("online", "loading text", DateTime.Now);
                }
                catch
                {
                    if (richTextBox1.Text.Length == 0)
                        richTextBox1.Text = File.ReadAllText(fileload);
                    else
                        richTextBox2.Text = File.ReadAllText(fileload);
                    DBClass classDB = new DBClass();
                    classDB.Information("online", "loading text", DateTime.Now);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog o1 = new OpenFileDialog();
            o1.Filter = "INI File |*.ini";
            if (o1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                inifile ini = new inifile(o1.FileName);
                string ini0 = ini.ReadINI("richTextBox1", "NumStr");
                string ini00 = ini.ReadINI("richTextBox2", "NumStr2");
                string ini1 = ini.ReadINI("Button", "Value_button1");
                int NumStr = Convert.ToInt32(ini0);
                for (int p = 0; p < NumStr; p++)
                {
                    string ini2 = ini.ReadINI("richTextBox1", Convert.ToString(p));
                    richTextBox1.Text += ini2 + Environment.NewLine;
                }
                int NumStr2 = Convert.ToInt32(ini00);
                for (int p = 0; p < NumStr; p++)
                {
                    string ini22 = ini.ReadINI("richTextBox2", Convert.ToString(p));
                    richTextBox2.Text += ini22 + Environment.NewLine;
                }
                string ini3 = ini.ReadINI("Button", "Value_button2");
                string ini4 = ini.ReadINI("Button", "Value_button1_color");
                string ini5 = ini.ReadINI("Button", "Value_button2_color");
                button1.Text = ini1;
                button2.Text = ini3;
                Color color2 = Color.FromName(ini5);
                Color color1 = Color.FromName(ini4);
                button1.BackColor = color1;
                button2.BackColor = color2;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveINI = new SaveFileDialog();
            saveINI.Filter = "INI File |*.ini";
            if (saveINI.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                inifile ini = new inifile(saveINI.FileName);
                ini.Write("Button", "Value_button1", button1.Text);
                int p;
                for (p = 0; p < richTextBox1.Lines.Length; p++)
                {
                    ini.Write("richTextBox1", Convert.ToString(p), richTextBox1.Lines[p].ToString());
                }
                int p1;
                for (p1 = 0; p1 < richTextBox1.Lines.Length; p1++)
                {
                    ini.Write("richTextBox2", Convert.ToString(p1), richTextBox2.Lines[p1].ToString());
                }
                ini.Write("richTextBox1", "NumStr", Convert.ToString(p));
                ini.Write("richTextBox2", "NumStr2", Convert.ToString(p1));
                ini.Write("Button", "Value_button2", button2.Text);
                ini.Write("Button", "Value_button1_color", button1.BackColor.ToString());
                ini.Write("Button", "Value_button2_color", button2.BackColor.ToString());
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            XDocument doc = new XDocument();
            XElement texts = new XElement("Text", SaveStroki);
            doc.Add(texts);
            doc.Save("D:\\text.xml");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            using (StreamReader sr = new StreamReader(fileload, Encoding.GetEncoding(1251)))
            {
                String ltext = sr.ReadToEnd();
                richTextBox1.Text = ltext;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            using (StreamReader sr = new StreamReader(fileload1, Encoding.GetEncoding(1251)))
            {
                String ltext1 = sr.ReadToEnd();
                richTextBox2.Text = ltext1;
            }
        }

        static string openFile = " ";

        private void button11_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text))
            {
                try
                {
                    DBClass classDB = new DBClass();
                    openFile = textBox1.Text;
                    string content = classDB.OpenText(openFile);
                    richTextBox1.Text = content;
                }
                catch
                {
                    MessageBox.Show("Текста с таким названием не существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    richTextBox1.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Выбирите текст!", "WARNING, CRITICAL ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text))
            {
                try
                {
                    DBClass classDB = new DBClass();
                    openFile = textBox1.Text;
                    string content = classDB.OpenText(openFile);
                    richTextBox2.Text = content;
                }
                catch
                {
                    MessageBox.Show("Текста с таким названием не существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    richTextBox2.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Выбирите текст!", "WARNING, CRITICAL ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text) &&
                !string.IsNullOrEmpty(richTextBox1.Text) && !string.IsNullOrWhiteSpace(richTextBox1.Text))
                {
                    DBClass classDB = new DBClass();
                    classDB.SaveText(textBox1.Text, richTextBox1.Text);
                    listView1.Items.Clear();
                    SelectTable();
                }
            }
            catch
            {
                MessageBox.Show("Не введено название текста или сам текст не существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text) &&
                !string.IsNullOrEmpty(richTextBox2.Text) && !string.IsNullOrWhiteSpace(richTextBox2.Text))
                {
                    DBClass classDB = new DBClass();
                    classDB.SaveText(textBox1.Text, richTextBox2.Text);
                    listView1.Items.Clear();
                    SelectTable();
                }
            }
            catch
            {
                MessageBox.Show("Не введено название текста или сам текст не существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void Child_Load(object sender, EventArgs e)
        {
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.View = View.Details;
            listView1.Columns.Add("Название текста (БД)");
            listView1.Columns[0].Width = 300;
            SelectTable();
        }

        public void SelectTable()
        {
            DBClass classDB = new DBClass();
            SqlDataReader command = classDB.SelectName();
            while (command.Read())
            {
                ListViewItem item = new ListViewItem(new string[]{
                Convert.ToString(command["name"]),
                });
                listView1.Items.Add(item);
            }
            command.Close();
        }
        private void Select(object sender, EventArgs e)
        {
            textBox1.Text = listView1.FocusedItem.SubItems[0].Text;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            DBClass classDB = new DBClass();
            classDB.SaveNesovpadStrokText(SaveStroki1, SaveStroki2);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            int wSize = Size.Width;
            int hSize = Size.Height;
            DBClass classDB = new DBClass();
            classDB.IniAdd(wSize, hSize);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            DBClass classDB = new DBClass();
            int wSizeL = classDB.ReadWidth();
            int hSizeL = classDB.ReadHeight();
            Width = wSizeL;
            Height = hSizeL;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            DBClass classDB = new DBClass();
            classDB.Information("offline", "exit from the program", DateTime.Now);
            Application.Exit();
        }
    }
}
