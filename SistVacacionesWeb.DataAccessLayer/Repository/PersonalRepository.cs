using SistVacacionesWeb.Domain.Models;
using SistVacacionesWeb.Domain.RepositoriesContracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistVacacionesWeb.DataAccessLayer.Repository
{
    public class PersonalRepository : Repository, IPersonalRepository, IDisposable
    {
        private readonly string _listar;
        private readonly string _grabar;
        private readonly string _recuperar;
        private readonly string _eliminar;

        public PersonalRepository()
        {
            _listar = "spListarPersonal";
            _grabar = "spGrabarPersonal";
            _recuperar = "spRecuperarPersonal";
            _eliminar = "spEliminarPersonal";
        }

        public List<PersonalModel> ListarPersonal(string codEmpresa)
        {
            List<PersonalModel> listPersonalModel = new List<PersonalModel>();
            try
            {
                using (var cn = GetSqlConnection())
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(_listar, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CodEmpresa", codEmpresa);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PersonalModel oPersonalModel = new PersonalModel();
                                oPersonalModel.Correlativo = reader.IsDBNull(reader.GetOrdinal("Correlativo")) ? 0 : Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Correlativo")));
                                oPersonalModel.IdPersonal = reader.IsDBNull(reader.GetOrdinal("IdPersonal")) ? 0 : reader.GetInt32(reader.GetOrdinal("IdPersonal"));
                                oPersonalModel.CodPersonal = reader.IsDBNull(reader.GetOrdinal("CodPersonal")) ? "" : reader.GetString(reader.GetOrdinal("CodPersonal"));
                                oPersonalModel.Nombre = reader.IsDBNull(reader.GetOrdinal("Nombre")) ? "" : reader.GetString(reader.GetOrdinal("Nombre"));
                                oPersonalModel.Apellido = reader.IsDBNull(reader.GetOrdinal("Apellido")) ? "" : reader.GetString(reader.GetOrdinal("Apellido"));
                                oPersonalModel.NombreCompleto = oPersonalModel.Nombre + " " + oPersonalModel.Apellido;
                                oPersonalModel.CodTipoDocumento = reader.IsDBNull(reader.GetOrdinal("CodTipoDocumento")) ? "" : reader.GetString(reader.GetOrdinal("CodTipoDocumento"));
                                oPersonalModel.NombreTipoDocumento = reader.IsDBNull(reader.GetOrdinal("NombreTipoDocumento")) ? "" : reader.GetString(reader.GetOrdinal("NombreTipoDocumento"));
                                oPersonalModel.NumeroDocumento = reader.IsDBNull(reader.GetOrdinal("NumeroDocumento")) ? "" : reader.GetString(reader.GetOrdinal("NumeroDocumento"));
                                oPersonalModel.Sexo = reader.IsDBNull(reader.GetOrdinal("Sexo")) ? 0 : reader.GetInt32(reader.GetOrdinal("Sexo"));
                                oPersonalModel.FechaNacimiento = reader.IsDBNull(reader.GetOrdinal("FechaNacimiento")) ? DateTime.Now : reader.GetDateTime(reader.GetOrdinal("FechaNacimiento"));
                                oPersonalModel.Direccion = reader.IsDBNull(reader.GetOrdinal("Direccion")) ? "" : reader.GetString(reader.GetOrdinal("Direccion"));
                                oPersonalModel.Telefono = reader.IsDBNull(reader.GetOrdinal("Telefono")) ? "" : reader.GetString(reader.GetOrdinal("Telefono"));
                                oPersonalModel.CorreoElectronico = reader.IsDBNull(reader.GetOrdinal("CorreoElectronico")) ? "" : reader.GetString(reader.GetOrdinal("CorreoElectronico"));
                                oPersonalModel.CodCentroCosto = reader.IsDBNull(reader.GetOrdinal("CodCentroCosto")) ? "" : reader.GetString(reader.GetOrdinal("CodCentroCosto"));
                                oPersonalModel.NombreCentroCosto = reader.IsDBNull(reader.GetOrdinal("NombreCentroCosto")) ? "" : reader.GetString(reader.GetOrdinal("NombreCentroCosto"));
                                oPersonalModel.CodArea = reader.IsDBNull(reader.GetOrdinal("CodArea")) ? "" : reader.GetString(reader.GetOrdinal("CodArea"));
                                oPersonalModel.NombreArea = reader.IsDBNull(reader.GetOrdinal("NombreArea")) ? "" : reader.GetString(reader.GetOrdinal("NombreArea"));
                                oPersonalModel.CodCargo = reader.IsDBNull(reader.GetOrdinal("CodCargo")) ? "" : reader.GetString(reader.GetOrdinal("CodCargo"));
                                oPersonalModel.NombreCargo = reader.IsDBNull(reader.GetOrdinal("NombreCargo")) ? "" : reader.GetString(reader.GetOrdinal("NombreCargo"));
                                oPersonalModel.FechaIngreso = reader.IsDBNull(reader.GetOrdinal("FechaIngreso")) ? DateTime.Now : reader.GetDateTime(reader.GetOrdinal("FechaIngreso"));
                                oPersonalModel.CodJefe = reader.IsDBNull(reader.GetOrdinal("CodJefe")) ? "" : reader.GetString(reader.GetOrdinal("CodJefe")).Trim();
                                oPersonalModel.NombreJefe = reader.IsDBNull(reader.GetOrdinal("NombreJefe")) ? "" : reader.GetString(reader.GetOrdinal("NombreJefe"));
                                oPersonalModel.ApellidoJefe = reader.IsDBNull(reader.GetOrdinal("ApellidoJefe")) ? "" : reader.GetString(reader.GetOrdinal("ApellidoJefe"));
                                oPersonalModel.Estado = reader.IsDBNull(reader.GetOrdinal("Estado")) ? 0 : reader.GetInt32(reader.GetOrdinal("Estado"));
                                oPersonalModel.CodEmpresa = reader.IsDBNull(reader.GetOrdinal("CodEmpresa")) ? "" : reader.GetString(reader.GetOrdinal("CodEmpresa"));
                                oPersonalModel.EstaBorrado = reader.IsDBNull(reader.GetOrdinal("EstaBorrado")) ? false : reader.GetBoolean(reader.GetOrdinal("EstaBorrado"));
                                listPersonalModel.Add(oPersonalModel);
                            }
                            return listPersonalModel;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return listPersonalModel;
            }
        }

        public int GrabarPersonal(PersonalModel oPersonalModel)
        {
            int result = 0;
            try
            {
                using (var cn = GetSqlConnection())
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(_grabar, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CodPersonal", oPersonalModel.CodPersonal);
                        cmd.Parameters.AddWithValue("@CodigoAnterior", oPersonalModel.CodigoAnterior == null ? "" : oPersonalModel.CodigoAnterior);
                        cmd.Parameters.AddWithValue("@Nombre", oPersonalModel.Nombre);
                        cmd.Parameters.AddWithValue("@Apellido", oPersonalModel.Apellido);
                        cmd.Parameters.AddWithValue("@CodTipoDocumento", oPersonalModel.CodTipoDocumento);
                        cmd.Parameters.AddWithValue("@NumeroDocumento", oPersonalModel.NumeroDocumento);
                        cmd.Parameters.AddWithValue("@Sexo", oPersonalModel.Sexo);
                        cmd.Parameters.AddWithValue("@FechaNacimiento", oPersonalModel.FechaNacimiento);
                        cmd.Parameters.AddWithValue("@Direccion", oPersonalModel.Direccion == null ? "" : oPersonalModel.Direccion);
                        cmd.Parameters.AddWithValue("@Telefono", oPersonalModel.Telefono == null ? "" : oPersonalModel.Telefono);
                        cmd.Parameters.AddWithValue("@CorreoElectronico", oPersonalModel.CorreoElectronico);
                        cmd.Parameters.AddWithValue("@CodCentroCosto", oPersonalModel.CodCentroCosto);
                        cmd.Parameters.AddWithValue("@CodArea", oPersonalModel.CodArea);
                        cmd.Parameters.AddWithValue("@CodCargo", oPersonalModel.CodCargo);
                        cmd.Parameters.AddWithValue("@FechaIngreso", oPersonalModel.FechaIngreso);
                        cmd.Parameters.AddWithValue("@CodJefe", oPersonalModel.CodJefe == null ? "" : oPersonalModel.CodJefe);
                        cmd.Parameters.AddWithValue("@Estado", oPersonalModel.Estado);
                        cmd.Parameters.AddWithValue("@CodEmpresa", oPersonalModel.CodEmpresa);
                        cmd.Parameters.AddWithValue("@EstaBorrado", oPersonalModel.EstaBorrado);
                        result = cmd.ExecuteNonQuery();
                        return result;
                    }
                }
            }
            catch (Exception)
            {
                return result;
            }
        }

        public PersonalModel RecuperarPersonal(string codPersonal, string codEmpresa)
        {
            PersonalModel oPersonalModel = new PersonalModel();
            try
            {
                using (var cn = GetSqlConnection())
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(_recuperar, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CodPersonal", codPersonal);
                        cmd.Parameters.AddWithValue("@CodEmpresa", codEmpresa);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                oPersonalModel.Correlativo = reader.IsDBNull(reader.GetOrdinal("Correlativo")) ? 0 : Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Correlativo")));
                                oPersonalModel.IdPersonal = reader.IsDBNull(reader.GetOrdinal("IdPersonal")) ? 0 : reader.GetInt32(reader.GetOrdinal("IdPersonal"));
                                oPersonalModel.CodPersonal = reader.IsDBNull(reader.GetOrdinal("CodPersonal")) ? "" : reader.GetString(reader.GetOrdinal("CodPersonal"));
                                oPersonalModel.Nombre = reader.IsDBNull(reader.GetOrdinal("Nombre")) ? "" : reader.GetString(reader.GetOrdinal("Nombre"));
                                oPersonalModel.Apellido = reader.IsDBNull(reader.GetOrdinal("Apellido")) ? "" : reader.GetString(reader.GetOrdinal("Apellido"));
                                oPersonalModel.NombreCompleto = oPersonalModel.Nombre + " " + oPersonalModel.Apellido;
                                oPersonalModel.CodTipoDocumento = reader.IsDBNull(reader.GetOrdinal("CodTipoDocumento")) ? "" : reader.GetString(reader.GetOrdinal("CodTipoDocumento"));
                                oPersonalModel.NombreTipoDocumento = reader.IsDBNull(reader.GetOrdinal("NombreTipoDocumento")) ? "" : reader.GetString(reader.GetOrdinal("NombreTipoDocumento"));
                                oPersonalModel.NumeroDocumento = reader.IsDBNull(reader.GetOrdinal("NumeroDocumento")) ? "" : reader.GetString(reader.GetOrdinal("NumeroDocumento"));
                                oPersonalModel.Sexo = reader.IsDBNull(reader.GetOrdinal("Sexo")) ? 0 : reader.GetInt32(reader.GetOrdinal("Sexo"));
                                oPersonalModel.FechaNacimiento = reader.IsDBNull(reader.GetOrdinal("FechaNacimiento")) ? DateTime.Now : reader.GetDateTime(reader.GetOrdinal("FechaNacimiento"));
                                oPersonalModel.Direccion = reader.IsDBNull(reader.GetOrdinal("Direccion")) ? "" : reader.GetString(reader.GetOrdinal("Direccion"));
                                oPersonalModel.Telefono = reader.IsDBNull(reader.GetOrdinal("Telefono")) ? "" : reader.GetString(reader.GetOrdinal("Telefono"));
                                oPersonalModel.CorreoElectronico = reader.IsDBNull(reader.GetOrdinal("CorreoElectronico")) ? "" : reader.GetString(reader.GetOrdinal("CorreoElectronico"));
                                oPersonalModel.CodCentroCosto = reader.IsDBNull(reader.GetOrdinal("CodCentroCosto")) ? "" : reader.GetString(reader.GetOrdinal("CodCentroCosto"));
                                oPersonalModel.NombreCentroCosto = reader.IsDBNull(reader.GetOrdinal("NombreCentroCosto")) ? "" : reader.GetString(reader.GetOrdinal("NombreCentroCosto"));
                                oPersonalModel.CodArea = reader.IsDBNull(reader.GetOrdinal("CodArea")) ? "" : reader.GetString(reader.GetOrdinal("CodArea"));
                                oPersonalModel.NombreArea = reader.IsDBNull(reader.GetOrdinal("NombreArea")) ? "" : reader.GetString(reader.GetOrdinal("NombreArea"));
                                oPersonalModel.CodCargo = reader.IsDBNull(reader.GetOrdinal("CodCargo")) ? "" : reader.GetString(reader.GetOrdinal("CodCargo"));
                                oPersonalModel.NombreCargo = reader.IsDBNull(reader.GetOrdinal("NombreCargo")) ? "" : reader.GetString(reader.GetOrdinal("NombreCargo"));
                                oPersonalModel.FechaIngreso = reader.IsDBNull(reader.GetOrdinal("FechaIngreso")) ? DateTime.Now : reader.GetDateTime(reader.GetOrdinal("FechaIngreso"));
                                oPersonalModel.CodJefe = reader.IsDBNull(reader.GetOrdinal("CodJefe")) ? "" : reader.GetString(reader.GetOrdinal("CodJefe")).Trim();
                                oPersonalModel.NombreJefe = reader.IsDBNull(reader.GetOrdinal("NombreJefe")) ? "" : reader.GetString(reader.GetOrdinal("NombreJefe"));
                                oPersonalModel.ApellidoJefe = reader.IsDBNull(reader.GetOrdinal("ApellidoJefe")) ? "" : reader.GetString(reader.GetOrdinal("ApellidoJefe"));
                                oPersonalModel.Estado = reader.IsDBNull(reader.GetOrdinal("Estado")) ? 0 : reader.GetInt32(reader.GetOrdinal("Estado"));
                                oPersonalModel.CodEmpresa = reader.IsDBNull(reader.GetOrdinal("CodEmpresa")) ? "" : reader.GetString(reader.GetOrdinal("CodEmpresa"));
                                oPersonalModel.EstaBorrado = reader.IsDBNull(reader.GetOrdinal("EstaBorrado")) ? false : reader.GetBoolean(reader.GetOrdinal("EstaBorrado"));
                            }
                            return oPersonalModel;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return oPersonalModel;
            }
        }

        public int EliminarPersonalFisico(string codPersonal, string codEmpresa)
        {
            int result = 0;
            try
            {
                using (var cn = GetSqlConnection())
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(_eliminar, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CodPersonal", codPersonal);
                        cmd.Parameters.AddWithValue("@CodEmpresa", codEmpresa);
                        result = cmd.ExecuteNonQuery();
                        return result;
                    }
                }
            }
            catch (Exception)
            {
                return result;
            }
        }

        public int EliminarPersonalLogico(PersonalModel oPersonalModel)
        {
            int result = 0;
            try
            {
                using (var cn = GetSqlConnection())
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(_grabar, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CodPersonal", oPersonalModel.CodPersonal);
                        cmd.Parameters.AddWithValue("@CodigoAnterior", oPersonalModel.CodigoAnterior == null ? "" : oPersonalModel.CodigoAnterior);
                        cmd.Parameters.AddWithValue("@Nombre", oPersonalModel.Nombre);
                        cmd.Parameters.AddWithValue("@Apellido", oPersonalModel.Apellido);
                        cmd.Parameters.AddWithValue("@CodTipoDocumento", oPersonalModel.CodTipoDocumento);
                        cmd.Parameters.AddWithValue("@NumeroDocumento", oPersonalModel.NumeroDocumento);
                        cmd.Parameters.AddWithValue("@Sexo", oPersonalModel.Sexo);
                        cmd.Parameters.AddWithValue("@FechaNacimiento", oPersonalModel.FechaNacimiento);
                        cmd.Parameters.AddWithValue("@Direccion", oPersonalModel.Direccion == null ? "" : oPersonalModel.Direccion);
                        cmd.Parameters.AddWithValue("@Telefono", oPersonalModel.Telefono == null ? "" : oPersonalModel.Telefono);
                        cmd.Parameters.AddWithValue("@CorreoElectronico", oPersonalModel.CorreoElectronico);
                        cmd.Parameters.AddWithValue("@CodCentroCosto", oPersonalModel.CodCentroCosto);
                        cmd.Parameters.AddWithValue("@CodArea", oPersonalModel.CodArea);
                        cmd.Parameters.AddWithValue("@CodCargo", oPersonalModel.CodCargo);
                        cmd.Parameters.AddWithValue("@FechaIngreso", oPersonalModel.FechaIngreso);
                        cmd.Parameters.AddWithValue("@CodJefe", oPersonalModel.CodJefe == null ? "" : oPersonalModel.CodJefe);
                        cmd.Parameters.AddWithValue("@Estado", oPersonalModel.Estado);
                        cmd.Parameters.AddWithValue("@CodEmpresa", oPersonalModel.CodEmpresa);
                        cmd.Parameters.AddWithValue("@EstaBorrado", oPersonalModel.EstaBorrado);
                        result = cmd.ExecuteNonQuery();
                        return result;
                    }
                }
            }
            catch (Exception)
            {
                return result;
            }
        }

        public void Dispose()
        {

        }
    }
}
