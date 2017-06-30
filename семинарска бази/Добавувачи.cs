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
    public partial class Добавувачи : Form
    {
        public Добавувачи()
        {
            InitializeComponent();
            
        }

       
        SqlConnection con = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\\Inventory.mdf; Integrated Security = True; Connect Timeout = 30");
        private void reset()
        {
            textBox1.Text = "";
            name.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)//INSERT
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into Dobavuvaci(ime,email) values('" + name.Text + "','" + textBox1.Text + "')";
            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Успешно внесен запис!");
            reset();
            button4.PerformClick();
        }

        private void button2_Click(object sender, EventArgs e) //DELETE
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from Dobavuvaci where ime = '" + name.Text + "'";
            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Успешно избришан запис");
            reset();
            button4.PerformClick();
        }

        private void button3_Click(object sender, EventArgs e)// UPDATE
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update  Dobavuvaci  set email = '" + textBox1.Text + "'  where   ime = '" + name.Text + "'";
            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("успешно ажуриран запис");
            reset();
            button4.PerformClick();
        }

        private void button4_Click(object sender, EventArgs e) //REFReSH
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Dobavuvaci";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            name.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["ime"].Value.ToString();
            textBox1.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["email"].Value.ToString();
        }

        private void Добавувачи_Load(object sender, EventArgs e)
        {
            button4.PerformClick();
            dataGridView1.ReadOnly = true;
        }
    }
}
