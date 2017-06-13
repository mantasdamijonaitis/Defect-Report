using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class CompanyTypeReportRow
    {
        public string CompanyType { get; set; }
        public int CompanyAmount { get; set; }
        public int VehicleAmount { get; set; }

        public CompanyTypeReportRow()
        {

        }

        public CompanyTypeReportRow(MySqlDataReader reader)
        {
            CompanyType = reader["CompanyType"].ToString();
            CompanyAmount = int.Parse(reader["CompanyAmount"].ToString());
            VehicleAmount = int.Parse(reader["VehicleAmount"].ToString());
        }

    }
}
