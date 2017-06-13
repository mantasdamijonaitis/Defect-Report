using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabasesSecondLabotaroryAuth.Models.ComplexCreateModels
{
    public class VehicleCreateModel
    {
        public IEnumerable<Vehicle> Vehicles { get; set; }
        public IEnumerable<VehicleType> VehicleTypes { get; set; }
    }
}
