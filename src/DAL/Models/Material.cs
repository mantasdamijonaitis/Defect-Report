using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Material
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Amount { get; set; }
        public int MeasureUnitId { get; set; }
        public MeasureUnit MeasureUnit { get; set; }

        public Material()
        {

        }

        public Material(MySqlDataReader reader)
        {
            Id = int.Parse(reader["Id"].ToString());
            Title = reader["Title"].ToString();
            Amount = int.Parse(reader["Amount"].ToString());
            MeasureUnitId = int.Parse(reader["MeasureUnitId"].ToString());
        }

    }
}
