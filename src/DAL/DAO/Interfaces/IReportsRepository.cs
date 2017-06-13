using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DAO.Interfaces
{
    public interface IReportsRepository
    {
        List<CompanyTypeReportRow> GetCompanyTypesReport();
        List<MeasureUnitReportRow> GetMeasureUnitsReport(int minAmount, int maxAmount);
        List<VehicleReportRow> GetVehicleReport(string licensePlatesFragment);
    }
}
