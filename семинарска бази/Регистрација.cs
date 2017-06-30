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

namespace семинарска_бази
{
    public partial class Регистрација : Form
    {
        public Регистрација()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dostapno.Text = "";
            match.Text = "";



            if (username.Text == "")
                dostapno.Text = "Внеси корисничко име!";

            if (textBox2.Text == "" || textBox3.Text == "")
                match.Text = "Внеси лозинка!";


            //не се совпаѓаат
            else if (textBox2.Text != textBox3.Text)
            {
                match.Text = "Лозинките не се совпаѓаат!";
                textBox2.Text = "";
                textBox3.Text = "";
            }

            //се совпаѓаат
            else if (textBox2.Text == textBox3.Text && username.Text != "")
            {

                SqlConnection con = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\\Inventory.mdf; Integrated Security = True; Connect Timeout = 30");
                con.Open();


                SqlCommand cmd = new SqlCommand("select count(*) from E_Login where username = '" + username.Text + "'", con);

                bool exists = false;

                cmd.Parameters.AddWithValue("username", username.Text);
                exists = (int)cmd.ExecuteScalar() > 0;


                // postoi korisnickoto ime
                if (exists)
                {
                    dostapno.Text = "Ова корисничко име е зафатено!";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    username.Text = "";                  
                }
                    

                else if (!exists)
                {
                    cmd.CommandText = "insert into E_Login (username,pasword) values('" + username.Text + "','" + textBox2.Text + "')";
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Вие сте регистрирани !");
                    this.Close();
                    КорисничкаНајава ob = new КорисничкаНајава();
                    ob.Show();

                }

                con.Close();

            }


            }
            
            }

      
    }
       
  

