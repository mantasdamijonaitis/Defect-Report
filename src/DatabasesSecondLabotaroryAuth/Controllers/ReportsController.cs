using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DAL.DAO.Interfaces;
using DAL.Models;

namespace DatabasesSecondLabotaroryAuth.Controllers
{
    public class ReportsController : Controller
    {

        private readonly IReportsRepository reportsRepository;

        public ReportsController(IReportsRepository reportsRepository)
        {
            this.reportsRepository = reportsRepository;
        }

        public IActionResult CompanyTypes()
        {
            List<CompanyTypeReportRow> results = reportsRepository.GetCompanyTypesReport();
            return View(results);
        }

        public IActionResult MeasureUnits(int? minAmount, int? maxAmount)
        {
            if (minAmount.HasValue && maxAmount.HasValue)
            {
                List<MeasureUnitReportRow> results = reportsRepository.GetMeasureUnitsReport(minAmount.Value, maxAmount.Value);
                return View(results);
            }
            return View();
        }

        public IActionResult Vehicles(string fragment)
        {
            if (!string.IsNullOrEmpty(fragment))
            {
                return View(reportsRepository.GetVehicleReport(fragment));
            }
            return View();
        }

    }
}