using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class FileMetaData
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public long Size { get; set; }
        public string Format { get; set; }
        public DateTime UploadDate { get; set; }
        public string FilePath { get; set; }

        [ForeignKey("AppUser")]
        public string AppUserId { get; set; } // IdentityUser uses string as Id type by default

        // Navigation property
        public AppUser AppUser { get; set; }
    }
}