using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workers.Models;

namespace Workers.Interfaces
{
    public interface IExcelService
    {
        Task<string> GenerateExcelFileAsync(StatisticExportRequest request);
    }
}
