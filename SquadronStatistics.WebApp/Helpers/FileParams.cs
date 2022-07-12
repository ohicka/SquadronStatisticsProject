namespace SquadronStatistics.WebApp.Helpers
{
    public class FileParams: PaginationParams
    {
        public string OrderBy { get; set; } = "uploadDate";
    }
}
