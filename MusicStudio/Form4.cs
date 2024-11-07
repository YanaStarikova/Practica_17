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
using System.Windows.Forms.Design;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MusicStudio
{
    public partial class Form4 : Form
    {
        int id_lessons;
        public int id_client;

        public Form4()
        {
            InitializeComponent();
            button1.FlatAppearance.BorderColor = Color.Purple;
            roundButton2.FlatAppearance.BorderColor = Color.Purple;
        }

        private void roundButton2_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<CabClient>().Count() > 0)
            {
                Form ifrm = Application.OpenForms[1];
                ifrm.Show();
            }
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(comboBox3.Text.Length > 0 && textBox1.Text.Length > 0)
            {
                button1.Enabled = true;
            }
            else button1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e) //отправить
        {
            try
            {
                string connectionString = @"Data Source=ADCLG1;Initial Catalog=practica_17;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                string sqlquery = $"insert into Reviews(id_lesson, id_client, comment) values ({comboBox3.Text},{id_client},'{textBox1.Text}');";
                SqlCommand command = new SqlCommand(sqlquery, connection);
                command.ExecuteNonQuery();
                MessageBox.Show("отзыв отправлен!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=ADCLG1;Initial Catalog=practica_17;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string sqlquery = $"Select id_lessons from Lessons where id_clients = {id_client}";
            SqlCommand command = new SqlCommand(sqlquery, connection);
            SqlDataReader reader = command.ExecuteReader();

            List<string[]> data = new List<string[]>();

            while (reader.Read())
            {
                data.Add(new string[1]);
                data[data.Count - 1][0] = reader[0].ToString();
            }
            reader.Close();
            connection.Close();

            foreach (string[] s in data)
            {
                comboBox3.Items.Add(s[0]);
            }
        }

        private void comboBox3_TextChanged(object sender, EventArgs e)
        {
            if (comboBox3.Text.Length > 0 && textBox1.Text.Length >0)
            {
                button1.Enabled=true;
            }
            else button1.Enabled = false;
        }
    }
}
