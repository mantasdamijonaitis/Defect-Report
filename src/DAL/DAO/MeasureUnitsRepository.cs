using DAL.DAO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Models;
using MySql.Data.MySqlClient;
using DAL.DAO.Base;

namespace DAL.DAO
{
    public class MeasureUnitsRepository : IMeasureUnitRepository
    {
        public int Create(MeasureUnit t)
        {
            string query = string.Format("INSERT INTO test.measureunits (MeasureUnit) VALUES ('{0}'); SELECT Id FROM test.measureunits WHERE MeasureUnit='{0}'", t.Title);
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                if (!reader.Read() || !reader.HasRows)
                {
                    return -1;
                }
                return int.Parse(reader["Id"].ToString());
            }
        }

        public bool Delete(int id)
        {
            string query = string.Format("DELETE FROM test.measureunits WHERE Id={0}; SELECT * FROM test.measureunits WHERE Id={0}", id);
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                if (reader.Read() || reader.HasRows)
                {
                    return false;
                }
                return true;
            }
        }

        public bool Exists(MeasureUnit t)
        {
            string query = string.Format("SELECT * FROM test.measureunits WHERE MeasureUnit='{0}'", t.Title);
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                return reader.Read() && reader.HasRows;
            }
        }

        public MeasureUnit Get(int id)
        {
            string query = string.Format("SELECT * FROM test.measureunits WHERE Id={0}", id);
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                if (reader.Read() && reader.HasRows)
                {
                    return new MeasureUnit(reader);
                }
                return null;
            }
        }

        public List<MeasureUnit> GetAll()
        {
            string query = string.Format("SELECT * FROM test.measureunits");
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                List<MeasureUnit> measureUnits = new List<MeasureUnit>();
                while (reader.Read() && reader.HasRows)
                {
                    measureUnits.Add(new MeasureUnit(reader));
                }
                return measureUnits;
            }
        }

        public bool Update(int id, MeasureUnit t)
        {
            string query = string.Format("UPDATE test.measureunits SET MeasureUnit='{0}' WHERE Id={1}; SELECT * FROM test.measureunits WHERE Id={1}", t.Title, id);
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                if (reader.Read() && reader.HasRows)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
