using DAL.DAO.Base;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO.Interfaces
{
    public interface IPhotoRepository : BaseClassRepository<Photo>
    {
        List<Photo> GetPhotosByDefectId(int defectId);
        bool DeletePhotosOfDefect(int defectId);
    }
}
