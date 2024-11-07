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
    public partial class CabClient : Form
    {
        public int ID;
        public string fullName;
        public CabClient()
        {
            InitializeComponent();
            button1.FlatAppearance.BorderColor = Color.Purple;
            button2.FlatAppearance.BorderColor = Color.Purple;
            button3.FlatAppearance.BorderColor = Color.Purple;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.id_client = ID;
            form3.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.id_client = ID;
            form4.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Review review = new Review();
            review.Show();
            this.Hide();
        }

        private void CabClient_Load(object sender, EventArgs e)
        {
            label1.Text = "ID: "+ID.ToString() + " " + fullName;
            label2.Text = "Обучающийся";

            string connectionString = @"Data Source=ADCLG1;Initial Catalog=practica_17;Integrated Security=True"; //строка подключения
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string selectQuery = $"SELECT id_lessons, \r\nLessons.id_clients,\r\nServices.name,\r\nTeachers.full_name,\r\nAppointments.dayweek, \r\nAppointments.time_start, \r\nAppointments.time_end,\r\nLessons.date_lesson\r\n\r\nFROM Lessons \r\ninner Join Appointments on (Lessons.id_appointment = Appointments.id_appointment)\r\ninner Join Clients on (Lessons.id_clients = Clients.id_client)\r\ninner Join Services  on (Services.id_service = Appointments.id_service)\r\ninner join Teachers on (Teachers.id_teacher = Appointments.id_teacher)\r\nWhere Lessons.id_clients = {ID}"; //запрос на получение данных
            SqlCommand command = new SqlCommand(selectQuery, connection);
            SqlDataReader reader = command.ExecuteReader();
            List<string[]> data = new List<string[]>();
            while (reader.Read())
            {
                data.Add(new string[8]);
                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
                data[data.Count - 1][3] = reader[3].ToString();
                data[data.Count - 1][4] = reader[4].ToString();
                data[data.Count - 1][5] = reader[5].ToString();
                data[data.Count - 1][6] = reader[6].ToString();
                data[data.Count - 1][7] = reader[7].ToString();
            }
            reader.Close();
            connection.Close();
            foreach (string[] s in data)
                dataGridView1.Rows.Add(s);
        }

        private void roundButton1_Click(object sender, EventArgs e) //выход
        {
            if (Application.OpenForms.OfType<Form1>().Count() > 0)
            {
                Form ifrm = Application.OpenForms[0];
                ifrm.Show();
                
            }
            this.Close();
        }

        private void roundButton2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            string connectionString = @"Data Source=ADCLG1;Initial Catalog=practica_17;Integrated Security=True"; //строка подключения
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string selectQuery = $"SELECT id_lessons, \r\nLessons.id_clients,\r\nServices.name,\r\nTeachers.full_name,\r\nAppointments.dayweek, \r\nAppointments.time_start, \r\nAppointments.time_end,\r\nLessons.date_lesson\r\n\r\nFROM Lessons \r\ninner Join Appointments on (Lessons.id_appointment = Appointments.id_appointment)\r\ninner Join Clients on (Lessons.id_clients = Clients.id_client)\r\ninner Join Services  on (Services.id_service = Appointments.id_service)\r\ninner join Teachers on (Teachers.id_teacher = Appointments.id_teacher)\r\nWhere Lessons.id_clients = {ID}"; //запрос на получение данных
            SqlCommand command = new SqlCommand(selectQuery, connection);
            SqlDataReader reader = command.ExecuteReader();
            List<string[]> data = new List<string[]>();
            while (reader.Read())
            {
                data.Add(new string[8]);
                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
                data[data.Count - 1][3] = reader[3].ToString();
                data[data.Count - 1][4] = reader[4].ToString();
                data[data.Count - 1][5] = reader[5].ToString();
                data[data.Count - 1][6] = reader[6].ToString();
                data[data.Count - 1][7] = reader[7].ToString();
            }
            reader.Close();
            connection.Close();
            foreach (string[] s in data)
                dataGridView1.Rows.Add(s);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
