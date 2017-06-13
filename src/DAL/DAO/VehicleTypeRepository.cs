using DAL.DAO.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;
using DAL.DAO.Base;
using MySql.Data.MySqlClient;

namespace DAL.DAO
{
    public class VehicleTypeRepository : IVehicleTypeRepository
    {
        public int Create(VehicleType t)
        {
            string query = string.Format(
                @"INSERT INTO test.vehicletypes (VehicleType) VALUES ('{0}');
                  SELECT * FROM test.vehicletypes WHERE VehicleType='{0}'", t.Title);
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                return reader.Read() && reader.HasRows ? 1 : -1;
            }
        }

        public bool Delete(int id)
        {
            string query = string.Format(
                @"DELETE FROM test.vehicletypes WHERE Id = {0};
                  SELECT * FROM test.vehicletypes WHERE Id = {0}", id);
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                return !(reader.Read() && reader.HasRows);
            }
        }

        public bool Exists(VehicleType t)
        {
            string query = string.Format(
               @"SELECT * FROM test.vehicletypes WHERE VehicleType = '{0}'", t.Title);
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                return reader.Read() && reader.HasRows;
            }
        }

        public VehicleType Get(int id)
        {
            string query = string.Format("SELECT * FROM test.vehicletypes WHERE Id={0}", id);
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                return reader.Read() && reader.HasRows ? new VehicleType(reader) : null;
            }
        }

        public List<VehicleType> GetAll()
        {
            string query = "SELECT * FROM test.vehicletypes";
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                List<VehicleType> vehicleTypes = new List<VehicleType>();
                while (reader.Read() && reader.HasRows)
                {
                    vehicleTypes.Add(new VehicleType(reader));
                }
                return vehicleTypes;
            }
        }

        public VehicleType GetByTitle(string title)
        {
            string query = string.Format("SELECT * FROM test.vehicletypes WHERE VehicleType='{0}'",
                title);
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                if (reader.Read() && reader.HasRows)
                {
                    return new VehicleType(reader);
                }
                return null;
            }
        }

        public bool Update(int id, VehicleType t)
        {
            string query = string.Format(
                @"UPDATE test.vehicletypes SET VehicleType='{0}' WHERE Id={1};
                  SELECT * FROM test.vehicletypes WHERE VehicleType='{0}'", t.Title, id);
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                return reader.Read() && reader.HasRows;
            }
        }
    }
}
