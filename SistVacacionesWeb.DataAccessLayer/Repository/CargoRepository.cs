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
    public class CargoRepository : Repository, ICargoRepository, IDisposable
    {
        private readonly string _listar;
        private readonly string _grabar;
        private readonly string _recuperar;
        private readonly string _eliminar;

        public CargoRepository()
        {
            _listar = "spListarCargo";
            _grabar = "spGrabarCargo";
            _recuperar = "spRecuperarCargo";
            _eliminar = "spEliminarCargo";
        }

        public List<CargoModel> ListarCargo(string codEmpresa)
        {
            List<CargoModel> listCargoModel = new List<CargoModel>();
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
                                CargoModel oCargoModel = new CargoModel();
                                oCargoModel.Correlativo = reader.IsDBNull(reader.GetOrdinal("Correlativo")) ? 0 : Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Correlativo")));
                                oCargoModel.IdCargo = reader.IsDBNull(reader.GetOrdinal("IdCargo")) ? 0 : reader.GetInt32(reader.GetOrdinal("IdCargo"));
                                oCargoModel.CodCargo = reader.IsDBNull(reader.GetOrdinal("CodCargo")) ? "" : reader.GetString(reader.GetOrdinal("CodCargo"));
                                oCargoModel.Nombre = reader.IsDBNull(reader.GetOrdinal("Nombre")) ? "" : reader.GetString(reader.GetOrdinal("Nombre"));
                                oCargoModel.CodArea = reader.IsDBNull(reader.GetOrdinal("CodArea")) ? "" : reader.GetString(reader.GetOrdinal("CodArea"));
                                oCargoModel.NombreArea = reader.IsDBNull(reader.GetOrdinal("NombreArea")) ? "" : reader.GetString(reader.GetOrdinal("NombreArea"));
                                oCargoModel.CodCentroCosto = reader.IsDBNull(reader.GetOrdinal("CodCentroCosto")) ? "" : reader.GetString(reader.GetOrdinal("CodCentroCosto"));
                                oCargoModel.NombreCentroCosto = reader.IsDBNull(reader.GetOrdinal("NombreCentroCosto")) ? "" : reader.GetString(reader.GetOrdinal("NombreCentroCosto"));
                                oCargoModel.Estado = reader.IsDBNull(reader.GetOrdinal("Estado")) ? 0 : reader.GetInt32(reader.GetOrdinal("Estado"));
                                oCargoModel.CodEmpresa = reader.IsDBNull(reader.GetOrdinal("CodEmpresa")) ? "" : reader.GetString(reader.GetOrdinal("CodEmpresa"));
                                oCargoModel.EstaBorrado = reader.IsDBNull(reader.GetOrdinal("EstaBorrado")) ? false : reader.GetBoolean(reader.GetOrdinal("EstaBorrado"));
                                listCargoModel.Add(oCargoModel);
                            }
                            return listCargoModel;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return listCargoModel;
            }
        }

        public int GrabarCargo(CargoModel oCargoModel)
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
                        cmd.Parameters.AddWithValue("@CodCargo", oCargoModel.CodCargo);
                        cmd.Parameters.AddWithValue("@Nombre", oCargoModel.Nombre);
                        cmd.Parameters.AddWithValue("@CodArea", oCargoModel.CodArea);
                        cmd.Parameters.AddWithValue("@Estado", oCargoModel.Estado);
                        cmd.Parameters.AddWithValue("@CodEmpresa", oCargoModel.CodEmpresa);
                        cmd.Parameters.AddWithValue("@EstaBorrado", oCargoModel.EstaBorrado);
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

        public CargoModel RecuperarCargo(string codCargo, string codEmpresa)
        {
            CargoModel oCargoModel = new CargoModel();
            try
            {
                using (var cn = GetSqlConnection())
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(_recuperar, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CodCargo", codCargo);
                        cmd.Parameters.AddWithValue("@CodEmpresa", codEmpresa);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                oCargoModel.Correlativo = reader.IsDBNull(reader.GetOrdinal("Correlativo")) ? 0 : Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Correlativo")));
                                oCargoModel.IdCargo = reader.IsDBNull(reader.GetOrdinal("IdCargo")) ? 0 : reader.GetInt32(reader.GetOrdinal("IdCargo"));
                                oCargoModel.CodCargo = reader.IsDBNull(reader.GetOrdinal("CodCargo")) ? "" : reader.GetString(reader.GetOrdinal("CodCargo"));
                                oCargoModel.Nombre = reader.IsDBNull(reader.GetOrdinal("Nombre")) ? "" : reader.GetString(reader.GetOrdinal("Nombre"));
                                oCargoModel.CodArea = reader.IsDBNull(reader.GetOrdinal("CodArea")) ? "" : reader.GetString(reader.GetOrdinal("CodArea"));
                                oCargoModel.NombreArea = reader.IsDBNull(reader.GetOrdinal("NombreArea")) ? "" : reader.GetString(reader.GetOrdinal("NombreArea"));
                                oCargoModel.CodCentroCosto = reader.IsDBNull(reader.GetOrdinal("CodCentroCosto")) ? "" : reader.GetString(reader.GetOrdinal("CodCentroCosto"));
                                oCargoModel.NombreCentroCosto = reader.IsDBNull(reader.GetOrdinal("NombreCentroCosto")) ? "" : reader.GetString(reader.GetOrdinal("NombreCentroCosto"));
                                oCargoModel.Estado = reader.IsDBNull(reader.GetOrdinal("Estado")) ? 0 : reader.GetInt32(reader.GetOrdinal("Estado"));
                                oCargoModel.CodEmpresa = reader.IsDBNull(reader.GetOrdinal("CodEmpresa")) ? "" : reader.GetString(reader.GetOrdinal("CodEmpresa"));
                                oCargoModel.EstaBorrado = reader.IsDBNull(reader.GetOrdinal("EstaBorrado")) ? false : reader.GetBoolean(reader.GetOrdinal("EstaBorrado"));
                            }
                            return oCargoModel;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return oCargoModel;
            }
        }

        public int EliminarCargoFisico(string codCargo, string codEmpresa)
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
                        cmd.Parameters.AddWithValue("@CodCargo", codCargo);
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

        public int EliminarCargoLogico(CargoModel oCargoModel)
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
                        cmd.Parameters.AddWithValue("@CodCargo", oCargoModel.CodCargo);
                        cmd.Parameters.AddWithValue("@Nombre", oCargoModel.Nombre);
                        cmd.Parameters.AddWithValue("@CodArea", oCargoModel.CodArea);
                        cmd.Parameters.AddWithValue("@Estado", oCargoModel.Estado);
                        cmd.Parameters.AddWithValue("@CodEmpresa", oCargoModel.CodEmpresa);
                        cmd.Parameters.AddWithValue("@EstaBorrado", oCargoModel.EstaBorrado);
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
