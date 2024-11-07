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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            panel1.BackColor = Color.Pink;
            panel2.BackColor = Color.Pink;
            panel3.BackColor = Color.Pink;
            panel4.BackColor = Color.Pink;
            panel5.BackColor = Color.Pink;
            panel6.BackColor = Color.Pink;
        }

        private void roundButton2_Click(object sender, EventArgs e) //назад
        {
            if (Application.OpenForms.OfType<CabAdmin>().Count() > 0)
            {
                Form ifrm = Application.OpenForms[1];
                ifrm.Show();
            }
            this.Close();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=ADCLG1;Initial Catalog=practica_17;Integrated Security=True"; 
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string selectQuery = $"select id_client, full_name, phone, email, password, login from Clients"; 
            SqlCommand command = new SqlCommand(selectQuery, connection);
            SqlDataReader reader = command.ExecuteReader();
            List<string[]> data = new List<string[]>();
            while (reader.Read())
            {
                data.Add(new string[6]);
                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
                data[data.Count - 1][3] = reader[3].ToString();
                data[data.Count - 1][4] = reader[4].ToString();
                data[data.Count - 1][5] = reader[5].ToString();
            }
            reader.Close();
            connection.Close();
            foreach (string[] s in data)
                dataGridView1.Rows.Add(s);
        }

        private void roundButton4_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            string connectionString = @"Data Source=ADCLG1;Initial Catalog=practica_17;Integrated Security=True"; 
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string selectQuery = $"select id_client, full_name, phone, email, password, login from Clients";
            SqlCommand command = new SqlCommand(selectQuery, connection);
            SqlDataReader reader = command.ExecuteReader();
            List<string[]> data = new List<string[]>();
            while (reader.Read())
            {
                data.Add(new string[6]);
                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
                data[data.Count - 1][3] = reader[3].ToString();
                data[data.Count - 1][4] = reader[4].ToString();
                data[data.Count - 1][5] = reader[5].ToString();
            }
            reader.Close();
            connection.Close();
            foreach (string[] s in data)
                dataGridView1.Rows.Add(s);
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                DataGridView dgv = (DataGridView)sender;
                if (dgv.SelectedRows.Count > 0)
                {
                    textBox1.Text = dgv.SelectedRows[0].Cells[0].Value.ToString();
                    textBox2.Text = dgv.SelectedRows[0].Cells[1].Value.ToString();
                    textBox3.Text = dgv.SelectedRows[0].Cells[2].Value.ToString();
                    textBox4.Text = dgv.SelectedRows[0].Cells[3].Value.ToString();
                    textBox5.Text = dgv.SelectedRows[0].Cells[4].Value.ToString();
                    textBox7.Text = dgv.SelectedRows[0].Cells[5].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime date = DateTime.Today;
                string connectionString = @"Data Source=ADCLG1;Initial Catalog=practica_17;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                string selectQuery = $"insert into Clients (full_name, phone, email, password, login, registration_date) values ('{textBox2.Text}','{textBox3.Text}','{textBox4.Text}','{textBox5.Text}','{textBox7.Text}', '{date}')";
                SqlCommand command = new SqlCommand(selectQuery, connection);
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Клиент добавлен!");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox7.Text = "";
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void roundButton3_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = @"Data Source=ADCLG1;Initial Catalog=practica_17;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                string selectQuery = $"DELETE FROM Clients\r\nWHERE id_client = {textBox1.Text};";
                SqlCommand command = new SqlCommand(selectQuery, connection);
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Клиент удален!");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox7.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = true; 
            }
        }

        private void roundButton1_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = @"Data Source=ADCLG1;Initial Catalog=practica_17;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                string selectQuery = $"UPDATE Clients set full_name = '{textBox2.Text}', phone = '{textBox3.Text}', email = '{textBox4.Text}', password = '{textBox5.Text}', login = '{textBox7.Text}' WHERE id_client = {textBox1.Text};";
                SqlCommand command = new SqlCommand(selectQuery, connection);
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Данные клиента обновлены!");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox7.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(textBox1.Text.Length > 0)
            {
                roundButton3.Enabled = true;
            }
            else
            {
                roundButton3.Enabled = false;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if(textBox2.Text.Length > 0 && textBox3.Text.Length>0 && textBox4.Text.Length > 0 && textBox5.Text.Length > 0 && textBox7.Text.Length > 0)
            {
                button1.Enabled = true;
                roundButton1.Enabled = true;
            }
            else
            {
                button1.Enabled=false;
                roundButton1.Enabled=false;
            }
        }
    }
}
