using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace семинарска_бази
{
    public partial class Почетна : Form
    {
        public Почетна(string name)
        {         
            InitializeComponent();
            label1.Text += " " + name + "!";
            user = name;
        }
        string user;

        private void suppliersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Добавувачи))
                {
                    form.Activate();
                    return;
                }
            }
            Добавувачи child = new Добавувачи();
            child.MdiParent = this;
            child.Show();
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Продукти))
                {
                    form.Activate();
                    return;
                }
            }
            Продукти child = new Продукти();
            child.MdiParent = this;
            child.Show();
        }

        private void orderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Нарачки))
                {
                    form.Activate();
                    return;
                }
            }

            Нарачки child = new Нарачки(user);
            child.MdiParent = this;
            child.Show();
        }

        private void categoriesToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Категории))
                {
                    form.Activate();
                    return;
                }
            }
            Категории child = new Категории();
            child.MdiParent = this;
            child.Show();
        }

        private void Homepage_Load(object sender, EventArgs e)
        {
            menuStrip1.ForeColor = Color.White;           
        }

        private void одјавиСеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            КорисничкаНајава child = new КорисничкаНајава();
            this.Close();
            child.Show();
        }
    }
}
