using Application.Core;
using Application.DTOs;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.FileMeta
{
    public class List
    {
        public class Query : IRequest<Result<List<FileMetaDataDto>>>
        {
            public string AppUserId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<FileMetaDataDto>>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<List<FileMetaDataDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                // Fetch files related to the AppUserId
                var files = await _context.Files
                    .Where(x => x.AppUserId == request.AppUserId)
                     .Select(file => new FileMetaDataDto
                     {
                         Id = file.Id,
                         FileName = file.FileName,
                         Size = file.Size,
                         Format = file.Format,
                         UploadDate = file.UploadDate,
                         FilePath = file.FilePath,
                     })
                    .ToListAsync(cancellationToken);

                // Handle case when no files are found
                if (files.Count == 0)
                {
                    return null;
                }

                return Result<List<FileMetaDataDto>>.Success(files);
            }
        }
    }
}