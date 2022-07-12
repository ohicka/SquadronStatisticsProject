using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SquadronStatistics.WebApp.Data.Entities
{
    public class File
    {
        //public File()
        //{
        //    FileRecords = new HashSet<FileRecord>();
        //}

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime UploadDate { get; set; } = DateTime.Now;
        public virtual ICollection<FileRecord> FileRecords { get; set; }
    }
}
