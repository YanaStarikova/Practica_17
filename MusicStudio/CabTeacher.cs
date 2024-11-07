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
    public partial class CabTeacher : Form
    {
        public int ID;
        public string fullName = "";
        int id_service;
        public CabTeacher()
        {
            InitializeComponent();
            button3.FlatAppearance.BorderColor = Color.Purple;
            roundButton1.FlatAppearance.BorderColor = Color.Purple;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.id_teacher = ID;
            form2.id_service = id_service;
            form2.Show();
            this.Hide();
        }

        private void roundButton1_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<Form1>().Count() > 0)
            {
                Form ifrm = Application.OpenForms[0];
                ifrm.Show();

            }
            this.Close();
        }

        private void CabTeacher_Load(object sender, EventArgs e)
        {
            label1.Text = "ID: " + ID.ToString() + " " + fullName;
            label2.Text = "Преподаватель";

            string connectionString = @"Data Source=ADCLG1;Initial Catalog=practica_17;Integrated Security=True"; //строка подключения
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string selectQuery = $"select id_appointment, name, dayweek, time_start, time_end from Appointments\r\ninner join Services on (Services.id_service = Appointments.id_service)\r\nwhere id_teacher = {ID}"; //запрос на получение данных
            SqlCommand command = new SqlCommand(selectQuery, connection);
            SqlDataReader reader = command.ExecuteReader();
            List<string[]> data = new List<string[]>();
            while (reader.Read())
            {
                data.Add(new string[5]);
                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
                data[data.Count - 1][3] = reader[3].ToString();
                data[data.Count - 1][4] = reader[4].ToString();
            }
            reader.Close();

            selectQuery = $"select id_service from Appointments where id_teacher = {ID}";
            command = new SqlCommand(selectQuery, connection);  
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                id_service = reader.GetInt32(0);
            }
            reader.Close();

            connection.Close();
            foreach (string[] s in data)
                dataGridView2.Rows.Add(s);
        }

        private void roundButton2_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            string connectionString = @"Data Source=ADCLG1;Initial Catalog=practica_17;Integrated Security=True"; //строка подключения
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string selectQuery = $"select id_appointment, name, dayweek, time_start, time_end from Appointments\r\ninner join Services on (Services.id_service = Appointments.id_service)\r\nwhere id_teacher = {ID}"; //запрос на получение данных
            SqlCommand command = new SqlCommand(selectQuery, connection);
            SqlDataReader reader = command.ExecuteReader();
            List<string[]> data = new List<string[]>();
            while (reader.Read())
            {
                data.Add(new string[5]);
                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
                data[data.Count - 1][3] = reader[3].ToString();
                data[data.Count - 1][4] = reader[4].ToString();
            }
            reader.Close();
            connection.Close();
            foreach (string[] s in data)
                dataGridView2.Rows.Add(s);
        }
    }
}
