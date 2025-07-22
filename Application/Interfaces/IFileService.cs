using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(byte[] fileBytes, string fileName);
        Task<byte[]?> GetFileAsync(string fileName);
        Task DeleteFileAsync(string fileName);
    }
}
