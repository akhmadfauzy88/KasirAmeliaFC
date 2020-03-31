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
                //String par;
                //label2.Text = textBox1.Text;
                try
                {
                    string cs = @"server=localhost;userid=root;password=VbsMac81;database=kasirameliafc";

                    var con = new MySqlConnection(cs);
                    var con1 = new MySqlConnection(cs);
                    con.Open();
                    con1.Open();

                    MySqlCommand check_barang = new MySqlCommand("SELECT * FROM cart WHERE (barang = @barang)", con);
                    check_barang.Parameters.AddWithValue("@barang", textBox1.Text);
                    MySqlDataReader reader = check_barang.ExecuteReader();
                    if (reader.HasRows)
                    {
                        //KMessageBox.Show("aw");
                        var sql = "UPDATE cart SET jumlah=jumlah+1 WHERE barang=@barc";
                        var cmd = new MySqlCommand(sql, con1);
                        cmd.Parameters.AddWithValue("@barc", textBox1.Text);
                        cmd.Prepare();
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        //MessageBox.Show("ew");
                        var sql = "INSERT INTO cart(Barang, Jumlah) VALUES(@barc, @jum)";
                        var cmd = new MySqlCommand(sql, con1);
                        cmd.Parameters.AddWithValue("@barc", textBox1.Text);
                        cmd.Parameters.AddWithValue("@jum", 1);
                        cmd.Prepare();
                        cmd.ExecuteNonQuery();
                    }

                    //var sql = string.Format("select * from listbarang where barcode='{0}'", textBox1.Text);
                    //var cmd = new MySqlCommand(sql, con);
                    //MySqlDataReader rdr = cmd.ExecuteReader();
                    //Console.WriteLine($"{rdr.GetName(0),-4} {rdr.GetName(1),-10} {rdr.GetName(2),10}");
                    //while (rdr.Read())
                    //{
                    //    int fl = 0;
                    //    Console.WriteLine(fl);
                    //    Console.WriteLine($"{rdr.GetString(0),-4} {rdr.GetString(1),-10} {rdr.GetString(2),10}");
                    //    fl++;
                    //}

                    DataTable dt = new DataTable();
                    MySqlDataAdapter adapt = new MySqlDataAdapter("select cart.id, listbarang.nama, listbarang.harga, cart.jumlah, (cart.jumlah * listbarang.harga) as Total from cart join listbarang on cart.barang = listbarang.barcode order by cart.id asc", con1);
                    adapt.Fill(dt);
                    dataGridView1.DataSource = dt;

                    con.Close();
                    con1.Close();
                    //Console.WriteLine($"MySQL version : {con.ServerVersion}");
                }
                catch (Exception ex)
                {
                    //toolStripStatusLabel2.Text = "Offline";
                    MessageBox.Show(ex.Message);
                }
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

                con.Close();
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
