using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ConstructionSite
    {
        public int Id;
        public string Address;
        public Company Carrier;
        public int CarrierId;
        public Company Contractor;
        public int ContractorId;
        public Company Executor;
        public int ExecutorId;
    }
}
