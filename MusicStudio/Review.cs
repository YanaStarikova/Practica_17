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
    public partial class Review : Form
    {
        public Review()
        {
            InitializeComponent();
            roundButton1.FlatAppearance.BorderColor = Color.Purple;
        }

        private void Review_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=ADCLG1;Initial Catalog=practica_17;Integrated Security=True"; 
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string selectQuery = $"select id_lesson, full_name,comment from Reviews \r\ninner join Clients on (Clients.id_client = Reviews.id_client)"; 
            SqlCommand command = new SqlCommand(selectQuery, connection);
            SqlDataReader reader = command.ExecuteReader();
            List<string[]> data = new List<string[]>();
            while (reader.Read()) 
            {
                data.Add(new string[3]);
                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
            }
            reader.Close();
            connection.Close();
            foreach (string[] s in data)
                dataGridView1.Rows.Add(s);
        }

        private void roundButton2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            string connectionString = @"Data Source=ADCLG1;Initial Catalog=practica_17;Integrated Security=True"; 
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string selectQuery = $"select id_lesson, full_name,comment from Reviews \r\ninner join Clients on (Clients.id_client = Reviews.id_client)"; 
            SqlCommand command = new SqlCommand(selectQuery, connection);
            SqlDataReader reader = command.ExecuteReader();
            List<string[]> data = new List<string[]>();
            while (reader.Read())
            {
                data.Add(new string[3]);
                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
            }
            reader.Close();
            connection.Close();
            foreach (string[] s in data)
                dataGridView1.Rows.Add(s);
        }

        private void roundButton1_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<CabClient>().Count() > 0)
            {
                Form ifrm = Application.OpenForms[1];
                ifrm.Show();
            }
            this.Close();
        }
    }
}
