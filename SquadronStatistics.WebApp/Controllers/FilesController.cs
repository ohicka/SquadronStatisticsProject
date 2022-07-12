using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SquadronStatistics.WebApp.Data.Repository.Interfaces;
using SquadronStatistics.WebApp.Dtos;
using SquadronStatistics.WebApp.Extensions;
using SquadronStatistics.WebApp.Helpers;
using SquadronStatistics.WebApp.Services;
using System;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SquadronStatistics.WebApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FilesController : ControllerBase
    {

        private readonly IFilesRepository _filesRepository;
        private readonly IUploadFileService _uploadFileService;

        public FilesController(IFilesRepository filesRepository, IUploadFileService uploadFileService)
        {
            _filesRepository = filesRepository;
            _uploadFileService = uploadFileService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetUploadedFilesInfo([FromQuery]PaginationParams fileParams)
        {
            var files = await _filesRepository.GetFiles(fileParams);

            Response.AddPaginationHeader(files.CurrentPage, files.PageSize, files.TotalCount, files.TotalPages);

            return Ok(files);
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile()
        {
            if (!Request.HasFormContentType)
            {
                return BadRequest("Content type is differnet from the expected form-data!");
            }

            var formCollection = await Request.ReadFormAsync();
            var uploadedFile = formCollection.Files.First();

            if (uploadedFile.Length == 0)
            {
                return BadRequest("The file is empty");
            }

            if (!ContentDispositionHeaderValue.TryParse(uploadedFile.ContentDisposition, out var contentDisposition))
            {
                return BadRequest("Not valid content disposition");
            }

            var fileName = contentDisposition.FileName.Trim('"');
            var newFileName = DateTime.Now.ToString("yyyyMMdd") + "_" + Guid.NewGuid().ToString() + "_" + fileName;

            var path = await _uploadFileService.SaveFileToLocalDirectory(uploadedFile, newFileName);

            if (path == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            FileUploadResponse processResponse;
            // validate content of the file and save to database
            using (var fileStream = uploadedFile.OpenReadStream())
            {
                processResponse = await _filesRepository.ProcessFile(fileStream, path);
            }

            if (processResponse == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(processResponse);
        }   


        public async Task<IActionResult> GetFileRows(int fileId)
        {
            if (fileId == 0)
                return BadRequest("Not valid file id.");

            var rows = await _filesRepository.GetFileRows(fileId);

            if (rows == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(rows);
        }

        public async Task<IActionResult> GetLastUploadedFileFromDb()
        {
            var file = await _filesRepository.GetLastUploadedFile();

            if (file == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(file);
        }

        public async Task<IActionResult> CheckExistingFilesInDb()
        {
            var notEmptyDb = await this._filesRepository.FileUploadedExists();
            return Ok(notEmptyDb);
        }

    }
}
