using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Defect
    {
        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}")]
        public DateTime DateCreated { get; set; }
        /*public int RegisteredUserId;
        public Employer RegisteredUser;
        public int InterceptedUserId;
        public Employer InterceptedUser;
        public int FixerUserId;
        public Employer FixerUser;
        public int CarrierUserId;
        public Employer CarrierUser;
        public int CarrierVehicleId;
        public Vehicle CarrierVehicle;*/
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}")]
        public DateTime TermDateTime { get; set; }
        /*public int MaterialToFixId;
        public Material MaterialToFix;
        public string ConstructionSiteId;
        public ConstructionSite ConstructionSite;*/
        public bool IsFixed { get; set; }
        public List<Photo> Photos { get; set; } = new List<Photo>() { new Photo() };

        public Defect()
        {

        }

        public Defect(MySqlDataReader reader)
        {
            Id = int.Parse(reader["Id"].ToString());
            DateCreated = DateTime.Parse(reader["DateAndTime"].ToString());
            TermDateTime = DateTime.Parse(reader["TermDateAndTime"].ToString());
            IsFixed = int.Parse(reader["IsFixed"].ToString()) == 1;
        }

    }

}
