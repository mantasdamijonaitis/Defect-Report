using DAL.DAO.Base;
using DAL.DAO.Interfaces;
using DAL.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DAO
{
    public class ReportsRepository : IReportsRepository
    {

        private readonly IVehicleRepository vehicleRepository;

        public ReportsRepository(IVehicleRepository vehicleRepository)
        {
            this.vehicleRepository = vehicleRepository;
        }

        public List<CompanyTypeReportRow> GetCompanyTypesReport()
        {
            string query =
                @"select CompanyT.CompanyType,
	                (select COUNT(*) from test.companies as Company where Company.CompanyTypeId = CompanyT.Id) as CompanyAmount,
	                (select COUNT(Vehicle.LicensePlates) from test.companies as Company, test.vehicles as Vehicle where Company.CompanyTypeId = CompanyT.Id and
																					Vehicle.CompanyId = Company.Id) as VehicleAmount
                  from test.companytypes as CompanyT
                  group by CompanyT.CompanyType";
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                List<CompanyTypeReportRow> results = new List<CompanyTypeReportRow>();
                while (reader.Read() && reader.HasRows)
                {
                    results.Add(new CompanyTypeReportRow(reader));
                }
                return results;
            }
        }

        public List<MeasureUnitReportRow> GetMeasureUnitsReport(int minAmount, int maxAmount)
        {
            string query = string.Format(
            @"select MeasureU.MeasureUnit,
	            (select SUM(Material.Amount) from test.materials as Material where Material.MeasureUnitId = MeasureU.Id AND Material.Amount >= {0} AND Material.Amount <= {1}) as TotalAmount,
	            (select MIN(Material.Amount) from test.materials as Material where Material.MeasureUnitId = MeasureU.Id AND Material.Amount >= {0} AND Material.Amount <= {1}) as MinAmount,
	            (select MAX(Material.Amount) from test.materials as Material where Material.MeasureUnitId = MeasureU.Id AND Material.Amount >= {0} AND Material.Amount <= {1}) as MaxAmount
            from test.measureunits as MeasureU
            group by MeasureU.MeasureUnit", minAmount, maxAmount);
            using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
            {
                List<MeasureUnitReportRow> results = new List<MeasureUnitReportRow>();
                while (reader.Read() && reader.HasRows) {
                    MeasureUnitReportRow row = new MeasureUnitReportRow(reader);
                    if (row.MinAmount >= minAmount && row.MaxAmount <= maxAmount)
                    {
                        results.Add(row);
                    }
                }
                return results;
            }
        }

        public List<VehicleReportRow> GetVehicleReport(string licensePlatesFragment)
        {
            List<VehicleReportRow> results = new List<VehicleReportRow>();
            List<Vehicle> vehicles = vehicleRepository.GetByLicensePlatesFragment(licensePlatesFragment);
            string queryFormat = @"select Defect.DateAndTime, Defect.TermDateAndTime, Defect.IsFixed,
	                                (select COUNT(*) from test.Photos as Photo where Photo.DefectId = Defect.Id) AS PhotoCount
                                   from test.defects as Defect where CarrierVehicleId={0}";
            foreach (Vehicle vehicle in vehicles)
            {
                string query = string.Format(queryFormat, vehicle.Id);
                List<DefectReportRow> defectReportRows = new List<DefectReportRow>();
                using (MySqlDataReader reader = DatabaseConnector.ExecuteSql(query))
                {
                    while (reader.Read() && reader.HasRows)
                    {
                        defectReportRows.Add(new DefectReportRow(reader));
                    }
                    results.Add(new VehicleReportRow()
                    {
                        Vehicle = vehicle,
                        DefectReportRows = defectReportRows
                    });
                }
            }
            return results;
        }
    }
}
