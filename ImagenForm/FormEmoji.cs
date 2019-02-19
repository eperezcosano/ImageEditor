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
    public partial class FormEmoji : Form
    {
        //Atributos
        Bitmap bmp;
        Imagen img;
        Imagen emojiImg = new Imagen("emoji1.png");
        Bitmap emojiBMP;
        Point punto;
        bool hecho;

        public FormEmoji()
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
            return this.hecho;
        }

        private void FormEmoji_Load(object sender, EventArgs e)
        {
            this.pictureBox1.ClientSize = new Size(this.img.GetAncho(), this.img.GetAlto());
            this.bmp = this.img.ConvertirPPMaBMP();
            this.SetSize(this.img.GetAncho() + 50, this.img.GetAlto() + 50);
            this.pictureBox1.Image = (Image)this.bmp;
            this.CenterToScreen();
            this.label1.Text = "Haga clic en la imagen donde quiera que haya el emoji";
            emojiImg.CargarBMP("emoji1.png");
            emojiBMP = emojiImg.ConvertirPPMaBMP();
            this.pictureBox1.Image = (Image)this.bmp;
            this.hecho = false;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            this.punto = new Point(e.X, e.Y);
            if (this.punto.X - 56 < 0 || this.punto.X + 56 > this.img.GetAncho() || this.punto.Y - 56 < 0 || this.punto.Y + 56 > this.img.GetAlto())
            {
                MessageBox.Show("El emoji no cabe en la posición seleccionada, intente en otro lugar.");
                this.label1.Text = "Haga clic en la imagen donde quiera que sea \n la esquina superior izquierda de la imagen recortada.";
                this.punto = new Point();
                return;
            }
            Pixel[,] tmpImg = this.img.GetDatos();
            Pixel[,] emojiMatrix = this.emojiImg.GetDatos();
            for (int y = 0, i = 0; y < this.img.GetAlto(); y++)
            {
                for (int x = 0, j = 0; x < this.img.GetAncho(); x++)
                {
                    if (y > this.punto.Y - 56 && y < this.punto.Y + 56 && x > this.punto.X - 56 && x < this.punto.X + 56)
                    {
                        if (emojiMatrix[i, j].GetR() != 255 || emojiMatrix[i, j].GetG() != 255 || emojiMatrix[i, j].GetB() != 255 )
                        {
                            tmpImg[y, x].SetR(emojiMatrix[i, j].GetR());
                            tmpImg[y, x].SetG(emojiMatrix[i, j].GetG());
                            tmpImg[y, x].SetB(emojiMatrix[i, j].GetB());
                        }
                        j++;
                        if (j >= 112)
                            j = 0;
                    }
                }
                if (y > this.punto.Y - 56 && y < this.punto.Y + 56)
                {
                    i++;
                    if (i >= 112)
                        i = 0;
                }
            }
            this.img.SetDatos(tmpImg);
            this.hecho = true;
            this.Close();
        }
    }
}
