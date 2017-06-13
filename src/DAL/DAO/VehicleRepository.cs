using DAL.DAO.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;
using MySql.Data.MySqlClient;
using DAL.DAO.Base;

namespace DAL.DAO
{
    public class VehicleRepository : IVehicleRepository
    {

        private readonly ICompanyRepository companyRepository;
        private readonly IVehicleTypeRepository vehicleTypeRepository;

        public VehicleRepository(IVehicleTypeRepository vehicleTypeRepository, 
            ICompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository;
            this.vehicleTypeRepository = vehicleTypeRepository;
        }

        public int Create(Vehicle t)
        {
            string query = string.Format(
                @"INSERT INTO test.vehicles (LicensePlates, CompanyId, VehicleTypeId) 
                  VALUES('{0}', {1}, {2});
                  SELECT * FROM test.vehicles WHERE LicensePlates='{0}' AND CompanyId={1} AND
                  VehicleTypeId={2};", t.LicensePlates, t.CompanyId, t.VehicleTypeId);
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                return reader.Read() && reader.HasRows ? 1 : -1;
            }
        }

        public bool Delete(int id)
        {
            string query = string.Format(@"DELETE FROM test.vehicles WHERE Id={0};
                                           SELECT * FROM test.vehicles WHERE Id={0}", id);
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                return !(reader.Read() && reader.HasRows);
            }
        }

        public bool Exists(Vehicle t)
        {
            string query = string.Format(
                @"SELECT * FROM test.vehicles WHERE LicensePlates='{0}' AND CompanyId={1} AND
                  VehicleTypeId={2}", t.LicensePlates, t.CompanyId, t.VehicleTypeId);
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                return reader.Read() && reader.HasRows;
            }
        }

        public Vehicle Get(int id)
        {
            string query = string.Format("SELECT * FROM test.vehicles WHERE Id={0}", id);
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                if (reader.Read() && reader.HasRows)
                {
                    Vehicle vehicle = new Vehicle(reader);
                    vehicle.Company = companyRepository.Get(vehicle.CompanyId);
                    vehicle.VehicleType = vehicleTypeRepository.Get(vehicle.VehicleTypeId);
                    return vehicle;
                }
                return null; 
            }
        }

        public List<Vehicle> GetAll()
        {
            string query = "SELECT * FROM test.vehicles";
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                List<Vehicle> vehicles = new List<Vehicle>();
                while (reader.Read() && reader.HasRows)
                {
                    Vehicle vehicle = new Vehicle(reader);
                    vehicle.Company = companyRepository.Get(vehicle.CompanyId);
                    vehicle.VehicleType = vehicleTypeRepository.Get(vehicle.VehicleTypeId);
                    vehicles.Add(vehicle);
                }
                return vehicles;
            }
        }

        public List<Vehicle> GetByLicensePlatesFragment(string fragment)
        {
            string query = string.Format(
                @"SELECT * FROM test.vehicles WHERE LicensePlates LIKE '%{0}%'", fragment);
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                List<Vehicle> results = new List<Vehicle>();
                while (reader.Read() && reader.HasRows)
                {
                    Vehicle vehicle = new Vehicle(reader);
                    vehicle.Company = companyRepository.Get(vehicle.CompanyId);
                    vehicle.VehicleType = vehicleTypeRepository.Get(vehicle.VehicleTypeId);
                    results.Add(vehicle);
                }
                return results;
            }
        }

        public bool Update(int id, Vehicle t)
        {
            string query = string.Format(
                @"UPDATE test.vehicles SET LicensePlates='{0}', CompanyId={1}, VehicleTypeId={2} 
                  WHERE Id={3};
                  SELECT * FROM test.vehicles WHERE LicensePlates='{0}' AND
                  CompanyId={1} AND VehicleTypeId={2}",
                t.LicensePlates, t.CompanyId, t.VehicleTypeId, id);
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                return reader.Read() && reader.HasRows;
            }
        }
    }
}
