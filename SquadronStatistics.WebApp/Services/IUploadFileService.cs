using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace SquadronStatistics.WebApp.Services
{
    public interface IUploadFileService
    {
        Task<string> SaveFileToLocalDirectory(IFormFile file, string fileName);
    }
}
