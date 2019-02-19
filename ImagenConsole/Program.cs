using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImagenLib;

namespace ImagenConsole
{
    class Program
    {
        //El menu espera una opcion tecleada por el usuario
        static int Menu(string m = "")
        {
            int i = -1;
            string c;
            while (i == -1)
            {
                Console.Clear();
                Console.WriteLine(m);
                m = "";
                Console.WriteLine("0. Salir");
                Console.WriteLine("1. Cargar");
                Console.WriteLine("2. Guardar");
                Console.WriteLine("3. Cambiar Pixel");
                Console.WriteLine("4. Mostrar Matriz");
                Console.WriteLine("5. Matriz Inversa");
                Console.WriteLine("6. Mostrar Cabezera");

                c = Console.ReadKey().KeyChar.ToString();
                try
                {
                    i = Convert.ToInt32(c);
                }
                catch (FormatException)
                {
                    m = "No es una entrada válida.";
                }
                if (i > 6)
                {
                    i = -1;
                    m = "No es un número válido.";
                }
            }
            return i;
        }
        //Cargar imagen.ppm en memoria
        static string Cargar(Imagen img)
        {
            switch (img.CargarPPM())
            {
                case 0:
                    return "Imagen cargada correctamente.";
                case -1:
                    return "Imagen no encontrada.";
                case -2:
                    return "Formato de los datos de la imagen incorrectos.";
                default:
                    return "";
            }
        }
        //Guardar imagen.ppm en el disco duro
        static string Salvar(Imagen img)
        {
            switch (img.GuardarPPM("imagen.ppm"))
            {
                case 0:
                    return "Imagen guardada correctamente.";
                case -1:
                    return "Imagen no encontrada.";
                case -2:
                    return "No hay ninguna imagen cargada.";
                default:
                    return "";
            }
        }
        //Cambiar los colores de un pixel de una posicion determinada.
        static string CambiarPixel(Imagen img)
        {
            int x, y;
            byte r, g, b;
            try
            {
                Console.Clear();
                Console.Write("Fila: ");
                x = Convert.ToInt32(Console.ReadLine());
                Console.Write("Columna: ");
                y = Convert.ToInt32(Console.ReadLine());
                Console.Write("Rojo: ");
                r = Convert.ToByte(Console.ReadLine());
                Console.Write("Verde: ");
                g = Convert.ToByte(Console.ReadLine());
                Console.Write("Azul: ");
                b = Convert.ToByte(Console.ReadLine());
            }
            catch(FormatException)
            {
                return "Formato de los datos introducidos incorrectos.";
            }
            switch (img.EditarPixel(x, y, r, g, b))
            {
                case -1:
                    return "No existe este pixel.";
                case -2:
                    return "No hay ninguna imagen cargada.";
                case -3:
                    return "Formato de los datos introducidos incorrectos.";
                default:
                    return "Pixel cambiado correctamente.";
            }
        }
        //Muestra la matriz de pixeles de la imagen cargada.
        static string MostrarMatriz(Imagen img)
        {
            Console.Clear();
            Pixel[,] matrix = img.GetDatos();
            if (matrix == null)
                return "No hay ninguna imagen cargada.";
            for (int i = 0; i < img.GetAlto(); i++)
            {
                for (int j = 0; j < img.GetAncho(); j++)
                {
                    Console.Write(matrix[i, j].GetR() + " " + matrix[i, j].GetG() + " " + matrix[i, j].GetB() + " ");
                }
                Console.WriteLine();
            }
            Console.ReadKey();
            return "";
        }
        //Voltea sobre el eje vertical la matriz.
        static string InvertirMatriz(Imagen img)
        {
            Pixel[,] matrix = img.GetDatos();
            if (matrix == null)
                return "No hay ninguna imagen cargada.";
            img.VoltearV();
            return "La imagen ha sido volteada.";
        }
        //Muestra la cabezera de la imagen cargada.
        static void Mostrarcabezera(Imagen img)
        {
            Console.Clear();
            Console.WriteLine("\n\n El identificador es: " + img.GetIdentificador() +
                "\n El ancho es: " + img.GetAncho() +
                "\n El alto es: " + img.GetAlto() +
                "\n Número de niveles: " + img.GetNiveles());
            Console.ReadKey();
        }
        static void Main(string[] args)
        {
            Imagen imagen = new Imagen("imagen.ppm");
            bool exit = false;
            string m = "";
            while (!exit)
            {
                switch (Menu(m))
                {
                    case 0:
                        exit = true;
                        break;
                    case 1:
                        m = Cargar(imagen);
                        break;
                    case 2:
                        m = Salvar(imagen);
                        break;
                    case 3:
                        m = CambiarPixel(imagen);
                        break;
                    case 4:
                        m = MostrarMatriz(imagen);
                        break;
                    case 5:
                        m = InvertirMatriz(imagen);
                        break;
                    case 6:
                        Mostrarcabezera(imagen);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
