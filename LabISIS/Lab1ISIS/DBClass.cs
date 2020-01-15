using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Lab1ISIS
{
    class DBClass
    {
        SqlConnection sqlConnection;

        public void OpenConnect()
        {
            string connectionString = @"Data Source=DESKTOP-CLA45I2;Initial Catalog=Lab3ISIS;Integrated Security=True";
            //DESKTOP-9TGS6MT\SQLEXPRESS
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
        }

        SqlConnection sqlConnection2;

        public void OpenConnect2()
        {
            string connectionString2 = @"Data Source=DESKTOP-CLA45I2;Initial Catalog=Information;Integrated Security=True";
            //DESKTOP-9TGS6MT\SQLEXPRESS
            sqlConnection2 = new SqlConnection(connectionString2);
            sqlConnection2.Open();
        }

        public void Information(string status, string info, DateTime date)
        {
            OpenConnect2();
            SqlCommand saveText = new SqlCommand("Insert into Infoes (status, info, date) VALUES (@Status, @Info, @Date)", sqlConnection2);
            saveText.Parameters.AddWithValue("Status", status);
            saveText.Parameters.AddWithValue("Info", info);
            saveText.Parameters.AddWithValue("Date", date);
            saveText.ExecuteNonQuery();
            CloseConnect2();
        }
        public void CloseConnect2()
        {
            sqlConnection2.Close();
        }

        public void CloseConnect()
        {
            sqlConnection.Close();
        }

        public bool Auth(string login, string password)
        {
            OpenConnect();
            bool answer = false;
            try
            {
                SqlCommand authorization = new SqlCommand("select password from Users where login = @Login and password = @Password", sqlConnection);
                authorization.Parameters.AddWithValue("@Login", login);
                authorization.Parameters.AddWithValue("@Password", password);
                string pass = (string)authorization.ExecuteScalar();
                if (pass == password)
                {
                    answer = true;
                    return answer;
                }
                else
                {
                    answer = false;
                    return answer;
                }
            }
            catch
            {
                answer = false;

            }
            CloseConnect();
            return answer;
        }
        public void SaveText(string name, string text)
        {
            OpenConnect();
            SqlCommand saveText = new SqlCommand("Insert into Text (name, content) VALUES (@Name, @Content)", sqlConnection);
            saveText.Parameters.AddWithValue("Name", name);
            saveText.Parameters.AddWithValue("Content", text);
            saveText.ExecuteNonQuery();
            CloseConnect();
        }

        public void SaveNesovpadStrokText(string text1, string text2)
        {
            OpenConnect();
            SqlCommand saveText = new SqlCommand("Insert into MismatchedLines ([mismatched lines text 1], [mismatched lines text 2]) VALUES (@Text1, @Text2)", sqlConnection);
            saveText.Parameters.AddWithValue("Text1", text1);
            saveText.Parameters.AddWithValue("Text2", text2);
            saveText.ExecuteNonQuery();
            CloseConnect();
        }

        public SqlDataReader SelectName()
        {
            OpenConnect();
            SqlCommand saveText = new SqlCommand("Select name from Text", sqlConnection);
            return saveText.ExecuteReader();
        }

        public string OpenText(string name)
        {
            OpenConnect();
            string strtext = "";
            SqlCommand loadtxt = new SqlCommand("select content from Text where name = @name", sqlConnection);
            loadtxt.Parameters.AddWithValue("name", name);
            strtext = (string)loadtxt.ExecuteScalar();
            CloseConnect();
            return strtext;
        }

        public void IniAdd(int wSize, int hSize)
        {
            OpenConnect();
            SqlCommand Widthcom = new SqlCommand("update IniSettings set value = @valueW where name = @Width", sqlConnection);
            Widthcom.Parameters.AddWithValue("Width", "Width");
            Widthcom.Parameters.AddWithValue("valueW", wSize);
            Widthcom.ExecuteScalar();

            SqlCommand Heightcom = new SqlCommand("update IniSettings set value = @valueH where name = @Height", sqlConnection);
            Heightcom.Parameters.AddWithValue("Height", "Height");
            Heightcom.Parameters.AddWithValue("valueH", hSize);
            Heightcom.ExecuteScalar();
        }

        public int ReadWidth()
        {
            OpenConnect();
            SqlCommand Widthcom = new SqlCommand("select value from IniSettings where name = @Width", sqlConnection);
            Widthcom.Parameters.AddWithValue("Width", "Width");
            int Width = Convert.ToInt32(Widthcom.ExecuteScalar());
            CloseConnect();
            return Width;
        }

        public int ReadHeight()
        {
            OpenConnect();
            SqlCommand Heightcom = new SqlCommand("select value from IniSettings where name = @Height", sqlConnection);
            Heightcom.Parameters.AddWithValue("Height", "Height");
            int Height = Convert.ToInt32(Heightcom.ExecuteScalar());
            CloseConnect();
            return Height;
        }


    }
}

