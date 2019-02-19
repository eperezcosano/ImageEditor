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
    public partial class FormGuardar : Form
    {
        int guardar = 0;

        public FormGuardar()
        {
            InitializeComponent();
        }

        //Devuelve la elección escogida.
        //0: No ha escogido nada; 1: Guardar y salir; 2: Descartar y salir.
        public int GetStatus()
        {
            return this.guardar;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.guardar = 1;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.guardar = 2;
            this.Close();
        }
    }
}
