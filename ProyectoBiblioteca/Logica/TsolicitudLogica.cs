using ProyectoBiblioteca.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBiblioteca.Logica
{
    public class TsolicitudLogica
    {

        private static TsolicitudLogica instancia = null;

        public TsolicitudLogica()
        {

        }

        public static TsolicitudLogica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new TsolicitudLogica();
                }

                return instancia;
            }
        }

        public bool Registrar(Tsolicitud oTsolicitud)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarTsolicitud", oConexion);
                    cmd.Parameters.AddWithValue("Descripcion", oTsolicitud.Descripcion);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public bool Modificar(Tsolicitud oTsolicitud)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ModificarTsolicitud", oConexion);
                    cmd.Parameters.AddWithValue("IdTiposolicitud", oTsolicitud.IdTiposolicitud);
                    cmd.Parameters.AddWithValue("Descripcion", oTsolicitud.Descripcion);
                    cmd.Parameters.AddWithValue("Estado", oTsolicitud.Estado);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }

            }

            return respuesta;

        }


        public List<Tsolicitud> Listar()
        {
            List<Tsolicitud> Lista = new List<Tsolicitud>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("select IdTiposolicitud,Descripcion,Estado from TIPOSOLICITUD", oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Tsolicitud()
                            {
                                IdTiposolicitud = Convert.ToInt32(dr["IdTiposolicitud"]),
                                Descripcion = dr["Descripcion"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<Tsolicitud>();
                }
            }
            return Lista;
        }

        public bool Eliminar(int id)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("delete from Tsolicitud where IdTiposolicitud = @id", oConexion);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = true;

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }

            }

            return respuesta;

        }
    }
}
