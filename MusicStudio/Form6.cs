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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MusicStudio
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
            button1.FlatAppearance.BorderColor = Color.Purple;
            roundButton1.FlatAppearance.BorderColor = Color.Purple;
            roundButton2.FlatAppearance.BorderColor = Color.Purple;
            roundButton3.FlatAppearance.BorderColor = Color.Purple;
            panel1.BackColor = Color.Pink;
            panel2.BackColor = Color.Pink;
            panel3.BackColor = Color.Pink;
        }

        private void button1_Click(object sender, EventArgs e) //добавить
        {
            try
            {
                DateTime date = DateTime.Today;
                string connectionString = @"Data Source=ADCLG1;Initial Catalog=practica_17;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                string selectQuery = $"insert into Services (name, price, need_a_musical_instrument) values ('{textBox2.Text}','{textBox3.Text}','{comboBox1.Text}')";
                SqlCommand command = new SqlCommand(selectQuery, connection);
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Услуга добавлена!");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                comboBox1.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void roundButton1_Click(object sender, EventArgs e) //изменить
        {
            try
            {
                string connectionString = @"Data Source=ADCLG1;Initial Catalog=practica_17;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                string selectQuery = $"UPDATE Services set name = '{textBox2.Text}', price = '{textBox3.Text}', need_a_musical_instrument = '{comboBox1.Text}' WHERE id_service = {textBox1.Text};";
                SqlCommand command = new SqlCommand(selectQuery, connection);
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Данные услуги обновлены!");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                comboBox1.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void roundButton3_Click(object sender, EventArgs e) //удалить
        {
            try
            {
                string connectionString = @"Data Source=ADCLG1;Initial Catalog=practica_17;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                string selectQuery = $"DELETE FROM Services\r\nWHERE id_service = {textBox1.Text};";
                SqlCommand command = new SqlCommand(selectQuery, connection);
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Услуга удалена!");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void roundButton2_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<CabAdmin>().Count() > 0)
            {
                Form ifrm = Application.OpenForms[1];
                ifrm.Show();
            }
            this.Close();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=ADCLG1;Initial Catalog=practica_17;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string selectQuery = $"select id_service, name, price, need_a_musical_instrument from Services";
            SqlCommand command = new SqlCommand(selectQuery, connection);
            SqlDataReader reader = command.ExecuteReader();
            List<string[]> data = new List<string[]>();
            while (reader.Read())
            {
                data.Add(new string[4]);
                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
                data[data.Count - 1][3] = reader[3].ToString();
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
            string selectQuery = $"select id_service, name, price, need_a_musical_instrument from Services";
            SqlCommand command = new SqlCommand(selectQuery, connection);
            SqlDataReader reader = command.ExecuteReader();
            List<string[]> data = new List<string[]>();
            while (reader.Read())
            {
                data.Add(new string[4]);
                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
                data[data.Count - 1][3] = reader[3].ToString();
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
                    comboBox1.Text = dgv.SelectedRows[0].Cells[3].Value.ToString();
                }
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
            else roundButton3.Enabled=false;
        }

        private void Form6_TextChanged(object sender, EventArgs e)
        {
            if(textBox2.Text.Length> 0 && textBox3.Text.Length>0 && comboBox1.Text.Length > 0)
            {
                roundButton1.Enabled = true;
                button1.Enabled = true;
            }
            else
            {
                roundButton1.Enabled = false;
                button1.Enabled = false;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
