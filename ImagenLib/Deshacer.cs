using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImagenLib
{
    public class Deshacer
    {
        Stack<Imagen> listaImagenes = new Stack<Imagen>();

        public int GetNumero()
        {
            return this.listaImagenes.Count;
        }
        public Imagen GetImagen()
        {
            if (this.listaImagenes.Count == 0)
                return null;
            return this.listaImagenes.Pop();
        }
        public void Apilar(Imagen imagen)
        {
            this.listaImagenes.Push(imagen.Copiar());
        }
        public void Vaciar()
        {
            this.listaImagenes.Clear();
        }
    }
}
