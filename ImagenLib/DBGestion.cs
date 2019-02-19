using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace ImagenLib
{
    public class DBGestion
    {
        //Atributos
        OleDbConnection cnx;
        string providerStr = "Provider=Microsoft.SQLSERVER.CE.OLEDB.3.5;";
        string cnxStr;

        //Constructor
        public DBGestion(string dbFileName)
        {
            cnxStr = providerStr + "Data Source=" + dbFileName + ";Persist Security Info=False;";
        }

        //Abrir base de datos
        public int openDB()
        {
            try
            {
                cnx = new OleDbConnection(cnxStr);
                cnx.Open();
            }
            catch (OleDbException)
            {
                return -1;
            }
            return 0;
        }

        //Cerrar base de datos
        public void closeDB()
        {
            if (cnx != null)
            {
                cnx.Close();
                cnx = null;
            }
        }

        //Obtener tabla Registro
        public DataTable GetTabla()
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM Registro";
            OleDbDataAdapter adp = new OleDbDataAdapter(query, cnx);
            adp.Fill(dt);
            return dt;
        }

        //Comprueba si un nombre de imagen existe en la tabla de Registro
        //Devuelve 0: Sí existe; -1: No existe.
        public int ComprobarImagen(string nombre)
        {
            DataTable dt = new DataTable();
            string query = "SELECT nombre FROM Registro WHERE nombre='" + nombre + "'";
            OleDbDataAdapter adp = new OleDbDataAdapter(query, cnx);
            adp.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }

        //Añade una fila a la tabla de Registro con los valores proporcionados.
        //Devuelve 0: Añadido correctamente; -1: Error al añadir.
        public int AñadirImagen(string nombre, int alto, int ancho)
        {
            DateTime time = DateTime.Now;
            string query = "INSERT INTO Registro VALUES ('" + nombre + "', " + alto + " , " + ancho + ", GETDATE())";
            OleDbCommand command = new OleDbCommand(query, cnx);
            int res = command.ExecuteNonQuery();
            if (res != 1)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        //Modifica los valores de una fila de la tabla Registro con los valores proporcionados
        //Devuelve 0: Modificado correctamente; -1: Error al modificar.
        public int ModificarImagen(string nombre, int alto, int ancho)
        {
            string query = "UPDATE Registro SET alto=" + alto + ", ancho=" + ancho + ", fecha = GETDATE() WHERE nombre = '" + nombre + "'";
            OleDbCommand command = new OleDbCommand(query, cnx);
            int res = command.ExecuteNonQuery();
            if (res != 1)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        //Elimina una fila cuyo nombre proporcionado de la tabla Registro
        //Devuelve 0: Eliminada correctamente; -1: Error al eliminar.
        public int EliminarImagen(string nombre)
        {
            string query = "DELETE FROM Registro WHERE nombre = '" + nombre + "'";
            OleDbCommand command = new OleDbCommand(query, cnx);
            int res = command.ExecuteNonQuery();
            if (res != 1)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
