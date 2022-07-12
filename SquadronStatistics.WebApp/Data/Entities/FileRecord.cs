using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SquadronStatistics.WebApp.Data.Entities
{
    public class FileRecord
    {
        [Key]
        public int RowId { get; set; }
        public string Color { get; set; }
        public int Number { get; set; }
        public string Label { get; set; }

        public int FileId { get; set; }
        public virtual File File { get; set; }

    }
}
