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
    public partial class FormVoltear : Form
    {
        int eje = 0;

        public FormVoltear()
        {
            InitializeComponent();
        }

        public int GetStatus()
        {
            return this.eje;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.eje = 1;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.eje = 2;
            this.Close();
        }

        private void FormVoltear_Load(object sender, EventArgs e)
        {

        }
    }
}
