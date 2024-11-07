using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicStudio
{
    public partial class Form3 : Form
    {
        int id_service;
        int id_appointment;
        int id_teacher;
        public int id_client;

        public Form3()
        {
            InitializeComponent();
            button1.FlatAppearance.BorderColor = Color.Purple;
            roundButton2.FlatAppearance.BorderColor = Color.Purple;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=ADCLG1;Initial Catalog=practica_17;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string sqlquery =  $"Select name from Services;";
            SqlCommand command = new SqlCommand(sqlquery,connection);
            SqlDataReader reader = command.ExecuteReader();

            List<string[]> data_name = new List<string[]>();

            while (reader.Read()) 
            {
                data_name.Add(new string[1]);
                data_name[data_name.Count - 1][0] = reader[0].ToString();
            }
            reader.Close();
            connection.Close();
            foreach(string[] s in data_name)
            {
                comboBox1.Items.Add(s[0]); //добавляем в comboBox данные о виде занятий
            }
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            comboBox5.Items.Clear();
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            comboBox5.Text = "";

            id_service = comboBox1.SelectedIndex + 1;

            comboBox3.Enabled = true;
            comboBox4.Enabled = true;

            string connectionString = @"Data Source=ADCLG1;Initial Catalog=practica_17;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string sqlquery = $"Select time_start from Appointments where id_service = {id_service};";
            SqlCommand command = new SqlCommand(sqlquery, connection);
            SqlDataReader reader = command.ExecuteReader();

            List<string[]> data_time_start = new List<string[]>(); //список с началом времени
            List<string[]> data_teachers = new List<string[]>();


            while (reader.Read())
            {
                data_time_start.Add(new string[1]);
                data_time_start[data_time_start.Count - 1][0] = reader[0].ToString();
            }
            reader.Close();

            sqlquery = $"Select full_name from Teachers\r\ninner join Appointments on (Appointments.id_teacher = Teachers.id_teacher) \r\nwhere id_service = {id_service};";
            command = new SqlCommand(sqlquery, connection);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                data_teachers.Add(new string[1]);
                data_teachers[data_teachers.Count - 1][0] = reader[0].ToString();
            }
            reader.Close ();
            connection.Close();

            foreach (string[] s in data_time_start)
            {
                comboBox3.Items.Add(s[0]); //добавляем в comboBox данные
            }
            foreach (string[] s in data_teachers)
            {
                comboBox4.Items.Add(s[0]); 
            }

        }

        private void comboBox3_TextChanged(object sender, EventArgs e)
        {
            if(comboBox1.Text.Length > 0 && comboBox3.Text.Length>0 && comboBox4.Text.Length > 0)
            {
                comboBox2.Enabled = true;

                string connectionString = @"Data Source=ADCLG1;Initial Catalog=practica_17;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                string sqlquery = $"Select time_end from Appointments where time_start = '{comboBox3.Text}';";
                SqlCommand command = new SqlCommand(sqlquery, connection);
                SqlDataReader reader = command.ExecuteReader();

                List<string[]> data_time_end = new List<string[]>();

                while (reader.Read())
                {
                    data_time_end.Add(new string[1]);
                    data_time_end[data_time_end.Count - 1][0] = reader[0].ToString();
                }
                reader.Close();
                connection.Close();

                foreach (string[] s in data_time_end)
                {
                    comboBox2.Items.Add(s[0]);
                }
            }
            else
            {
                comboBox2.Enabled = false;
            }
            
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBox2.Text.Length > 0 && comboBox3.Text.Length > 0)
                {
                    comboBox5.Enabled = true;

                    string connectionString = @"Data Source=ADCLG1;Initial Catalog=practica_17;Integrated Security=True";
                    SqlConnection connection = new SqlConnection(connectionString);
                    connection.Open();
                    List<string[]> data_days = new List<string[]>();
                    string sqlquery = $"select dayweek from Appointments\r\nwhere time_start = '{comboBox3.Text}' and time_end = '{comboBox2.Text}'";
                    SqlCommand command = new SqlCommand(sqlquery, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        data_days.Add(new string[1]);
                        data_days[data_days.Count - 1][0] = reader[0].ToString();
                    }

                    foreach (string[] s in data_days)
                    {
                        comboBox5.Items.Add(s[0]);
                    }
                }
                else
                {
                    comboBox5.Enabled = false;
                }

                if (comboBox1.Text.Length > 0 && comboBox3.Text.Length > 0 && comboBox4.Text.Length > 0 && comboBox2.Text.Length > 0 && comboBox5.Text.Length > 0)
                {
                    button1.Enabled = true;
                }
                else button1.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e) //записаться
        {
            try
            {
                string connectionString = @"Data Source=ADCLG1;Initial Catalog=practica_17;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                string sqlquery = $"select id_appointment from Appointments\r\ninner join Teachers on (Appointments.id_teacher = Teachers.id_teacher)\r\ninner join Services on (Appointments.id_service = Services.id_service)\r\nwhere time_start = '{comboBox3.Text}' and time_end = '{comboBox2.Text}' and dayweek = '{comboBox5.Text}' and Appointments.id_teacher = {id_teacher} and Services.id_service = {id_service}";
                SqlCommand command = new SqlCommand(sqlquery, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    id_appointment = reader.GetInt32(0);
                }
                reader.Close();

                string dayweek = comboBox5.Text;
                string day = "";
                DateTime date = DateTime.Today; //сегодняшняя дата
                while (true) //пробегаемся по циклу, чтобы вычислить ближайщую дату занятия
                {
                    day = date.DayOfWeek.ToString();
                    if (dayweek == day)
                    {
                        break;
                    }
                    else
                    {
                        date = date.AddDays(1);
                    }
                }

                sqlquery = $"insert into Lessons (id_appointment, id_clients, date_lesson)\r\nvalues ({id_appointment},{id_client},'{date}');";
                command = new SqlCommand(sqlquery, connection);
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Вы успешно записаны!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void comboBox4_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string teacher = comboBox4.Text;
                string connectionString = @"Data Source=    ;Initial Catalog=practica_17;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                string sqlquery = $"select id_teacher from Teachers where full_name = '{teacher}'";
                SqlCommand command = new SqlCommand(sqlquery, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    id_teacher = reader.GetInt32(0);
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void roundButton2_Click_1(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<CabClient>().Count() > 0)
            {
                Form ifrm = Application.OpenForms[1];
                ifrm.Show();
            }
            this.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox5.Items.Clear();
            if (comboBox2.Text.Length > 0 && comboBox3.Text.Length > 0)
            {
                comboBox5.Enabled = true;

                string connectionString = @"Data Source=ADCLG1;Initial Catalog=practica_17;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                List<string[]> data_days = new List<string[]>();
                string sqlquery = $"select dayweek from Appointments\r\nwhere time_start = '{comboBox3.Text}' and time_end = '{comboBox2.Text}'";
                SqlCommand command = new SqlCommand(sqlquery, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    data_days.Add(new string[1]);
                    data_days[data_days.Count - 1][0] = reader[0].ToString();
                }

                foreach (string[] s in data_days)
                {
                    comboBox5.Items.Add(s[0]);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
