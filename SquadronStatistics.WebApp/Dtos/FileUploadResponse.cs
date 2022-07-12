namespace SquadronStatistics.WebApp.Dtos
{
    public class FileUploadResponse
    {
        public int FileId { get; set; }
        public string FileName { get; set; }
        public int ValidRowsNumber { get; set; }
    }
}
