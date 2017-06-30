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
    public partial class Продукти : Form
    {
        public Продукти()
        {
            InitializeComponent();
            
        }
        SqlConnection con = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\\Inventory.mdf; Integrated Security = True; Connect Timeout = 30");

        private void comboBox1Fill()
        {
            SqlCommand cmd = new SqlCommand("select ime from Kategorii order by ime asc", con);
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while(dr.Read())
                {
                    comboBox1.Items.Add(dr["ime"]);
                }

                con.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

           

        }
        private void comboBox2Fill()
        {
            SqlCommand cmd = new SqlCommand("select ime from Dobavuvaci order by ime asc", con);
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBox2.Items.Add(dr["ime"]);
                }

                con.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }



        }
        

        private void reset()
        {
            name.Text = "";
            textBox1.Text = "";           
            textBox3.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
        }
        private void Products_Load(object sender, EventArgs e)
        {
            button4.PerformClick();
            dataGridView1.ReadOnly = true;
        }

        private void button1_Click(object sender, EventArgs e)//ВНЕС
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            
            cmd.CommandText = "insert into Produkti(ime,opis,kategorija,cena,dobavuvac) values('" + name.Text + "','" + textBox1.Text + "','" + comboBox1.Text + "','" + textBox3.Text + "','" + comboBox2.Text + "')";
            cmd.ExecuteNonQuery();
            con.Close();

            reset();

            MessageBox.Show("Успешно внесен запис!");
            button4.PerformClick();
        }


        private void button4_Click(object sender, EventArgs e) //ПОГЛЕД
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Produkti";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;


            cheapestProduct();
            expensiveProduct();

            //products sum
            psum.Text = dataGridView1.Rows.Count.ToString();

            con.Close();
        

        }
        private void cheapestProduct()
        {
            SqlCommand cmd = new SqlCommand("naeftinProdukt", con);
            cmd.CommandType = CommandType.StoredProcedure;

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            cheapest.Text = dt.Rows[0][0].ToString();
        }
        private void expensiveProduct()
        {
            SqlCommand cmd = new SqlCommand("najskapProdukt", con);
            cmd.CommandType = CommandType.StoredProcedure;

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            expensive.Text = dt.Rows[0][0].ToString();
        }
        private bool set()
        {
            if (name.Text != "" && textBox1.Text != "" && textBox3.Text != "" && comboBox1.Text != "" && comboBox2.Text != "")
                return true;
            return false;
            
        }
        private void button2_Click(object sender, EventArgs e) // БРИШИ
        {
            if (set())
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;


                cmd.CommandText = "delete from Produkti where ime = '" + name.Text + "'and opis = '" + textBox1.Text + "' and cena = '" + textBox3.Text + "' ";
                cmd.ExecuteNonQuery();
                MessageBox.Show("Успешно извришан запис!");
                con.Close();

                reset();
                button4.PerformClick();
            }
            else
                MessageBox.Show("Проблем со бришење! Проверете ги внесените вредности!");
            
        }
        
        private void button3_Click(object sender, EventArgs e) // АЖУРИРАЈ
        {
            con.Open();
            
            SqlCommand cmd  = new SqlCommand ("update Produkti set opis = @p2,kategorija = @p3,cena = @p4,dobavuvac = @p5 where ime = @p1",con);
            cmd.Parameters.AddWithValue("p1", name.Text);
            cmd.Parameters.AddWithValue("p2", textBox1.Text);
            cmd.Parameters.AddWithValue("p3", comboBox1.Text);
            cmd.Parameters.AddWithValue("p4", textBox3.Text);
            cmd.Parameters.AddWithValue("p5", comboBox2.Text);


            cmd.ExecuteNonQuery();
            con.Close();

            reset();
            MessageBox.Show("Успешно ажуриран запис!");
            button4.PerformClick();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ПребарувањеПродукти child = new ПребарувањеПродукти();
            child.Show();
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            name.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["ime"].Value.ToString();
            textBox1.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["opis"].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["kategorija"].Value.ToString();
            textBox3.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["cena"].Value.ToString();
            comboBox2.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["dobavuvac"].Value.ToString();
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1Fill();
        }

        private void comboBox2_Click(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            comboBox2Fill();

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            reset();
        }
    }
}
