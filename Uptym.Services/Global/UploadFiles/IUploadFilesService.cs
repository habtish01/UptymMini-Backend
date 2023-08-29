﻿using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Uptym.Core.Interfaces;

namespace Uptym.Services.Global.UploadFiles
{
    public interface IUploadFilesService
    {
        Task<IResponseDTO> UploadFile(string path, IFormFile file, bool deleteOldFiles = false);
        Task<IResponseDTO> UploadFiles(string path, List<IFormFile> files, bool deleteOldFiles = false);
        string CopyFile(string oldAttachmentUrl, string newAttachmentUrl);
    }
}
