using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ImagenLib;

namespace ImagenForm
{
    public partial class FormRecortarImg : Form
    {
        //Atributos
        Bitmap bmp;
        Imagen img;
        Point p1, p2;
        bool recortado;

        //Constructor
        public FormRecortarImg()
        {
            InitializeComponent();
        }
        //Reestablece el tamaño del formulario
        //w: anchura, h: altura.
        public void SetSize(int w, int h)
        {
            if (w < 300)
                w = 260;
            if (h < 250)
                h = 160;
            this.Width = w + 27;
            this.Height = h + 70;
        }
        //Setters y Getters
        public void SetImg(Imagen img)
        {
            this.img = img;
        }
        public Imagen GetImg()
        {
            return this.img;
        }
        public bool GetStatus()
        {
            return this.recortado;
        }
        //Load
        private void FormRecortarImg_Load(object sender, EventArgs e)
        {
            this.pictureBox1.ClientSize = new Size(this.img.GetAncho(), this.img.GetAlto());
            this.bmp = this.img.ConvertirPPMaBMP();
            this.SetSize(this.img.GetAncho(), this.img.GetAlto());
            this.pictureBox1.Image = (Image)this.bmp;
            this.CenterToScreen();
            this.label1.Text = "Haga clic en la imagen donde quiera que sea \n la esquina superior izquierda de la imagen recortada.";
            this.recortado = false;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.p1.IsEmpty == true)
            {
                this.p1 = new Point(e.X, e.Y);
                this.label1.Text = "Haga clic en la imagen donde quiera que sea \n la esquina inferior derecha de la imagen recortada.";
                return;
            }
            this.p2 = new Point(e.X, e.Y);
            if (this.p1 == this.p2 || this.p1.X > this.p2.X || this.p1.Y > this.p2.Y)
            {
                MessageBox.Show("Puntos incorrectos, inténtelo de nuevo.");
                this.label1.Text = "Haga clic en la imagen donde quiera que sea \n la esquina superior izquierda de la imagen recortada.";
                this.p1 = new Point();
                this.p2 = new Point();
                return;
            }
            Pixel[,] tmpImg = new Pixel[this.p2.Y - this.p1.Y, this.p2.X - this.p1.X];
            Pixel[,] orgImg = this.img.GetDatos();
            for (int i = this.p1.Y, y = 0; i < this.p2.Y; i++, y++)
            {
                for (int j = this.p1.X, x = 0; j < this.p2.X; j++, x++)
                {
                    tmpImg[y, x] = new Pixel(orgImg[i, j].GetR(), orgImg[i, j].GetG(), orgImg[i, j].GetB());
                }
            }
            this.img = new Imagen(this.img.GetArchivo());
            this.img.SetIdentificador("P3");
            this.img.SetNiveles(255);
            this.img.SetAlto(this.p2.Y - this.p1.Y);
            this.img.SetAncho(this.p2.X - this.p1.X);
            this.img.SetDatos(tmpImg);
            this.recortado = true;
            this.Close();
        }
    }
}
