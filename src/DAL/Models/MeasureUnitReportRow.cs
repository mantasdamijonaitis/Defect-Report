using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class MeasureUnitReportRow
    {
        public string MeasureUnit { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal MinAmount { get; set; }
        public decimal MaxAmount { get; set; }

        public MeasureUnitReportRow()
        {

        }

        public MeasureUnitReportRow(MySqlDataReader reader)
        {
            MeasureUnit = reader["MeasureUnit"].ToString();
            decimal totalAmount, minAmount, maxAmount = 0;
            bool totalAmountSuccess = decimal.TryParse(reader["TotalAmount"].ToString(), out totalAmount);
            bool minAmountSuccess = decimal.TryParse(reader["MinAmount"].ToString(), out minAmount);
            bool maxAmountSuccess = decimal.TryParse(reader["MaxAmount"].ToString(), out maxAmount);
            TotalAmount = totalAmountSuccess ? totalAmount : 0;
            MinAmount = minAmountSuccess ? minAmount : 0;
            MaxAmount = maxAmountSuccess ? maxAmount : 0;
        }

    }

}
