using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImagenLib
{
    public class Pixel
    {
        //Atributos
        byte r;
        byte g;
        byte b;

        //Constructor
        public Pixel(byte red, byte green, byte blue)
        {
            this.r = red;
            this.g = green;
            this.b = blue;
        }

        //Establecer y Obtener cualquier color.
        public void SetR(byte red)
        {
            this.r = red;
        }
        public void SetG(byte green)
        {
            this.g = green;
        }
        public void SetB(byte blue)
        {
            this.b = blue;
        }
        public byte GetR()
        {
            return this.r;
        }
        public byte GetG()
        {
            return this.g;
        }
        public byte GetB()
        {
            return this.b;
        }
        public Pixel copy()
        {
            return new Pixel(this.r, this.g, this.b);
        }
    }
}
