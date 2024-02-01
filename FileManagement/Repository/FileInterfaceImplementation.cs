using FileManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using static FileManagement.Controllers.HomeController;
using FileManagement.Repository;


namespace FileManagement.Repository
{
    public class FileInterfaceImplementation : FileInterface
    {
        public bool UploadFile(FileUploadModel model)
        {
            if (model.File != null && model.File.Length > 0)
            {
                string folderPath = model.File.ContentType.StartsWith("image/") ? "wwwroot/images" : "wwwroot/pdfs";
                string fileName = Path.Combine(folderPath, model.File.FileName);

                using (var stream = new FileStream(fileName, FileMode.Create))
                {
                    model.File.CopyTo(stream);
                }
                return true;
              
            }
            return false;

        }

      
        public bool DeleteFile(string fileName, FileType fileType)
        {
            string folderPath = fileType == FileType.Image ? "wwwroot/images" : "wwwroot/pdfs";
            string filePath = Path.Combine(folderPath, fileName);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
                return true;

            }
            return false;
           

        }
      /*  public IActionResult DownloadFile(string fileName, FileType fileType)
        {
            string folderPath = fileType == FileType.Image ? "wwwroot/images" : "wwwroot/pdfs";
            string filePath = Path.Combine(folderPath, fileName);

            if (File.Exists(filePath))
            {
                var fileBytes = System.IO.File.ReadAllBytes(filePath);
                return File(fileBytes, "application/octet-stream", fileName);
            }
            else
            {
                // Handle the case when the file does not exist
                return null;
            }
        }
      */


     





    }
}
