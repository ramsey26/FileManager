using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class FileMetaDataDto
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public long Size { get; set; }
        public string Format { get; set; }
        public DateTime UploadDate { get; set; }
        public string FilePath { get; set; }
    }
}