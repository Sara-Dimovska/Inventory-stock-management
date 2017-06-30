using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace семинарска_бази
{
    public partial class КорисничкаНајава : Form
    {
        public КорисничкаНајава()
        {
            InitializeComponent();
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Регистрација ob = new Регистрација();
            ob.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           

            SqlConnection con = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\\Inventory.mdf; Integrated Security = True; Connect Timeout = 30");
            con.Open();
         
            string cmd = "select username from  E_Login where username = '" + textBox1.Text + "' and pasword = '" + textBox2.Text + "' ";
            SqlDataAdapter da = new SqlDataAdapter(cmd, con);
            DataSet ds = new DataSet();
            da.Fill(ds);

            DataTable dt = ds.Tables[0];

            if (dt.Rows.Count >= 1)
            {
                Почетна hp = new Почетна(textBox1.Text);
                hp.Show();
                this.Hide();
            }
            else
                label4.Text = "Невалидно име или лозинка!";


            con.Close();
           
  
            
        }

       
    }
}
