using ProyectoBiblioteca.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace ProyectoBiblioteca.Logica
{
    public class TipocuadrillaLogica
    {
        private static TipocuadrillaLogica instancia = null;

        public TipocuadrillaLogica() {

        }

        public static TipocuadrillaLogica Instancia {
            get {
                if (instancia == null) {
                    instancia = new TipocuadrillaLogica();
                }

                return instancia;
            }
        }

        public bool Registrar(Tipocuadrilla oTipocuadrilla)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarTipocuadrilla", oConexion);
                    cmd.Parameters.AddWithValue("Descripcion", oTipocuadrilla.Descripcion);
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

        public bool Modificar(Tipocuadrilla oTipocuadrilla)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ModificarTipocuadrilla", oConexion);
                    cmd.Parameters.AddWithValue("IdTipocuadrilla", oTipocuadrilla.IdTipocuadrilla);
                    cmd.Parameters.AddWithValue("Descripcion", oTipocuadrilla.Descripcion);
                    cmd.Parameters.AddWithValue("Estado", oTipocuadrilla.Estado);
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


        public List<Tipocuadrilla> Listar()
        {
            List<Tipocuadrilla> Lista = new List<Tipocuadrilla>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("select IdTipocuadrilla,Descripcion,Estado from Tipocuadrilla", oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader()) {
                        while (dr.Read()) {
                            Lista.Add(new Tipocuadrilla() {
                                IdTipocuadrilla = Convert.ToInt32(dr["IdTipocuadrilla"]),
                                Descripcion = dr["Descripcion"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<Tipocuadrilla>();
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
                    SqlCommand cmd = new SqlCommand("delete from TIPOCUADRILLA where idTipocuadrilla = @id", oConexion);
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