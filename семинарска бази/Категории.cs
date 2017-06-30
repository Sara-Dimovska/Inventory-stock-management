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

namespace семинарска_бази
{
    public partial class Категории : Form
    {
        public Категории()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\\Inventory.mdf; Integrated Security = True; Connect Timeout = 30");

        private void button1_Click(object sender, EventArgs e) //INSERT
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into Kategorii(ime) values ('"+name.Text+"')";
            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Успешно внесен запис!");
            name.Text = "";
            button4.PerformClick();
        }

        private void button2_Click(object sender, EventArgs e) //DELETE
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from Kategorii where ime = '" + name.Text + "'";
            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Успешно избришан запис");
            name.Text = "";
            button4.PerformClick();
        }

        private void button4_Click(object sender, EventArgs e) //VIEW
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Kategorii";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
            label3.Text = dataGridView1.Rows.Count.ToString();
        }

        private void Categories_Load(object sender, EventArgs e)
        {
            button4.PerformClick();
            dataGridView1.ReadOnly = true;
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            name.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["ime"].Value.ToString();
        }

       
    }
}
