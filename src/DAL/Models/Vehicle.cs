using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string LicensePlates { get; set; }
        public int CompanyId { get; set; }
        public int VehicleTypeId { get; set; }
        public Company Company { get; set; }
        public VehicleType VehicleType { get; set; }

        public Vehicle()
        {

        }

        public Vehicle(MySqlDataReader reader)
        {
            Id = int.Parse(reader["Id"].ToString());
            LicensePlates = reader["LicensePlates"].ToString();
            CompanyId = int.Parse(reader["CompanyId"].ToString());
            VehicleTypeId = int.Parse(reader["VehicleTypeId"].ToString());
        }

    }
}
