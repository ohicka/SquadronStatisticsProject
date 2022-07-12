using System.Drawing;

namespace SquadronStatistics.WebApp.Dtos
{
    public class RowDto
    {
        public int RowId { get; set; }
        public string Color { get; set; }
        public int Value { get; set; }
        public string Label { get; set; }
    }
}
