using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImagenForm
{
    public partial class Referencia : Form
    {

        int i;
        int t;
        int c = 0;

        public int res() 
        {
            return i;
        }

        public int res2() 
        {
            return t;
        }

        public int res3() 
        {
            return c;
        }


        public Referencia()
        {
            InitializeComponent();

            label2.Enabled = false;
            textBox1.Enabled = false;
            label3.Enabled = false;
            button3.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label2.Enabled = false;
            textBox1.Enabled = false;
            label3.Enabled = false;
            button2.Enabled = true;
            button3.Enabled = true;
            int z = this.i * 0;
            this.i = z;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label2.Enabled = true;
            textBox1.Enabled = true;
            label3.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            int y = this.i + 1;
            this.i = y;
        }

        

        private void button3_Click(object sender, EventArgs e)
        {
            if (i != 0)
            {
                try
                {
                    this.t = Convert.ToInt32(this.textBox1.Text);
                    this.c = 1;
                    Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Dato introducido invalido");
                    return;
                }
            }
            else 
            {
                this.c = 1;
                Close();
            }
            
           
        }

       
       

      
       

   
    }
}
