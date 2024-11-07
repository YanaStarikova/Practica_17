using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicStudio
{
    public partial class CabAdmin : Form
    {
        public CabAdmin()
        {
            InitializeComponent();
        }

        private void roundButton2_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.Show();
            this.Hide();
        }

        private void roundButton3_Click(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();
            form6.Show();
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

        private void CabAdmin_Load(object sender, EventArgs e)
        {
            label2.Text = "Администратор";
        }

        private void roundButton3_Click_1(object sender, EventArgs e)
        {
            Report form = new Report();
            form.Show();
            this.Hide();
        }
    }
}
