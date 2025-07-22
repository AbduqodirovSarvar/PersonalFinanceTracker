using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workers.Interfaces;
using Workers.Models;

namespace Workers.Services
{
    public class ExcelService : IExcelService
    {
        public Task<string> GenerateExcelFileAsync(StatisticExportRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
