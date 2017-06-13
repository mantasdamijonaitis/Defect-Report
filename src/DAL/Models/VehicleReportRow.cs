using DAL.DAO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class VehicleReportRow
    {
        public Vehicle Vehicle { get; set; }
        public List<DefectReportRow> DefectReportRows { get; set; }

    }
}
