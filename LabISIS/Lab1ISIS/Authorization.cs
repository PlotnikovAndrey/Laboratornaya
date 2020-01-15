using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;

namespace Lab1ISIS
{
    public partial class Authorization : Form
    {
        public Authorization()
        {
            InitializeComponent();

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            client = new HttpClient(httpClientHandler);
            client.BaseAddress = new Uri(Properties.Settings.Default.WebSettings);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));


            timer1.Start();
            string symbols = "abcdefghijklmnopqrstuvwxyz";
            Random rand = new Random();
            for (int i = 0; i < 5; i++, textBox3.Text += symbols[rand.Next(symbols.Length)].ToString()) ;
        }

        public static bool user;
        public static HttpClient client;

        public class Info
        {
            public int Id { get; set; }

            public string status { get; set; }

            public string info { get; set; }

            public string date { get; set; }
        }

        private async Task<Uri> CreateInfo(Info info)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "info", info);
            response.EnsureSuccessStatusCode();

            return response.Headers.Location;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox3.Text = "";
            string symbols = "abcdefghijklmnopqrstuvwxyz";
            Random rand = new Random();
            for (int i = 0; i < 5; i++, textBox3.Text += symbols[rand.Next(symbols.Length)].ToString()) ;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text)
                && textBox3.Text == textBox4.Text)
            {
                DBClass classDB = new DBClass();

                //user = classDB.Auth(textBox1.Text, textBox2.Text);
                //if (user == true)
                if (textBox1.Text == "1" && textBox2.Text == "1")
                {
                    Form Form1 = new Form1();
                    Form1.Show();
                    this.Hide();
                    Info info = new Info();
                    info.status = "online";
                    info.info = "logging in";
                    DateTime now = DateTime.Now;
                    info.date = now.ToString("dd.MM.yyyy hh:mm");
                    CreateInfo(info);
                }
                else
                {
                    Info info = new Info();
                    info.status = "offline";
                    info.info = "connection to the program,login failed";
                    DateTime now = DateTime.Now;
                    info.date = now.ToString("dd.MM.yyyy hh:mm");
                    MessageBox.Show("Вы ввели неверный логин или пароль или капчу ", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    CreateInfo(info);
                    //classDB.Information("offline", "connection to the program,login failed", DateTime.Now);
                }
            }
            else
            {
                MessageBox.Show("Одно или несколько полей не заполнены", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DBClass classDB = new DBClass();
                //classDB.Information("offline", "connection to the program,login failed", DateTime.Now);
            }

        }
    }
}
