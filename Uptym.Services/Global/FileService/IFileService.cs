using System.Collections.Generic;
using Uptym.Core.Common;
using Uptym.DTO.Equipment.Equipment;

namespace Uptym.Services.Global.FileService
{
    public interface IFileService<T>
    {
        GeneratedFile ExportToExcel(List<T> arg);
    }
}
