using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MusicStudio
{
    public partial class Report : Form
    {
        public Report()
        {
            InitializeComponent();
        }

        private void roundButton3_Click(object sender, EventArgs e) //назад
        {
            if (Application.OpenForms.OfType<CabAdmin>().Count() > 0)
            {
                Form ifrm = Application.OpenForms[1];
                ifrm.Show();
            }
            this.Close();
        }

        private void roundButton2_Click(object sender, EventArgs e) //выгрузить
        {
            try
            {
                using (var image = new Bitmap(359, 323))
                {
                    using (var gr = Graphics.FromImage(image))
                    {
                        if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                            return;
                        chart1.DrawToBitmap(image, new Rectangle(0, 0, 359, 323));
                        string filename = saveFileDialog1.FileName;
                        filename += ".png";
                        image.Save(filename, ImageFormat.Png);
                    }
                    MessageBox.Show("Файл сохранен");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Report_Load(object sender, EventArgs e) 
        {
            string connectionString = @"Data Source=ADCLG1;Initial Catalog=practica_17;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string selectQuery = $"selecT Services.name, id_clients from Lessons \r\ninner join Appointments on (Appointments.id_appointment = Lessons.id_appointment)\r\ninner join Services on (Services.id_service = Appointments.id_service)\r\n";
            SqlCommand command = new SqlCommand(selectQuery, connection);
            SqlDataReader reader = command.ExecuteReader();

            List<string[]> data = new List<string[]>();

            while (reader.Read())
            {
                data.Add(new string[2]);
                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
            }
            reader.Close();
            connection.Close();
            var serviceCounts = data.GroupBy(item => item[1])
                            .Select(group => new {
                                ServiceName = group.Key,
                                ClientCount = group.Select(item => item[0]).Distinct().Count()
                            }).ToList();

            chart1.Series.Clear();
            Series series = new Series
            {
                Name = "Клиенты",
                ChartType = SeriesChartType.Column
            };

            for (int i = 0; i < serviceCounts.Count; i++)
            {
                series.Points.AddXY(serviceCounts[i].ServiceName, serviceCounts[i].ClientCount);
            }
            chart1.Series.Add(series);
            chart1.ChartAreas[0].AxisX.Title = "Услуги ID:"; // Заголовок оси X
            chart1.ChartAreas[0].AxisX.Interval = 1; // Интервал между метками на оси X
            chart1.ChartAreas[0].RecalculateAxesScale();
        }
    }
}
