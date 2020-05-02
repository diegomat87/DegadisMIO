using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Datos
{
    public class General
    {
        public static MySqlConnection crearConexion()
        {
            return new MySqlConnection("server=localhost;database=Degadis;uid=root;password=root");
        }

        public string Ruta
        {
            get
            {
                return Properties.Settings.Default.Ruta;
            }
            set
            {
                Properties.Settings.Default.Ruta = value;
                Properties.Settings.Default.Save();
            }
        }
    }
}