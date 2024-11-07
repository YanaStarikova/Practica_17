using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MusicStudio
{
    public partial class Form2 : Form
    {
        public int id_teacher;
        public int id_service;
        public Form2()
        {
            InitializeComponent();
            button1.FlatAppearance.BorderColor = Color.Purple;
            roundButton1.FlatAppearance.BorderColor= Color.Purple;
            roundButton2.FlatAppearance.BorderColor= Color.Purple;
        }

        private void roundButton2_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<CabTeacher>().Count() > 0)
            {
                Form ifrm = Application.OpenForms[1];
                ifrm.Show();
            }
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e) //добавить
        {
            try
            {
                string connectionString = @"Data Source=ADCLG1;Initial Catalog=practica_17;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlquery = "INSERT INTO Appointments (id_service, id_teacher, dayweek, time_start, time_end) VALUES (@id_service, @id_teacher, @dayweek, @time_start, @time_end)";
                    using (SqlCommand command = new SqlCommand(sqlquery, connection))
                    {
                        
                        command.Parameters.AddWithValue("@id_service", id_service);// Добавляем параметры
                        command.Parameters.AddWithValue("@id_teacher", id_teacher);
                        command.Parameters.AddWithValue("@dayweek", comboBox3.Text); 
                        
                        TimeSpan timeStart = TimeSpan.Parse(comboBox1.Text);// Преобразуем строку в TimeSpan
                        TimeSpan timeEnd = TimeSpan.Parse(comboBox4.Text);
                        if(timeStart >= timeEnd)
                        {
                            Exception exception = new Exception("Время начала должно быть меньше время конца!");
                            MessageBox.Show(exception.Message);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@time_start", timeStart);
                            command.Parameters.AddWithValue("@time_end", timeEnd);

                            command.ExecuteNonQuery();
                            MessageBox.Show("Запись добавлена!");
                        }

                    }
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Пожалуйста, введите корректное время!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form2_Load(object sender, EventArgs e) //загрузка
        {
        }

        private void roundButton1_Click(object sender, EventArgs e) //удалить
        {
            try
            {
                string connectionString = @"Data Source=ADCLG1;Initial Catalog=practica_17;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                string sqlquery = $"DELETE FROM Appointments\r\nWHERE id_appointment = {textBox1.Text};";
                SqlCommand command = new SqlCommand(sqlquery, connection);
                command.ExecuteNonQuery();
                MessageBox.Show("Запись удалена!");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if(comboBox1.Text.Length > 0 && comboBox3.Text.Length > 0 && comboBox4.Text.Length > 0)
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                roundButton1.Enabled = true;
            }
            else
            {
                roundButton1.Enabled = false;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = true; // предотвратить ввод буквы
            }
        }
    }
}
