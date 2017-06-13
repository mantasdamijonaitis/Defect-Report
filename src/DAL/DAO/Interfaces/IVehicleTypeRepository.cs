using DAL.DAO.Base;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO.Interfaces
{
    public interface IVehicleTypeRepository : BaseClassRepository<VehicleType>
    {
        VehicleType GetByTitle(string title);
    }
}
