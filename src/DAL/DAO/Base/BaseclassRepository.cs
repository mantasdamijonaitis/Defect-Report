using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO.Base
{
    public interface BaseClassRepository<T>
    {
        int Create(T t);
        bool Update(int id, T t);
        T Get(int id);
        List<T> GetAll();
        bool Exists(T t);
        bool Delete(int id);
    }
}
