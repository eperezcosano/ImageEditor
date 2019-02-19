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
    public partial class FormMarco : Form
    {
        //Atributos
        bool todoOk = false;
        int grosor;
        Color color;

        //Constructor
        public FormMarco()
        {
            InitializeComponent();
        }

        //Getters
        public bool GetStatus()
        {
            return this.todoOk;
        }
        public int GetGrosor()
        {
            return this.grosor;
        }
        public Color GetColor()
        {
            return this.color;
        }

        //Seleccionar color
        private void button2_Click(object sender, EventArgs e)
        {
            this.colorDialog1.ShowDialog();
            this.button2.BackColor = this.colorDialog1.Color;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try 
	        {	        
                Convert.ToInt32(this.textBox1.Text);
	        }
	        catch (Exception)
	        {
                MessageBox.Show("Grosor inválido");
                return;
	        }
            if (Convert.ToInt32(this.textBox1.Text) <= 0)
            {
                MessageBox.Show("Grosor inválido");
                return;
            }
            this.grosor = Convert.ToInt32(this.textBox1.Text);
            this.color = this.button2.BackColor;
            this.todoOk = true;
            this.Close();
        }




    }
}
