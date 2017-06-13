using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DAO
{
    public class DefectReportRow
    {
        public string DateAndTime { get; set; }
        public string TermDateAndTime { get; set; }
        public int PhotoCount { get; set; }
        public bool IsFixed { get; set; }

        public DefectReportRow()
        {

        }

        public DefectReportRow(MySqlDataReader reader)
        {
            DateAndTime = reader["DateAndTime"].ToString();
            TermDateAndTime = reader["TermDateAndTime"].ToString();
            PhotoCount = int.Parse(reader["PhotoCount"].ToString());
            IsFixed = reader["IsFixed"].ToString() == "1";
        }

    }
}
