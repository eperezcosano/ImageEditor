using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace ImagenLib
{
    public class Imagen
    {
        //Atributos
        string archivo;
        string identificador;
        int ancho;
        int alto;
        byte niveles;
        Pixel[,] datos;

        //Constructor
        public Imagen(string archivo)
        {
            this.archivo = archivo;
        }

        //Cargar imagen PPM en memoria
        //Devuelve 0: si ha sido cargada correctamente; -1: No encontrado; -2: Formato incorrecto.
        public int CargarPPM()
        {
            string id;
            string[] lineaDim, lineaDatos;
            byte r, g, b, n;
            Pixel[,] tmpMatrix;
            using (StreamReader R = new StreamReader(this.archivo))
            {
                try
                {
                    id = R.ReadLine();
                    if (id != "P1" && id != "P2" && id != "P3" && id != "P4" && id != "P5" && id != "P6")
                        return -2;
                    this.identificador = id;
                    lineaDim = R.ReadLine().Split(' ');
                    if (lineaDim.Length != 2)
                        return -2;
                    this.alto = Convert.ToInt32(lineaDim[1]);
                    this.ancho = Convert.ToInt32(lineaDim[0]);
                    n = Convert.ToByte(R.ReadLine());
                    if (n > 255)
                        return -2;
                    this.niveles = n;
                    tmpMatrix = new Pixel[this.alto, this.ancho];
                    for (int i = 0; R.Peek() >= 0; i++)
                    {
                        lineaDatos = R.ReadLine().Split(' ');
                        if (lineaDatos.Length != this.ancho * 3)
                            return -2;
                        for (int j = 0; j < lineaDatos.Length; j += 3)
                        {
                            r = Convert.ToByte(lineaDatos[j]);
                            g = Convert.ToByte(lineaDatos[j + 1]);
                            b = Convert.ToByte(lineaDatos[j + 2]);
                            tmpMatrix[i, j / 3] = new Pixel(r, g, b);
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (ex is FileNotFoundException)
                        return -1;
                    if (ex is FormatException)
                        return -2;
                    return -1;
                }
                this.datos = tmpMatrix;
                R.Close();
                return 0;
            }
        }

        //Realiza una copia de una imagen
        public Imagen Copiar()
        {
            Imagen img = new Imagen(this.archivo);
            img.SetIdentificador(this.identificador);
            img.SetAlto(this.alto);
            img.SetAncho(this.ancho);
            img.SetNiveles(this.niveles);
            img.SetDatos(this.datos);
            return img;
        }

        //Convierte cualquier imagen PPM a una imagen Bitmap en memoria.
        public Bitmap ConvertirPPMaBMP()
        {
            Bitmap bmp = new Bitmap(this.ancho, this.alto);
            for (int i = 0; i < this.alto; i++)
            {
                for (int j = 0; j < this.ancho; j++)
                {
                    Color color = Color.FromArgb(this.datos[i, j].GetR(), this.datos[i, j].GetG(), this.datos[i, j].GetB());
                    bmp.SetPixel(j, i, color);
                }
            }
            return bmp;
        }

        //Carga una imagen Bitmap en memoria
        //Devuelve 0: si ha sido cargada correctamente; -1: No encontrado; -2: Formato incorrecto.
        public int CargarBMP(string ruta)
        {
            using (Bitmap bmp = new Bitmap(ruta))
            {
                try
                {
                    this.archivo = ruta;
                    this.identificador = "P3";
                    this.ancho = bmp.Width;
                    this.alto = bmp.Height;
                    this.niveles = 255;
                    Pixel[,] tmpDatos = new Pixel[this.alto, this.ancho];
                    for (int i = 0; i < this.ancho; i++)
                    {
                        for (int j = 0; j < this.alto; j++)
                        {
                            Color color = bmp.GetPixel(i, j);
                            tmpDatos[j, i] = new Pixel(color.R, color.G, color.B);
                        }
                    }
                    this.datos = tmpDatos;
                    return 0;
                }
                catch (Exception ex)
                {
                    if (ex is FileNotFoundException)
                        return -1;
                    if (ex is FormatException)
                        return -2;
                    return -1;
                }
            }
        }

        //Guarda una imagen PPM en el disco duro
        //Devuelve 0: si ha sido guardada correctamente; -1: No encontrado; -2: Datos vacíos.
        public int GuardarPPM(string ruta)
        {
            if (this.identificador == null || this.ancho == 0 || this.alto == 0 || this.niveles == 0 || this.datos == null)
                return -2;
            StreamWriter W;
            try
            {
                W = new StreamWriter(ruta);              
                W.WriteLine(this.identificador);
                W.WriteLine(this.ancho + " " + this.alto);
                W.WriteLine(this.niveles);
                for (int i = 0; i < this.alto; i++)
                {
                    string linea = "";
                    for (int j = 0; j < this.ancho; j++)
                    {
                        linea += this.datos[i, j].GetR() + " " + this.datos[i, j].GetG() + " " + this.datos[i, j].GetB();
                        if (j < this.ancho - 1)
                            linea += " ";
                    }
                    W.WriteLine(linea);
                }
            }
            catch (Exception ex)
            {
                if (ex is FileNotFoundException)
                    return -1;
                if (ex is FormatException)
                    return -2;
                return -1;
            }
            W.Close();
            return 0;
        }

        //Guarda una imagen Bitmap en el disco duro
        //Devuelve 0: si ha sido guardada correctamente; -1: No encontrado.
        public int GuardarBMP(Bitmap bmp, string ruta)
        {
            try
            {
                bmp.Save(ruta);
            }
            catch (Exception)
            {
                return -1;
            }
            return 0;
        }

        //Voltea la imagen sobre el eje vertical.
        public void VoltearV()
        {
            Pixel[,] vmatrix = new Pixel[this.alto, this.ancho];
            for (int i = 0; i < this.alto; i++)
            {
                for (int j = this.ancho - 1, c = 0; j >= 0; j--, c++)
                {
                    vmatrix[i, c] = this.datos[i, j];
                }
            }
            this.datos = vmatrix;
        }

        public void PixelarImagen(int ladoCuadros, int startX, int startY, int finalX, int finalY)
        {
            for (int i = startY; i < finalY; i += ladoCuadros)
            {
                for (int j = startX; j < finalX; j += ladoCuadros)
                {
                    // EVITO PASARME
                    if (i >= this.alto || j >= this.ancho)
                        continue;

                    // SELECCIONO EL PRIMER PIXEL
                    Pixel p = this.datos[i, j].copy();

                    // LO COPIO EN CUADRADOS
                    for (int a = 0; a < ladoCuadros; a++)
                    {
                        for (int b = 0; b < ladoCuadros; b++)
                        {
                            // ME ASEGURO DE NO PASARME
                            if (this.alto > (i + a) && this.ancho > (j + b))
                            {
                                datos[(i + a), (j + b)] = p.copy();
                            }
                        }
                    }

                }
            }
        }

        //Voltea la imagen sobre el eje horizontal.
        public void VoltearH()
        {
            Pixel[,] hmatrix = new Pixel[this.alto, this.ancho];
            for (int j = 0; j < this.ancho; j++)
            {
                for (int i = this.alto - 1, f = 0; i >= 0; i--, f++)
                {
                    hmatrix[f, j] = this.datos[i, j];
                }
            }
            this.datos = hmatrix;
        }

        //Convierte la imagen a escala de grises
        public void EscalaGrises()
        {
            Pixel[,] gmatrix = new Pixel[this.alto, this.ancho];
            for (int i = 0; i < this.alto; i++)
            {
                for (int j = 0; j < this.ancho; j++)
                {
                    byte pixelGris = Convert.ToByte((Convert.ToInt32(this.datos[i, j].GetR()) + Convert.ToInt32(this.datos[i, j].GetG()) + Convert.ToInt32(this.datos[i, j].GetB())) / 3);
                    gmatrix[i, j] = new Pixel(pixelGris, pixelGris, pixelGris);
                }
            }
            this.datos = gmatrix;
        }
        //Aumenta o disminuye la intensidad de un color. Tiene en cuenta problemas de saturación.
        //Entra: string color: rojo, verdo o azul. Porcentage en %. bool aumentar = true. false= disminuye.
        //Devuelve 0 si ha intesificado correctamente, -1 color incorrecto, -2 porcentaje incorrecto.
        public int IntensificarColor(string color, float porcentaje, bool aumentar)
        {
            if (porcentaje <= 0 || porcentaje > 100)
                return -2;
            if (aumentar == true)
                porcentaje = 1 + (porcentaje / 100);
            else
                porcentaje = 1 - (porcentaje / 100);

            Pixel[,] icMatrix = new Pixel[this.alto, this.ancho];
            for (int i = 0; i < this.alto; i++)
            {
                for (int j = 0; j < this.ancho; j++)
                {
                    switch (color)
                    {
                        case "Rojo":
                            byte R;
                            float rojoFinal = this.datos[i, j].GetR() * porcentaje;
                            if (rojoFinal > 255)
                                R = 255;
                            else
                                R = Convert.ToByte(rojoFinal);
                            icMatrix[i, j] = new Pixel(R, this.datos[i, j].GetG(), this.datos[i, j].GetB());
                            break;
                        case "Verde":
                            byte G;
                            float verdeFinal = this.datos[i, j].GetG() * porcentaje;
                            if (verdeFinal > 255)
                                G = 255;
                            else
                                G = Convert.ToByte(verdeFinal);
                            icMatrix[i, j] = new Pixel(this.datos[i, j].GetR(), G, this.datos[i, j].GetB());
                            break;
                        case "Azul":
                            byte B;
                            float azulFinal = this.datos[i, j].GetB() * porcentaje;
                            if (azulFinal > 255)
                                B = 255;
                            else
                                B = Convert.ToByte(azulFinal);
                            icMatrix[i, j] = new Pixel(this.datos[i, j].GetR(), this.datos[i, j].GetG(), B);
                            break;
                        default:
                            return -1;
                    }
                }
            }
            this.datos = icMatrix;
            return 0;
        }
        //Añade un marco con el grosor y colores indicados
        public void AñadirMarco(int grosor, Color color)
        {
            Pixel[,] mMatrix = new Pixel[this.alto, this.ancho];
            for (int i = 0; i < this.alto; i++)
            {
                for (int j = 0; j < this.ancho; j++)
                {
                    if (i < grosor || j < grosor || i > this.alto - grosor || j > this.ancho - grosor)
                        mMatrix[i, j] = new Pixel(color.R, color.G, color.B);
                    else
                        mMatrix[i, j] = this.datos[i, j];
                }
            }
            this.datos = mMatrix;
        }

        //Edita los colores de un pixel de una posicion determinada.
        //Devuelve 0: Editado correctamente; -1: Posición incorrecta; -2: Imagen no cargada; -3: Colores incorrectos
        public int EditarPixel(int i, int j, byte r, byte g, byte b)
        {
            if (this.identificador == null || this.ancho == 0 || this.alto == 0 || this.niveles == 0 || this.datos == null)
                return -2;
            if (i > this.ancho || j > this.alto || i < 0 || j < 0)
                return -1;
            if (r > 255 || g > 255 || b > 255)
                return -3;
            this.datos[i, j].SetR(r);
            this.datos[i, j].SetG(g);
            this.datos[i, j].SetB(b);
            return 0;
        }


        public void Recortar(int p, int pp, int ppp, int pppp, int r, int rr) 
        {
            this.ancho = r;
            this.alto = rr;

            // LE AGREGAMOS UNA COPIA DE LOS PÍXELES DE LA IMAGEN ACTUAL
            Pixel[,] newDatos = new Pixel[r, rr];

            int i = p;
            int k = 0;

            while (i < ppp && k < r) 
            {
                int j = pp;
                int l = 0;

                while (j < pppp && l < rr) 
                {
                    newDatos[k, l] = new Pixel((byte)datos[i, j].GetR(), (byte)datos[i, j].GetG(), (byte)datos[i, j].GetB());

                    j++;
                    l++;
                }
                i++;
                k++;
            }

            this.datos = newDatos;

        }
        //Establecer y obtener cualquier atributo.
        public void SetArchivo(string archivo)
        {
            this.archivo = archivo;
        }
        public void SetIdentificador(string identificador)
        {
            this.identificador = identificador;
        }
        public void SetAncho(int ancho)
        {
            this.ancho = ancho;
        }
        public void SetAlto(int alto)
        {
            this.alto = alto;
        }
        public void SetNiveles(byte niveles)
        {
            this.niveles = niveles;
        }
        public void SetDatos(Pixel[,] datos)
        {
            this.datos = datos;
        }
        public string GetArchivo()
        {
            return this.archivo;
        }
        public string GetIdentificador()
        {
            return this.identificador;
        }
        public int GetAncho()
        {
            return this.ancho;
        }
        public int GetAlto()
        {
            return this.alto;
        }
        public byte GetNiveles()
        {
            return this.niveles;
        }
        public Pixel[,] GetDatos()
        {
            return this.datos;
        }
    }
}
