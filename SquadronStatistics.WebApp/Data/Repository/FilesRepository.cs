using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SquadronStatistics.WebApp.Data.Entities;
using SquadronStatistics.WebApp.Data.Repository.Interfaces;
using SquadronStatistics.WebApp.Dtos;
using SquadronStatistics.WebApp.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SquadronStatistics.WebApp.Data.Repository
{
    public class FilesRepository : IFilesRepository
    {
        private readonly DataContext _context;
        public FilesRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<PagedList<FileDto>> GetFiles(PaginationParams fileParams)
        {
            var queryFiles = _context.Files
                .Select(file => new FileDto { Id = file.Id, Name = file.Name, UploadDate = file.UploadDate })
                .OrderByDescending(file => file.Id)
                .AsNoTracking();

            return await PagedList<FileDto>.Create(queryFiles, fileParams.PageNumber, fileParams.PageSize);
        }

        public async Task<FileUploadResponse> ProcessFile(Stream fileStream, string path)
        {
            try
            {
                List<Row> validRows = await FileValidationHelper.ValidateFileContent(fileStream);

                var rowsToSave = new List<FileRecord>();
                rowsToSave.AddRange(
                        validRows.Select(item => new FileRecord()
                        {
                            FileId = 1,
                            Color = item.Color.ToString(),
                            Number = item.Value,
                            Label = item.Label,
                        })
                );

                var fileName = Path.GetFileName(path);
                Entities.File newFile = new Entities.File() { Name = fileName, FileRecords = rowsToSave };
                _context.Files.Add(newFile);

                await _context.SaveChangesAsync();

                return new FileUploadResponse()
                {
                    FileId = newFile.Id,
                    FileName = fileName,
                    ValidRowsNumber = validRows.Count
                };  

            }
            catch (Exception e)
            {
                // log error 
            }

            return null;

        }

        public async Task<List<RowDto>> GetFileRows(int fileId)
        {
            var rowsQuery = this._context.FileRecords.Where(r => r.FileId == fileId)
                                        .Select(r => new RowDto() { RowId = r.RowId, Color = r.Color, Value = r.Number, Label = r.Label });

            return await rowsQuery.ToListAsync();
        }

        public async Task<FileDto> GetLastUploadedFile()
        {
            var maxId = await _context.Files.MaxAsync(f => f.Id);
            var lastFile = await this._context.Files.SingleAsync(f => f.Id == maxId);

            return new FileDto
            {
                Id = lastFile.Id,
                Name = lastFile.Name,
                UploadDate = lastFile.UploadDate
            };
        }

        public async Task<bool> FileUploadedExists()
        {
            return await _context.Files.AnyAsync();
        }
    }
}
