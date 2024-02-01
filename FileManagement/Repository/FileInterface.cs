using FileManagement.Models;
using Microsoft.AspNetCore.Mvc;
using static FileManagement.Controllers.HomeController;

namespace FileManagement.Repository
{
    public interface FileInterface
    {

        public bool UploadFile(FileUploadModel model);
        public bool DeleteFile(string fileName, FileType fileType);
      

    }
}
