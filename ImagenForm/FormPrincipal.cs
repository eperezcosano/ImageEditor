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
    public partial class FormPrincipal : Form
    {
        const int MAX = 100;
        public const string rutaDB = "..\\..\\..\\ImagenDataBase\\ImagenDB.sdf";

        //Atributos
        DBGestion db;
        Bitmap bmp;
        Imagen img;
        Deshacer pila = new Deshacer();
        string fname;
        string nombre;
        string extension;
        int[] vector = new int[MAX];
        bool exit = false;

        //Constructor
        public FormPrincipal()
        {
            InitializeComponent();
            //Desactivar algunos componentes del menu ja que no hay imagen cargada.
            this.guardarToolStripMenuItem.Enabled = false;
            this.guardarComoToolStripMenuItem.Enabled = false;
            this.editarToolStripMenuItem.Enabled = false;
            this.deshacerToolStripMenuItem.Enabled = false;
            this.detallesImagenToolStripMenuItem.Enabled = false;

            //Iniciar conexión con la base de datos.
            this.db = new DBGestion(rutaDB);
            int res = this.db.openDB();
            if (res != 0)
            {
                MessageBox.Show("No se ha podido establecer una conexión con la base de datos porque no ha podido ser encontrada.");
                System.Environment.Exit(-1);
            }
        }

        //En cargar, abrimos un OpenFileDialog con unos filtros los cuales solo
        //se pueden seleccionar un determinado tipo de imagen.
        private void cargarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.Title = "Seleccionar Imagen";
            this.openFileDialog1.Filter = "Imagen (*.ppm;*.png;*.jpg)|*.ppm;*.png;*.jpg";
            this.openFileDialog1.ShowDialog();
        }

        //Al haber seleccionado la imagen, la cargamos en memoria y la ponemos en el PictureBox.
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            int res;
            this.fname = openFileDialog1.FileName;
            this.nombre = Path.GetFileName(this.fname);
            this.extension = Path.GetExtension(this.fname);

            if (this.extension != ".ppm" && this.extension != ".png" && this.extension != ".jpg")
            {
                MessageBox.Show("Error: Extensión inválida.");
                return;
            }
            this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            this.img = new Imagen(this.fname);
            if (this.extension == ".ppm")
                res = this.img.CargarPPM();
            else
                res = this.img.CargarBMP(this.fname);
            if (res != 0)
            {
                if (res == -1)
                    MessageBox.Show("Error: Archivo no encontrado.");
                if (res == -2)
                    MessageBox.Show("Error: Formato inválido.");
                return;
            }

            this.bmp = this.img.ConvertirPPMaBMP();
            this.pictureBox1.ClientSize = new Size(this.img.GetAncho(), this.img.GetAlto());
            this.pictureBox1.Image = (Image)bmp;
            
            this.Width = this.img.GetAncho() + 50;
            this.Height = this.img.GetAlto() + 70;

            this.guardarToolStripMenuItem.Enabled = true;
            this.guardarComoToolStripMenuItem.Enabled = true;
            this.editarToolStripMenuItem.Enabled = true;
            this.detallesImagenToolStripMenuItem.Enabled = true;
            this.CenterToScreen();
        }

        //Si guardamos, aparte de sobreescribir los cambios en el disco duro, actualizamos la base de datos.
        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.img == null)
            {
                MessageBox.Show("Error: Imposible guardar, imagen no cargada.");
                return;
            }
            int res1 = this.db.ComprobarImagen(this.nombre);
            if (res1 == 0)
            {
                int res2 = this.db.ModificarImagen(this.nombre, this.img.GetAlto(), this.img.GetAncho());
                if (res2 != 0)
                    MessageBox.Show("Base de Datos Error: No se puede actualizar los datos.");
            }
            else
            {
                int res2 = this.db.AñadirImagen(this.nombre, this.img.GetAlto(), this.img.GetAncho());
                if (res2 != 0)
                    MessageBox.Show("Base de Datos Error: No se puede añadir los datos.");
            }
            if (this.extension == ".ppm")
                res1 = this.img.GuardarPPM(this.fname);
            else
                res1 = this.img.GuardarBMP(bmp, this.fname);
            if (res1 == -2)
                MessageBox.Show("Error: Imposible guardar, imagen no cargada.");
            if (res1 == -1)
                MessageBox.Show("Error: Archivo no encontrado.");
            if (res1 == 0)
            {
                MessageBox.Show("Archivo guardado correctamente.");
                this.pila.Vaciar();
                this.deshacerToolStripMenuItem.Enabled = false;
            }
        }

        //Si elegimos guardar como, se abre un SaveFileDialog. Podemos cambiar la ruta, el nombre y la extension.
        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.img == null)
            {
                MessageBox.Show("Error: Imposible guardar, imagen no cargada.");
                return;
            }
            this.saveFileDialog1.Title = "Guardar Como...";
            this.saveFileDialog1.FileName = this.nombre;
            this.saveFileDialog1.Filter = "Imagen (*.jpg)|*.jpg|Imagen (*.png)|*.png|Imagen (*.ppm)|*.ppm";
            this.saveFileDialog1.ShowDialog();
        }

        //Al guardar como, podremos haber cambiado la ruta y el nombre de la imagen. Y actualizamos memoria.
        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            this.fname = this.saveFileDialog1.FileName;
            this.nombre = Path.GetFileName(this.fname);
            this.extension = Path.GetExtension(this.fname);
            this.guardarToolStripMenuItem.PerformClick();
        }

        //Al salir, preguntar guardar si se ha hecho algún cambio.
        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.pila.GetNumero() != 0)
            {
                FormGuardar guardar = new FormGuardar();
                guardar.ShowDialog();
                if (guardar.GetStatus() == 0)
                    return;
                if (guardar.GetStatus() == 1)
                    this.guardarToolStripMenuItem.PerformClick();
            }
            this.exit = true;
            this.db.closeDB();
            this.Close();
        }

        //Al voltear primero se abre un formulario para escoger el eje, 
        //luego se voltea en memoria y lo ponemos en PictureBox
        private void voltearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.img == null)
            {
                MessageBox.Show("Error: Imagen no cargada.");
                return;
            }

            FormVoltear voltear = new FormVoltear();
            voltear.ShowDialog();
            if (voltear.GetStatus() == 0)
                return;
            this.pila.Apilar(this.img);
            if (voltear.GetStatus() == 1)
                this.img.VoltearV();
            if (voltear.GetStatus() == 2)
                this.img.VoltearH();
            this.bmp = this.img.ConvertirPPMaBMP();
            this.pictureBox1.Image = (Image)this.bmp;
            this.deshacerToolStripMenuItem.Enabled = true;
        }

        //Muestra el nombre y el tamaño de la imagen cargada en un nuevo formulario.
        private void detallesImagenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDetalles detalles = new FormDetalles();
            detalles.SetDetalles(this.nombre, this.img.GetAncho(), this.img.GetAlto());
            detalles.ShowDialog();
        }

        //Muestra la tabla de registros de la base de datos en un nuevo formulario.
        private void baseDeDatosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormContenidoDB registro = new FormContenidoDB();
            registro.ShowDialog();
        }

        //Muestra informacion acerca del proyecto.
        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAcercaDe acercaDe = new FormAcercaDe();
            acercaDe.ShowDialog();
        }

        //Si acaso se sale de otra manera que no sea haciando clic en salir
        //Entonces llamamos la funcion de salir.
        private void FormPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (exit == false)
            {
                this.salirToolStripMenuItem.PerformClick();
                e.Cancel = true;
            }
        }

        //Vuelve a la edicion anterior de la imagen
        private void deshacerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.pila.GetNumero() == 0)
            {
                MessageBox.Show("No hay cambios a deshacer.");
                this.deshacerToolStripMenuItem.Enabled = false;
                return;
            }

            this.img = this.pila.GetImagen();
            this.bmp = this.img.ConvertirPPMaBMP();
            this.pictureBox1.Width = img.GetAncho();
            this.pictureBox1.Height = img.GetAlto();
            this.pictureBox1.Image = (Image)this.bmp;
            if (this.pila.GetNumero() == 0)
                this.deshacerToolStripMenuItem.Enabled = false;
        }

        //Convierte la imagen a una escala de grises
        private void escalaDeGrisesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.img == null)
            {
                MessageBox.Show("Error: Imagen no cargada.");
                return;
            }
            this.pila.Apilar(this.img);
            this.img.EscalaGrises();
            this.bmp = this.img.ConvertirPPMaBMP();
            this.pictureBox1.Image = (Image)this.bmp;
            this.deshacerToolStripMenuItem.Enabled = true;
        }

        //Cambia la intensidad de un color de la imagen
        private void intensidadDeColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.img == null)
            {
                MessageBox.Show("Error: Imagen no cargada.");
                return;
            }
            FormIntensidad intensidad = new FormIntensidad();
            intensidad.ShowDialog();
            if (intensidad.GetStatus() == false)
                return;
            this.pila.Apilar(this.img);
            int res = this.img.IntensificarColor(intensidad.GetColor(), intensidad.GetPorcentaje(), intensidad.GetAumentar());
            if (res == -1)
            {
                MessageBox.Show("Error: Color Incorrecto.");
                this.pila.GetImagen();
                return;
            }
            if (res == -2)
            {
                MessageBox.Show("Error: Porcentaje Incorrecto.");
                this.pila.GetImagen();
                return;
            }
            this.bmp = this.img.ConvertirPPMaBMP();
            this.pictureBox1.Image = (Image)this.bmp;
            this.deshacerToolStripMenuItem.Enabled = true;
        }

        //Añade un marco a la imagen
        private void añadirMarcoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.img == null)
            {
                MessageBox.Show("Error: Imagen no cargada.");
                return;
            }
            FormMarco marco = new FormMarco();
            marco.ShowDialog();
            if (marco.GetStatus() == false)
                return;
            this.pila.Apilar(this.img);
            this.img.AñadirMarco(marco.GetGrosor(), marco.GetColor());
            this.bmp = this.img.ConvertirPPMaBMP();
            this.pictureBox1.Image = (Image)this.bmp;
            this.deshacerToolStripMenuItem.Enabled = true;
        }

        //Recorta la imagen a una más pequeña seleccionada por el usuario.
        private void recortarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.img == null)
            {
                MessageBox.Show("Error: Imagen no cargada.");
                return;
            }
            FormRecortarImg formRecortar = new FormRecortarImg();
            formRecortar.SetImg(this.img);
            formRecortar.ShowDialog();
            if (formRecortar.GetStatus() == false)
                return; 
            this.pila.Apilar(this.img);
            this.img = formRecortar.GetImg();
            this.bmp = this.img.ConvertirPPMaBMP();
            this.pictureBox1.Image = (Image)this.bmp;
            this.deshacerToolStripMenuItem.Enabled = true;
        }

        private void presentaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Presentaciones pres = new Presentaciones();
            pres.ShowDialog();
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void crearMosaicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormMosaico fm1 = new FormMosaico();
            fm1.ShowDialog();
        }

        private void pixelarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPixelar fp = new FormPixelar();
            fp.setImgAlto(this.img.GetAlto());
            fp.setImgAncho(this.img.GetAncho());
            fp.ShowDialog();

            if (fp.isCorrect())
            {
                this.pila.Apilar(this.img);
                if (fp.isAllImage())
                {
                    this.img.PixelarImagen(fp.getBar(), 1, 1, 99999, 99999);
                }
                else
                {
                    this.img.PixelarImagen(fp.getBar(), fp.getStartX(), fp.getStartY(), fp.getFinalX(), fp.getFinalY());
                }
            }

            this.bmp = this.img.ConvertirPPMaBMP();
            this.pictureBox1.Image = (Image)this.bmp;
            this.deshacerToolStripMenuItem.Enabled = true;
        }

        private void añadirSonrisaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.img == null)
            {
                MessageBox.Show("Error: Imagen no cargada.");
                return;
            }
            FormEmoji formsonrisa = new FormEmoji();
            formsonrisa.SetImg(this.img);
            formsonrisa.ShowDialog();
            if (formsonrisa.GetStatus() == false)
                return;
            this.pila.Apilar(this.img);
            this.img = formsonrisa.GetImg();
            this.bmp = this.img.ConvertirPPMaBMP();
            this.pictureBox1.Image = (Image)this.bmp;
            this.deshacerToolStripMenuItem.Enabled = true;
        }

       
    }
}
