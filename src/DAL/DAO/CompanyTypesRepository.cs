using DAL.DAO.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;
using MySql.Data.MySqlClient;
using DAL.DAO.Base;

namespace DAL.DAO
{
    public class CompanyTypesRepository : ICompanyTypeRepository
    {
        public int Create(CompanyType t)
        {
            string query = string.Format("INSERT INTO test.companytypes (CompanyType) VALUES ('{0}'); SELECT * FROM test.companytypes WHERE CompanyType='{0}'",
                t.Title);
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                return reader.Read() && reader.HasRows ? 1 : -1;
            }
        }

        public bool Delete(int id)
        {
            string query = string.Format("DELETE FROM test.companytypes WHERE Id={0}; SELECT * FROM CompanyTypes WHERE Id={0}", id);
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                return !(reader.Read() && reader.HasRows);
            }
        }

        public bool Exists(CompanyType t)
        {
            string query = string.Format("SELECT * FROM test.companytypes WHERE CompanyType='{0}'", t.Title);
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                return reader.Read() && reader.HasRows;
            }
        }

        public CompanyType Get(int id)
        {
            string query = string.Format("SELECT * FROM test.companytypes WHERE Id='{0}'", id);
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                if (reader.Read() && reader.HasRows)
                {
                    return new CompanyType(reader);
                }
                return null;
            }
        }

        public List<CompanyType> GetAll()
        {
            string query = "SELECT * FROM test.companytypes";
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                List<CompanyType> companyTypes = new List<CompanyType>();
                while (reader.Read() && reader.HasRows)
                {
                    companyTypes.Add(new CompanyType(reader));
                }
                return companyTypes;
            }
        }

        public bool Update(int id, CompanyType t)
        {
            string query = string.Format("UPDATE test.companytypes SET CompanyType='{0}' WHERE Id={1}; SELECT * FROM test.companytypes WHERE CompanyType='{0}'",
                t.Title, id);
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                return reader.Read() && reader.HasRows;
            }
        }
    }
}
