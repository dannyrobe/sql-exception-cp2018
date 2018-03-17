using System;
using System.Collections.Generic;
using System.Text;

namespace SqlExceptionTalk.Models
{
    public class JobDataModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime? Date { get; set; }
        public decimal? Amount { get; set; }
    }
}
