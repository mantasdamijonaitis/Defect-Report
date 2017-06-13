using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Employer
    {
        public int Id;
        public string FirstName;
        public string LastName;
        public int PositionId;
        public Position Position;
        public int SystemUserId;
        public SystemUser SystemUser;
        public int CompanyId;
        public Company Company;
    }
}
