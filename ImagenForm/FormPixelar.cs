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
    public partial class FormPixelar : Form
    {
        public FormPixelar()
        {
            InitializeComponent();
        }

        Boolean correcto = false;
        private int imagAlto;
        private int imagAncho;

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox.Checked)
            {
                inputX.Enabled = false;
                inputY.Enabled = false;
                inputX2.Enabled = false;
                inputY2.Enabled = false;
            }
            else
            {
                inputX.Enabled = true;
                inputY.Enabled = true;
                inputX2.Enabled = true;
                inputY2.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (!checkBox.Checked)
            {
                try
                {
                    int x1 = Convert.ToInt32(this.inputX.Text);
                    int y1 = Convert.ToInt32(this.inputY.Text);
                    int x2 = Convert.ToInt32(this.inputX2.Text);
                    int y2 = Convert.ToInt32(this.inputY2.Text);

                    if (x1 < 1 || y1 < 1 || x2 < -1 || y2 < 1)
                    {
                        MessageBox.Show("Los valores introducidos son demasiado pequeños.");
                        return;
                    }

                    if (x1 >= this.imagAncho || y1 >= this.imagAlto || x2 >= this.imagAncho || y2 >= this.imagAlto)
                    {
                        MessageBox.Show("Los valores introducidos son demasiado altos.");
                        return;
                    }

                    if (x2 <= x1 || y2 <= y1)
                    {
                        MessageBox.Show("La posiciones finales tienen que ser más grandes que las iniciales.");
                        return;
                    }

                }
                catch (Exception)
                {
                    MessageBox.Show("Los parámetros introducidos tienen que ser números enteros.");
                    return;
                }
            }

            correcto = true;
            this.Close();
        }

        public Boolean isAllImage()
        {
            return this.checkBox.Checked;
        }

        public Boolean isCorrect()
        {
            return this.correcto;
        }

        public int getBar()
        {
            return bar.Value;
        }

        public int getStartX()
        {
            return Convert.ToInt32(this.inputX.Text);
        }

        public int getStartY()
        {
            return Convert.ToInt32(this.inputY.Text);
        }

        public int getFinalX()
        {
            return Convert.ToInt32(this.inputX2.Text);
        }

        public int getFinalY()
        {
            return Convert.ToInt32(this.inputY2.Text);
        }

        public void setImgAncho(int i)
        {
            this.imagAncho = i;
        }

        public void setImgAlto(int i)
        {
            this.imagAlto = i;
        }
    }
}
