using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ImagenLib;

namespace ImagenForm
{
    public partial class FormMosaico : Form
    {
        List<Imagen> listaImagenes = new List<Imagen>();
        Bitmap bmp;
        int imagenesCargadas = 0;

        public FormMosaico()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.Title = "Seleccionar Imagen";
            this.openFileDialog1.Filter = "Imagen (*.ppm;*.png;*.jpg)|*.ppm;*.png;*.jpg";
            this.openFileDialog1.ShowDialog();
        }

        private PictureBox getNextPictureBox()
        {
            if (this.imagenesCargadas == 0)
                return pictureBox1;
            if (this.imagenesCargadas == 1)
                return pictureBox2;
            if (this.imagenesCargadas == 2)
                return pictureBox3;
            if (this.imagenesCargadas == 3)
                return pictureBox4;
            if (this.imagenesCargadas == 4)
                return pictureBox5;
            if (this.imagenesCargadas == 5)
                return pictureBox6;
            if (this.imagenesCargadas == 6)
                return pictureBox7;
            if (this.imagenesCargadas == 7)
                return pictureBox8;
            return pictureBox9;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

            Imagen img;
            int loadResult;
            String url = openFileDialog1.FileName;
            String name = Path.GetFileName(url);
            String extension = Path.GetExtension(url);

            if (extension != ".ppm" && extension != ".png" && extension != ".jpg")
            {
                MessageBox.Show("Error: Extensión inválida.");
                return;
            }

            img = new Imagen(url);
            if (extension == ".ppm")
                loadResult = img.CargarPPM();
            else
                loadResult = img.CargarBMP(url);
            if (loadResult == -1)
            {
                MessageBox.Show("Error: Archivo no encontrado.");
                return;
            }
            else if (loadResult == -2)
            {
                MessageBox.Show("Error: Archivo no encontrado.");
                return;
            }

            this.bmp = img.ConvertirPPMaBMP();
            getNextPictureBox().Image = (Image) bmp;
            getNextPictureBox().SizeMode = PictureBoxSizeMode.StretchImage;
            imagenesCargadas++;

            if (imagenesCargadas != 9)
            {
                label2.Text = "Faltan " + (9 - imagenesCargadas) + " imágenes por seleccionar";
                return;
            }

            this.button1.Enabled = false;
            this.button1.Visible = false;
            this.label2.Visible = false;

            this.botonGuardar.Visible = true;
        }

        private void botonGuardar_Click(object sender, EventArgs e)
        {
            this.saveFileDialog1.Title = "Guardar Mosaico en...";
            this.saveFileDialog1.FileName = "Mosaico.png";
            this.saveFileDialog1.Filter = "Imagen (*.png)|*.png";
            this.saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            // BITMAP DE TODO EL FORM
            Bitmap original = new Bitmap(this.Width, this.Height);
            DrawToBitmap(original, new Rectangle(0, 0, original.Width, original.Height));

            // RECORTAMOS LO QUE NOS INTERESA
            Rectangle srcRect = new Rectangle(17, 95, 560, 360);
            Bitmap cropped = (Bitmap)original.Clone(srcRect, original.PixelFormat);

            // GUARDAMOS
            cropped.Save(saveFileDialog1.FileName, ImageFormat.Png);

            this.Close();
            MessageBox.Show("El mosaico se está guardando en tu PC, este proceso puede tardar unos segundos.");
        }

    }
}
