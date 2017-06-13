using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class MeasureUnit
    {
        public int Id { get; set;}
        public string Title { get; set; }

        public MeasureUnit()
        {

        }

        public MeasureUnit(MySqlDataReader reader)
        {
            Id = int.Parse(reader["Id"].ToString());
            Title = reader["MeasureUnit"].ToString();
        }

    }
}
