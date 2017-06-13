using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Creator { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}")]
        public DateTime CreatedAt { get; set; }
        public string Path { get; set; }
        public int DefectId { get; set; }
        public Defect Defect { get; set; }
        public IFormFile PhotoFile { get; set; }
        public bool NewlyInserted = false;

        public Photo()
        {

        }

        public Photo(MySqlDataReader reader)
        {
            Id = int.Parse(reader["Id"].ToString());
            Creator = reader["Creator"].ToString();
            CreatedAt = DateTime.Parse(reader["DateAndTime"].ToString());
            Path = reader["Path"].ToString();
            if (Path.Contains("wwwroot"))
            {
                Path = Path.Replace("wwwroot", string.Empty);
            }
            DefectId = int.Parse(reader["DefectId"].ToString());
        }

    }
}
