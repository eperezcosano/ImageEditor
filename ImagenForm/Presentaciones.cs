using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ImagenLib;

namespace ImagenForm
{
    public partial class Presentaciones : Form
    {
        String[] vector = new String[50];

        Bitmap bmp;
        Imagen img;
        string fname;
        string extension;
        int r;
        int rr;
        int rrr;
        int i = 0;
        int k;
        int e = 0;

        public Presentaciones()
        {
            InitializeComponent();

            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;

        }

        private void cargarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Referencia refe = new Referencia();
            refe.ShowDialog();

            this.r = refe.res(); // que opcion se ha elegido (r=0 manual)(r!=0 automatico)
            this.rr = refe.res2();//tiempo entre fotos
            this.rrr = refe.res3(); //openfiledialog if press buttom acept

            if (rrr == 1)
            {
                this.openFileDialog2.Title = "Seleccionar Imagen";
                this.openFileDialog2.Filter = "Imagen (*.ppm;*.png;*.jpg)|*.ppm;*.png;*.jpg";
                this.openFileDialog2.Multiselect = true;
                this.openFileDialog2.ShowDialog();
            }
        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            int y = openFileDialog2.FileNames.Count();
            this.k = openFileDialog2.FileNames.Count();

            for (int x = 0; x < y; x++)
            {
                this.fname = openFileDialog2.FileNames[x];
                this.vector[x] = this.fname;
                this.extension = Path.GetExtension(this.fname);

                if (this.extension != ".ppm" && this.extension != ".png" && this.extension != ".jpg")
                {
                    MessageBox.Show("Error: Extensión inválida.");
                }
            }

            if (this.r == 0)
            {
                button5.Enabled = true;

                string first = this.vector[0];
                string ex = Path.GetExtension(first);

                int res;
                this.pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                this.img = new Imagen(first);

                if (ex == ".ppm")
                    res = this.img.CargarPPM();
                else
                    res = this.img.CargarBMP(first);

                if (res != 0)
                {
                    if (res == -1)
                        MessageBox.Show("Error: Archivo no encontrado.");
                    if (res == -2)
                        MessageBox.Show("Error: Formato inválido.");
                    return;
                }

                this.bmp = this.img.ConvertirPPMaBMP();
                this.pictureBox2.Padding = new System.Windows.Forms.Padding(15);
                this.pictureBox2.Image = (Image)bmp;
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                button1.Enabled = true;
                button2.Enabled = false;
                button3.Enabled = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Siguiente
            this.e = this.e + 1;

            if (this.e > this.k - 1)
            {
                MessageBox.Show("No hay mas imagenes por visualizar");
                button5.Enabled = false;
                button4.Enabled = true;
                this.e = this.e - 1;
            }
            else
            {
                button4.Enabled = true;
                string first = this.vector[this.e];
                string ex = Path.GetExtension(first);

                int res;
                this.pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                this.img = new Imagen(first);

                if (ex == ".ppm")
                    res = this.img.CargarPPM();
                else
                    res = this.img.CargarBMP(first);

                if (res != 0)
                {
                    if (res == -1)
                        MessageBox.Show("Error: Archivo no encontrado.");
                    if (res == -2)
                        MessageBox.Show("Error: Formato inválido.");
                    return;
                }

                this.bmp = this.img.ConvertirPPMaBMP();
                this.pictureBox2.Padding = new System.Windows.Forms.Padding(15);
                this.pictureBox2.Image = (Image)bmp;
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

                button4.Enabled = true;
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.e = this.e - 1;

            if (this.e < 0)
            {
                MessageBox.Show("No hay mas imagenes anteriores por visualizar");
                button4.Enabled = false;
                button5.Enabled = true;
                this.e = this.e + 1;
            }
            else
            {
                string first = this.vector[this.e];
                string ex = Path.GetExtension(first);

                int res;
                this.pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                this.img = new Imagen(first);

                if (ex == ".ppm")
                    res = this.img.CargarPPM();
                else
                    res = this.img.CargarBMP(first);

                if (res != 0)
                {
                    if (res == -1)
                        MessageBox.Show("Error: Archivo no encontrado.");
                    if (res == -2)
                        MessageBox.Show("Error: Formato inválido.");
                    return;
                }

                this.bmp = this.img.ConvertirPPMaBMP();
                this.pictureBox2.Padding = new System.Windows.Forms.Padding(15);
                this.pictureBox2.Image = (Image)bmp;
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                button5.Enabled = true;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Play
            string first = this.vector[this.i];
            string ex = Path.GetExtension(first);

            int res;
            this.pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            this.img = new Imagen(first);

            if (ex == ".ppm")
                res = this.img.CargarPPM();
            else
                res = this.img.CargarBMP(first);

            if (res != 0)
            {
                if (res == -1)
                    MessageBox.Show("Error: Archivo no encontrado.");
                if (res == -2)
                    MessageBox.Show("Error: Formato inválido.");
                return;
            }

            this.bmp = this.img.ConvertirPPMaBMP();
           this.pictureBox2.Padding = new System.Windows.Forms.Padding(15);
            this.pictureBox2.Image = (Image)bmp;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

            button2.Enabled = true;
            button3.Enabled = true;
            timer1.Interval = this.rr * 1000; ;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.i = this.i + 1;

            if (this.i > this.k - 1)
            {
                timer1.Stop();
                timer1.Enabled = false;
                MessageBox.Show("No hay mas imagenes por visualizar");
                button1.Enabled = false;
                button3.Enabled = true;
                button2.Enabled = false;
            }
            else 
            {
                string first = this.vector[this.i];
                string ex = Path.GetExtension(first);

                int res;
                this.pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                this.img = new Imagen(first);

                if (ex == ".ppm")
                    res = this.img.CargarPPM();
                else
                    res = this.img.CargarBMP(first);

                if (res != 0)
                {
                    if (res == -1)
                        MessageBox.Show("Error: Archivo no encontrado.");
                    if (res == -2)
                        MessageBox.Show("Error: Formato inválido.");
                    return;
                }

                this.bmp = this.img.ConvertirPPMaBMP();
                this.pictureBox2.Padding = new System.Windows.Forms.Padding(15);
                this.pictureBox2.Image = (Image)bmp;
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            }
             
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = true;
            this.i = 0;
            string first = this.vector[this.i];
            string ex = Path.GetExtension(first);

            int res;
            this.pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            this.img = new Imagen(first);

            if (ex == ".ppm")
                res = this.img.CargarPPM();
            else
                res = this.img.CargarBMP(first);

            if (res != 0)
            {
                if (res == -1)
                    MessageBox.Show("Error: Archivo no encontrado.");
                if (res == -2)
                    MessageBox.Show("Error: Formato inválido.");
                return;
            }

            this.bmp = this.img.ConvertirPPMaBMP();
            this.pictureBox2.Padding = new System.Windows.Forms.Padding(15);
            this.pictureBox2.Image = (Image)bmp;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

            timer1.Stop();
            timer1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1.Enabled = false;
            button3.Enabled = true;
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
