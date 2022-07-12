using Microsoft.AspNetCore.Http;
using SquadronStatistics.WebApp.Data.Entities;
using SquadronStatistics.WebApp.Dtos;
using SquadronStatistics.WebApp.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SquadronStatistics.WebApp.Data.Repository.Interfaces
{
    public interface IFilesRepository
    {
        Task<PagedList<FileDto>> GetFiles(PaginationParams fileParams);
        Task<FileUploadResponse> ProcessFile(Stream fileStream, string path);
        Task<List<RowDto>> GetFileRows(int fileId);
        Task<FileDto> GetLastUploadedFile();
        Task<bool> FileUploadedExists();
    }
}