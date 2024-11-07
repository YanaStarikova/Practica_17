using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicStudio
{
    public partial class Form1 : Form
    {
        string password;
        string login;
        string fullNameUser;
        int ID;
        public Form1()
        {
            InitializeComponent();
            button1.FlatAppearance.BorderColor = Color.Purple;
            panel1.BackColor = Color.Pink;
            panel2.BackColor = Color.Pink;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "admin" && textBox2.Text == "admin")
            {
                CabAdmin cabAdmin = new CabAdmin();
                cabAdmin.Show();
                textBox1.Text = "";
                textBox2.Text = "";
                this.Hide();
            }
            else
            {
                string connectionString = @"Data Source=ADCLG1;Initial Catalog=practica_17;Integrated Security=True"; //строка подключения
                SqlConnection connection = new SqlConnection(connectionString);
                try
                {
                    connection.Open();
                    string selectQuery = "SELECT password, login, full_name, id_client FROM Clients;"; //запрос на получение данных
                    SqlCommand command = new SqlCommand(selectQuery, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        while (true)
                        {
                            fullNameUser = reader.GetString(2);
                            password = reader.GetString(0);
                            login = reader.GetString(1);
                            ID = reader.GetInt32(3);

                            if (login == textBox1.Text && password == textBox2.Text) //проверка обучающихся
                            { 
                                CabClient cabClient = new CabClient();
                                cabClient.ID = ID;
                                cabClient.fullName = fullNameUser;
                                cabClient.Show();
                                this.Hide();
                                textBox1.Text = "";
                                textBox2.Text = "";
                                break;
                            }
                            else if (reader.Read() == false) //если нет строк
                            {
                                reader.Close();
                                selectQuery = "SELECT password, login, full_name, id_teacher FROM Teachers;"; 
                                command = new SqlCommand(selectQuery, connection);
                                reader = command.ExecuteReader();
                                break;
                            }
                        }
                    }
                    if (Application.OpenForms.OfType<CabClient>().Count() == 0)
                    {
                        while (reader.Read())
                        {
                            fullNameUser = reader.GetString(2);
                            password = reader.GetString(0);
                            login = reader.GetString(1);
                            ID = reader.GetInt32(3);

                            if (login == textBox1.Text && password == textBox2.Text) //проверка учителей
                            {
                                CabTeacher cab = new CabTeacher();
                                cab.ID = ID;
                                cab.fullName = fullNameUser;
                                cab.Show();
                                this.Hide();
                                textBox1.Text = "";
                                textBox2.Text = "";
                                break;
                            }
                            else if (reader.Read() == false)
                            {
                                Exception exception = new Exception("Неверный логин или пароль!!!");
                                MessageBox.Show(exception.Message);
                                break;
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0 && textBox2.Text.Length > 0)
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }
    }
}
