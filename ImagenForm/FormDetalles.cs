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
    public partial class FormDetalles : Form
    {
        string nombre;
        int ancho;
        int alto;

        public FormDetalles()
        {
            InitializeComponent();
        }

        public void SetDetalles(string nombre, int ancho, int alto)
        {
            this.nombre = nombre;
            this.ancho = ancho;
            this.alto = alto;
        }

        private void FormDetalles_Load(object sender, EventArgs e)
        {
            label3.Text = this.nombre;
            label4.Text = this.ancho + " x " + this.alto + " píxeles";
        }
    }
}
