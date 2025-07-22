using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Files.Queries
{
    public class GetFileQueryHandler(IFileService fileService) : IRequestHandler<GetFileQuery, byte[]?>
    {
        private readonly IFileService _fileService = fileService;
        public Task<byte[]?> Handle(GetFileQuery request, CancellationToken cancellationToken)
        {
            var file = _fileService.GetFileAsync(request.FileName);
            return file ?? throw new FileNotFoundException($"File '{request.FileName}' not found.");
        }
    }
}
