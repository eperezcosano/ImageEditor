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
    public partial class FormIntensidad : Form
    {
        //Atributos
        bool todoCorrecto = false;
        bool aumentar;
        string color;
        int porcentaje;
        
        //Constructor
        public FormIntensidad()
        {
            InitializeComponent();
        }

        //Getters
        public bool GetStatus()
        {
            return this.todoCorrecto;
        }
        public bool GetAumentar()
        {
            return this.aumentar;
        }
        public string GetColor()
        {
            return this.color;
        }
        public int GetPorcentaje()
        {
            return this.porcentaje;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!this.radioButton1.Checked && !this.radioButton2.Checked)
            {
                MessageBox.Show("Seleccione si aumentar o disminuir la intensidad.");
                return;
            }
            if (this.domainUpDown1.Text != "Rojo" && this.domainUpDown1.Text != "Verde" && this.domainUpDown1.Text != "Azul")
            {
                MessageBox.Show("El color seleccionado es inválido.");
                return;
            }
            try 
	        {	        
                Convert.ToInt32(this.textBox1.Text);
	        }
	        catch (Exception)
	        {
		        MessageBox.Show("El porcentaje es inválido.");
                return;
	        }
            if (Convert.ToInt32(this.textBox1.Text) <= 0 || Convert.ToInt32(this.textBox1.Text) > 100)
            {
                MessageBox.Show("El porcentaje debe ser entre 1% y 100%.");
                return;
            }
            if (this.radioButton1.Checked)
                this.aumentar = true;
            if (this.radioButton2.Checked)
                this.aumentar = false;
            this.color = this.domainUpDown1.Text;
            this.porcentaje = Convert.ToInt32(this.textBox1.Text);
            this.todoCorrecto = true;
            this.Close();
        }

    }
}
