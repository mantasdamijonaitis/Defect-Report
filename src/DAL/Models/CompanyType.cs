using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class CompanyType
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public CompanyType()
        {

        }

        public CompanyType(MySqlDataReader reader)
        {
            Id = int.Parse(reader["Id"].ToString());
            Title = reader["CompanyType"].ToString();
        }

    }
}
