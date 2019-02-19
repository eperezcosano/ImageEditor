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
    public partial class FormContenidoDB : Form
    {
        public FormContenidoDB()
        {
            InitializeComponent();
        }

        private void FormContenidoDB_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'imagenDBDataSet.Registro' table. You can move, or remove it, as needed.
            this.registroTableAdapter.Fill(this.imagenDBDataSet.Registro);
            // TODO: This line of code loads data into the 'imagenDBDataSet.Registro' table. You can move, or remove it, as needed.
            this.registroTableAdapter.Fill(this.imagenDBDataSet.Registro);

        }
    }
}
