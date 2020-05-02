using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

namespace Datos
{
    public class Gases
    {
        public List<Entidades.Gas> getGases()
        {
            List<Entidades.Gas> gases = new List<Entidades.Gas>();
            using (MySqlConnection conexion = General.crearConexion())
            {
                MySqlCommand getGases = new MySqlCommand("select * FROM gases", conexion);

                conexion.Open();
                try
                {
                    MySqlDataReader aux = getGases.ExecuteReader();
                    using (aux)
                    {
                        while (aux.Read())
                        {
                            Entidades.Gas gas = new Entidades.Gas();
                            gas.id = Convert.ToInt16(aux["IdGas"]);
                            gas.formula = Convert.ToString(aux["Formula"]);
                            gas.Descripcion = Convert.ToString(aux["Nombre"]);

                            gases.Add(gas);
                        }
                    }
                }
                finally
                {
                    conexion.Close();
                }
            }
            return gases;
        }
        public Entidades.Gas getGas(int id)
        {
            Entidades.Gas gas = new Entidades.Gas();
            using (MySqlConnection conexion = General.crearConexion())
            {
                MySqlCommand getGas = new MySqlCommand("select * FROM gases WHERE IdGas=@id", conexion);
                getGas.Parameters.Add(new MySqlParameter("@id", MySqlDbType.Int16));

                conexion.Open();
                getGas.Parameters["@id"].Value = id;
                try
                {
                    MySqlDataReader aux = getGas.ExecuteReader();
                    using (aux)
                    {
                        while (aux.Read())
                        {
                            gas.id = Convert.ToInt16(aux["IdGas"]);
                            gas.formula = Convert.ToString(aux["Formula"]);
                            gas.Descripcion = Convert.ToString(aux["Nombre"]);
                            gas.pesoMolecular = Convert.ToDouble(aux["PesoMolecular"]);
                            gas.tempFuga = Convert.ToDouble(aux["TempFuga"]);
                            gas.densidadFuga = Convert.ToDouble(aux["DensidadFuga"]);
                            gas.calorEspecifico = Convert.ToDouble(aux["CalorEspecifico"]);
                            gas.capacidadCalorifica = Convert.ToDouble(aux["CapacidadCalorifica"]);
                            gas.ULC = Convert.ToDouble(aux["ULC"]);
                            gas.LLC = Convert.ToDouble(aux["LLC"]);
                            gas.AlturaFuga = Convert.ToDouble(aux["AlturaFuga"]);
                            break;
                        }
                    }
                }
                finally
                {
                    conexion.Close();
                }
            }
            return gas;
        }

        public void setGas(Entidades.Gas gas)
        {
            gas.id = getUltimo() + 1;
            using (MySqlConnection conexion = General.crearConexion())
            {
                MySqlCommand setGas = new MySqlCommand("INSERT INTO gases (IdGas,Nombre,Formula,PesoMolecular,TempFuga,DensidadFuga,CalorEspecifico,CapacidadCalorifica,ULC,LLC,AlturaFuga) VALUES (" + gas.id + ",'" + gas.Descripcion + "','" + gas.formula + "'," + gas.pesoMolecular + "," + gas.tempFuga + "," + gas.densidadFuga + "," + gas.calorEspecifico + "," + gas.capacidadCalorifica + "," + gas.ULC + "," + gas.LLC + "," + gas.AlturaFuga + ");", conexion);
                conexion.Open();
                DataSet ds = new DataSet();
                MySqlDataAdapter adapter = new MySqlDataAdapter(setGas);
                adapter.Fill(ds);
                conexion.Close();
            }
        }

        private int getUltimo()
        {
            int cod = 0;
            using (MySqlConnection conexion = General.crearConexion())
            {
                MySqlCommand getGas = new MySqlCommand("select MAX(g.`IdGas`) as ultimo from `gases` g", conexion);

                conexion.Open();
                try
                {
                    MySqlDataReader aux = getGas.ExecuteReader();
                    using (aux)
                    {
                        while (aux.Read())
                        {
                            cod = Convert.ToInt16(aux["ultimo"]);
                            break;
                        }
                    }
                }
                finally
                {
                    conexion.Close();
                }
                return cod;
            }
        }
    }
}
