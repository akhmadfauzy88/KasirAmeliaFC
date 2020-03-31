using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace KasirAmeliaFC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                //label2.Text = textBox1.Text;
                textBox1.Clear();
                textBox1.Focus();
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            //Cek Koneksi Database
            try
            {
                string cs = @"server=localhost;userid=root;password=VbsMac81;database=kasirameliafc";

                var con = new MySqlConnection(cs);
                con.Open();

                toolStripStatusLabel2.Text = "Online";

                //Console.WriteLine($"MySQL version : {con.ServerVersion}");
            }
            catch (Exception ex)
            {
                toolStripStatusLabel2.Text = "Offline";
                MessageBox.Show(ex.Message);
            }

            textBox1.Clear();
            textBox1.Focus();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Form2().Show();
            this.Hide();
        }
    }
}
