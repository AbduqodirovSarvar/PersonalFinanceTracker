using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly string _rootFolder = Path.Combine(Directory.GetCurrentDirectory(), "GeneratedFiles");

        public FileService()
        {
            if (!Directory.Exists(_rootFolder))
                Directory.CreateDirectory(_rootFolder);
        }

        public async Task<string> SaveFileAsync(byte[] fileBytes, string fileName)
        {
            var filePath = Path.Combine(_rootFolder, fileName);
            await File.WriteAllBytesAsync(filePath, fileBytes);
            return fileName;
        }

        public async Task<byte[]?> GetFileAsync(string fileName)
        {
            var filePath = Path.Combine(_rootFolder, fileName);

            if (!File.Exists(filePath))
                return null;

            return await File.ReadAllBytesAsync(filePath);
        }

        public async Task DeleteFileAsync(string fileName)
        {
            var filePath = Path.Combine(_rootFolder, fileName);
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }
    }
}
