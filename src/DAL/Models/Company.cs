using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public int CompanyTypeId { get; set; }
        public CompanyType CompanyType { get; set; }

        public Company()
        {

        }

        public Company(MySqlDataReader reader)
        {
            Id = int.Parse(reader["Id"].ToString());
            Title = reader["Title"].ToString();
            Code = reader["CompanyCode"].ToString();
            CompanyTypeId = int.Parse(reader["CompanyTypeId"].ToString());
        }

    }
}
