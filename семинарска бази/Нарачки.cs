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

using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace семинарска_бази
{
    public partial class Нарачки : Form
    {
        public Нарачки(string user)
        {
            InitializeComponent();
            e_user = user;
        }
        string e_user;
        SqlConnection con = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\\Inventory.mdf; Integrated Security = True; Connect Timeout = 30");

        private void Emploee_Orders_Load(object sender, EventArgs e)
        {
            button2.PerformClick();
            dataGridView1.ReadOnly = true;
        }
        private void reset()
        {
            comboBox1.Text = "";
            comboBox2.Text = "";
            q.Text = "";
        }
        private void comboBox1Fill()
        {
            SqlCommand cmd = new SqlCommand("select ID from Produkti order by ime asc", con);
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBox1.Items.Add(dr["ID"]);
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
        private void button1_Click(object sender, EventArgs e) //INSERT
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            DateTime time = DateTime.Now;     

            cmd.CommandText = "insert into E_orders(productID,supplierID,quantity,date_time) values('" + comboBox1.Text + "','" + comboBox2.Text + "','" + q.Text + "', '" + time.ToString() + "')";
            cmd.ExecuteNonQuery();
            con.Close();
            
            MessageBox.Show("Успешно извршена порачка!");
            reset();
            button2.PerformClick();
        }

        private void button2_Click(object sender, EventArgs e)//POGLED
        {
            con.Open();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;


            cmd.CommandText = "select * from E_orders";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;




            //datum
            object maxdate = dt.Compute("MAX(date_time)", "");
            date.Text = maxdate.ToString();

           // orders count
            order_count.Text = dataGridView1.Rows.Count.ToString();

            //total
            totalPrice();
            

            con.Close();
        }
        private void totalPrice()
        {
           

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT  SUM(E_orders.quantity * Produkti.cena) FROM E_orders join Produkti on Produkti.ID = E_orders.productID";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
           

            orders_amount.Text = dt.Rows[0][0].ToString();

        }
        private void label4_Click(object sender, EventArgs e)
        {

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

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            comboBox1.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["productID"].Value.ToString();
            comboBox2.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["supplierID"].Value.ToString();
            q.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["quantity"].Value.ToString();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Document doc = new Document(iTextSharp.text.PageSize.A4, 10, 10, 42, 35);
            PdfWriter pdf = PdfWriter.GetInstance(doc, new FileStream("Нарачка.pdf", FileMode.Create));
            doc.Open();
            Paragraph p1 = new Paragraph("     NARACATEL: " + e_user);
            doc.Add(p1);

            string ID = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["ID"].Value.ToString();
            string pID = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["productID"].Value.ToString();
            string sID = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["supplierID"].Value.ToString();
            string quantity = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["quantity"].Value.ToString();

          
            Paragraph p2 = new Paragraph("  ID:" + ID + "  produktID:" + pID + "  dobavuvac: " + sID + "  kolicina: " + quantity);

            doc.Add(p2);
            
            doc.Close();
            MessageBox.Show("Успешно го конвертиравте во PDF фајл селектираниот запис!");
        }
    }
}
