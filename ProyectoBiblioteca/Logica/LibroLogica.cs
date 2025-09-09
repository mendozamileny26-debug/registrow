using ProyectoBiblioteca.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace ProyectoBiblioteca.Logica
{
    public class LibroLogica
    {

        private static LibroLogica instancia = null;

        public LibroLogica()
        {

        }

        public static LibroLogica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new LibroLogica();
                }

                return instancia;
            }
        }

        public List<Libro> Listar(int IdUsuario, int IdTipoPersona)
        {

            List<Libro> rptListaLibro = new List<Libro>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("select l.IdSolicitud,");
                sb.AppendLine("l.IdTiposolicitud,");
                sb.AppendLine("t.Descripcion as DescripcionTiposolicitud,");
                sb.AppendLine("l.RutaPortada,");
                sb.AppendLine("l.NombrePortada,");
                sb.AppendLine("a.IdArea,");
                sb.AppendLine("a.Descripcion as DescripcionArea,");
                sb.AppendLine("c.IdTipocuadrilla,");
                sb.AppendLine("c.Descripcion as DescripcionTipocuadrilla,");
                sb.AppendLine("z.IdZona,");
                sb.AppendLine("z.Descripcion as DescripcionZona,");
                sb.AppendLine("l.Ubicacion,");
                sb.AppendLine("l.TD1,");
                sb.AppendLine("l.TD2,");
                sb.AppendLine("l.DniL,");
                sb.AppendLine("l.DniO,");
                sb.AppendLine("l.NombreL,");
                sb.AppendLine("l.NombreO,");
                sb.AppendLine("l.CelularL,");
                sb.AppendLine("l.CelularO,");
                sb.AppendLine("l.CorreoL,");
                sb.AppendLine("l.CorreoO,");
                sb.AppendLine("l.ModeloV,");
                sb.AppendLine("l.PlacaV,");
                sb.AppendLine("l.SCTR,");
                sb.AppendLine("l.UsuarioRegistro,");
                sb.AppendLine("l.MensajeValidacion,");
                sb.AppendLine("l.Estado,");
                sb.AppendLine("l.IdUsuario");  // 🔹 Nuevo
                sb.AppendLine("from SOLICITUD l");
                sb.AppendLine("inner join TIPOSOLICITUD t on t.IdTiposolicitud = l.IdTiposolicitud");
                sb.AppendLine("inner join AREA a on a.IdArea = l.IdArea");
                sb.AppendLine("inner join TIPOCUADRILLA c on c.IdTipocuadrilla = l.IdTipocuadrilla");
                sb.AppendLine("inner join ZONA z on z.IdZona = l.IdZona");


                // 🔹 Filtro dinámico
                if (IdTipoPersona == 2) // Usuario normal: solo sus registros
                {
                    sb.AppendLine("where l.IdUsuario = @IdUsuario");
                }
                // Si es admin (IdTipoPersona == 1) → no se agrega filtro

                SqlCommand cmd = new SqlCommand(sb.ToString(), oConexion);
                cmd.CommandType = CommandType.Text;

                if (IdTipoPersona == 2)
                {
                    cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                }

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaLibro.Add(new Libro()
                        {
                            IdSolicitud = Convert.ToInt32(dr["IdSolicitud"].ToString()),
                            oTsolicitud = new Tsolicitud() { IdTiposolicitud = Convert.ToInt32(dr["IdTiposolicitud"].ToString()), Descripcion = dr["DescripcionTiposolicitud"].ToString() },
                            RutaPortada = dr["RutaPortada"].ToString(), 
                            NombrePortada = dr["NombrePortada"].ToString(),
                            oArea = new Area() { IdArea = Convert.ToInt32(dr["IdArea"].ToString()), Descripcion = dr["DescripcionArea"].ToString() },
                            oTipocuadrilla = new Tipocuadrilla() { IdTipocuadrilla = Convert.ToInt32(dr["IdTipocuadrilla"].ToString()), Descripcion = dr["DescripcionTipocuadrilla"].ToString() },
                            oZona = new Zona() { IdZona = Convert.ToInt32(dr["IdZona"].ToString()), Descripcion = dr["DescripcionZona"].ToString() },
                            Ubicacion = dr["Ubicacion"].ToString(),
                            TD1 = dr["TD1"] != DBNull.Value && Convert.ToBoolean(dr["TD1"]),
                            TD2 = dr["TD2"] != DBNull.Value && Convert.ToBoolean(dr["TD2"]),
                            DniL = dr["DniL"].ToString(),
                            DniO = dr["DniO"].ToString(),
                            NombreL = dr["NombreL"].ToString(),
                            NombreO = dr["NombreO"].ToString(),
                            CelularL = dr["CelularL"].ToString(),
                            CelularO = dr["CelularO"].ToString(),
                            CorreoL = dr["CorreoL"].ToString(),
                            CorreoO = dr["CorreoO"].ToString(),
                            ModeloV = dr["ModeloV"].ToString(),
                            PlacaV = dr["PlacaV"].ToString(),
                            SCTR = dr["SCTR"] != DBNull.Value && Convert.ToBoolean(dr["SCTR"]),
                            IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                            UsuarioRegistro = dr["UsuarioRegistro"].ToString(),
                            MensajeValidacion = dr["MensajeValidacion"].ToString(),
                            //SCTR = Convert.ToBoolean(dr["SCTR"].ToString()),
                            //base64 = Utilidades.convertirBase64(Path.Combine(dr["RutaPortada"].ToString(), dr["NombrePortada"].ToString())),
                            extension = Path.GetExtension(dr["NombrePortada"].ToString()).Replace(".",""),
                            Estado = dr["Estado"].ToString() // ✅ ESTA LÍNEA ES CLAVE

                            
                        });
                    }
                    dr.Close();

                    return rptListaLibro;

                }
                catch (Exception ex)
                {
                    rptListaLibro = null;
                    return rptListaLibro;
                }
            }
        }


        public int Registrar(Libro objeto, string nuevoestado)
        {
            int respuesta = 0;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_registrarSolicitud", oConexion);
                    cmd.Parameters.AddWithValue("IdTiposolicitud", objeto.oTsolicitud.IdTiposolicitud);
                    cmd.Parameters.AddWithValue("RutaPortada", (object)objeto.RutaPortada ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("NombrePortada", (object)objeto.NombrePortada ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("IdArea", objeto.oArea.IdArea);
                    cmd.Parameters.AddWithValue("IdTipocuadrilla", objeto.oTipocuadrilla.IdTipocuadrilla);
                    cmd.Parameters.AddWithValue("IdZona", objeto.oZona.IdZona);
                    cmd.Parameters.AddWithValue("Ubicacion", objeto.Ubicacion);
                    cmd.Parameters.AddWithValue("TD1", objeto.TD1);
                    cmd.Parameters.AddWithValue("TD2", objeto.TD2);
                    cmd.Parameters.AddWithValue("DniL", objeto.DniL);
                    cmd.Parameters.AddWithValue("DniO", objeto.DniO);
                    cmd.Parameters.AddWithValue("NombreL", objeto.NombreL);
                    cmd.Parameters.AddWithValue("NombreO", objeto.NombreO);
                    cmd.Parameters.AddWithValue("CelularL", objeto.CelularL);
                    cmd.Parameters.AddWithValue("CelularO", objeto.CelularO);
                    cmd.Parameters.AddWithValue("CorreoL", objeto.CorreoL);
                    cmd.Parameters.AddWithValue("CorreoO", objeto.CorreoO);
                    cmd.Parameters.AddWithValue("ModeloV", objeto.ModeloV);
                    cmd.Parameters.AddWithValue("PlacaV", objeto.PlacaV);
                    cmd.Parameters.AddWithValue("Estado", nuevoestado);

                    // ✅ Nuevo: quién registró (IdUsuario)
                    cmd.Parameters.AddWithValue("IdUsuario", objeto.IdUsuario);

                    // 🔸 PARÁMETROS FALTANTES
                    cmd.Parameters.AddWithValue("UsuarioRegistro", objeto.UsuarioRegistro ?? "Sistema");

                    // 🔹 Nuevos parámetros de evidencias
                    cmd.Parameters.AddWithValue("RutaDocApoyo", (object)objeto.RutaDocApoyo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("RutaVehiculo", (object)objeto.RutaVehiculo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("RutaSoat", (object)objeto.RutaSoat ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("RutaLicencia", (object)objeto.RutaLicencia ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("RutaPoliza", (object)objeto.RutaPoliza ?? DBNull.Value);


                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToInt32(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception ex)
                {
                    respuesta = 0;
                }
            }
            return respuesta;
        }

        //nuevo codigo - evidencias
        public bool ActualizarRutasEvidencias(Libro solicitud)
        {
            bool respuesta = false;

            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("UPDATE SOLICITUD SET " +
                        "RutaPortada = @RutaPortada, " +
                        "RutaDocApoyo = @RutaDocApoyo, " +
                        "RutaVehiculo = @RutaVehiculo, " +
                        "RutaSoat = @RutaSoat, " +
                        "RutaLicencia = @RutaLicencia, " +
                        "RutaPoliza = @RutaPoliza " +
                        "WHERE IdSolicitud = @IdSolicitud", oConexion);

                    cmd.Parameters.AddWithValue("@RutaPortada", (object)solicitud.RutaPortada ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@RutaDocApoyo", (object)solicitud.RutaDocApoyo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@RutaVehiculo", (object)solicitud.RutaVehiculo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@RutaSoat", (object)solicitud.RutaSoat ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@RutaLicencia", (object)solicitud.RutaLicencia ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@RutaPoliza", (object)solicitud.RutaPoliza ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IdSolicitud", solicitud.IdSolicitud);

                    oConexion.Open();
                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception)
                {
                    respuesta = false;
                }
            }

            return respuesta;
        }

        public int Validacion(Libro objeto)
        {
            int respuesta = 0;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("ValidarDocumentos", oConexion);
                    
                    cmd.Parameters.AddWithValue("DniL", objeto.DniL);
                    cmd.Parameters.AddWithValue("DniO", objeto.DniO);

                    cmd.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToInt32(cmd.Parameters["@Resultado"].Value);

                }
                catch (Exception ex)
                {
                    respuesta = 0;
                }
            }
            return respuesta;
        }
        public bool Modificar(Libro objeto)
        {
            bool respuesta = false;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_modificarSolicitud", oConexion);
                    cmd.Parameters.AddWithValue("IdSolicitud", objeto.IdSolicitud);
                    cmd.Parameters.AddWithValue("IdTiposolicitud", objeto.oTsolicitud.IdTiposolicitud);
                    cmd.Parameters.AddWithValue("IdArea", objeto.oArea.IdArea);
                    cmd.Parameters.AddWithValue("IdTipocuadrilla", objeto.oTipocuadrilla.IdTipocuadrilla);
                    cmd.Parameters.AddWithValue("Ubicacion", objeto.Ubicacion);
                    cmd.Parameters.AddWithValue("DniL", objeto.DniL);
                    cmd.Parameters.AddWithValue("DniO", objeto.DniO);
                    cmd.Parameters.AddWithValue("NombreL", objeto.NombreL);
                    cmd.Parameters.AddWithValue("NombreO", objeto.NombreO);
                    cmd.Parameters.AddWithValue("CelularL", objeto.CelularL);
                    cmd.Parameters.AddWithValue("CelularO", objeto.CelularO);
                    cmd.Parameters.AddWithValue("CorreoL", objeto.CorreoL);
                    cmd.Parameters.AddWithValue("CorreoO", objeto.CorreoO);
                    cmd.Parameters.AddWithValue("ModeloV", objeto.ModeloV);
                    cmd.Parameters.AddWithValue("PlacaV", objeto.PlacaV);
                    cmd.Parameters.AddWithValue("SCTR", objeto.SCTR);
                    //cmd.Parameters.AddWithValue("Estado", objeto.Estado);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
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

        public bool ActualizarRutaImagen(Libro objeto)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_actualizarRutaImagen", oConexion);
                    cmd.Parameters.AddWithValue("IdSolicitud", objeto.IdSolicitud);
                    cmd.Parameters.AddWithValue("NombrePortada", objeto.NombrePortada);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }


        public bool Eliminar(int id)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("delete from SOLICITUD where IdSolicitud = @id", oConexion);
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