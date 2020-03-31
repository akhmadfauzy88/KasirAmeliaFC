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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            new Form1().Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string cs = @"server=localhost;userid=root;password=VbsMac81;database=kasirameliafc";

                var con = new MySqlConnection(cs);
                con.Open();

                //toolStripStatusLabel2.Text = "Online";
                var sql = "INSERT INTO listbarang(Barcode, Nama, Harga) VALUES(@barc, @name, @val)";
                var cmd = new MySqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@barc", textBox1.Text);
                cmd.Parameters.AddWithValue("@name", textBox2.Text);
                cmd.Parameters.AddWithValue("@val", textBox3.Text);
                cmd.Prepare();

                cmd.ExecuteNonQuery();

                con.Close();
                //Console.WriteLine($"MySQL version : {con.ServerVersion}");
            }
            catch (Exception ex)
            {
                //toolStripStatusLabel2.Text = "Offline";
                MessageBox.Show(ex.Message);
            }
        }
    }
}
