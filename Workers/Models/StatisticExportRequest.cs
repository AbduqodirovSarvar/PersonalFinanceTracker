using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers.Models
{
    public class StatisticExportRequest
    {
        public int UserId { get; set; }
        public string ReportType { get; set; }
    }
}
