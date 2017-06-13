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
    public class CompanyRepository : ICompanyRepository
    {

        private ICompanyTypeRepository companyTypesRepository;

        public CompanyRepository(ICompanyTypeRepository companyTypesRepository)
        {
            this.companyTypesRepository = companyTypesRepository;
        }

        public int Create(Company t)
        {
            string query = string.Format(
                @"INSERT INTO test.companies (Title, CompanyCode, CompanyTypeId) VALUES ('{0}', '{1}', {2});
                SELECT * FROM test.companies WHERE Title='{0}' AND CompanyCode='{1}' AND CompanyTypeId={2}", t.Title, t.Code, t.CompanyTypeId);
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                return reader.Read() && reader.HasRows ? 1 : -1;
            }
        }

        public bool Delete(int id)
        {
            string query = string.Format("DELETE FROM test.companies WHERE Id={0}; SELECT * FROM test.companies WHERE Id={0}", id);
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                return (!reader.Read() && reader.HasRows);
            }
        }

        public bool Exists(Company t)
        {
            string query = string.Format("SELECT * FROM test.companies WHERE Title='{0}' AND CompanyCode='{1}' AND CompanyTypeId={2}", 
                t.Title, t.Code, t.CompanyTypeId); ;
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                return reader.Read() && reader.HasRows;
            }
        }

        public Company Get(int id)
        {
            string query = string.Format("SELECT * FROM test.companies WHERE Id = {0}", id);
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                if (reader.Read() && reader.HasRows)
                {
                    Company company = new Company(reader);
                    company.CompanyType = companyTypesRepository.Get(company.CompanyTypeId);
                    return company;
                }
                return null;
            }
        }

        public List<Company> GetAll()
        {
            string query = "SELECT * FROM test.companies";
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                List<Company> companies = new List<Company>();
                while (reader.Read() && reader.HasRows)
                {
                    Company company = new Company(reader);
                    company.CompanyType = companyTypesRepository.Get(company.CompanyTypeId);
                    companies.Add(company);
                }
                return companies;
            }
        }

        public bool Update(int id, Company t)
        {
            string query = string.Format(
                @"UPDATE test.companies SET Title='{0}', CompanyCode='{1}', CompanyTypeId={2} WHERE Id={3};
                  SELECT * FROM test.companies WHERE Title='{0}' AND CompanyCode='{1}' AND CompanyTypeId={2}", t.Title, t.Code, t.CompanyTypeId, id);
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                return reader.Read() && reader.HasRows;
            }
        }

    }
}
