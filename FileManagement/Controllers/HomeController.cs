using FileManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.VisualBasic.FileIO;
using System.Diagnostics;

using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using FileManagement.Repository;

namespace FileManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly FileInterface _fileInterface;
        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment environment, FileInterface fileInterface)
        {
            _logger = logger;
            _environment = environment;
            _fileInterface = fileInterface;
        }




        public IActionResult Index()
        {
            var imageFiles = GetFiles("wwwroot/images", FileType.Image);
            var pdfFiles = GetFiles("wwwroot/pdfs", FileType.Pdf);

            var viewModel = new FileListViewModel
            {
                ImageFiles = imageFiles,
                PdfFiles = pdfFiles
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Upload(FileUploadModel model)
        {
            var check = _fileInterface.UploadFile(model);
            if (check == true)
            {
                TempData["Message"] = "File uploaded successfully!";

            }
            else
            {
                TempData["Message"] = "Please choose a file.";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(string fileName, FileType fileType)
        {
          var check =  _fileInterface.DeleteFile(fileName,fileType);
            if (check == true)
            {
                TempData["Message"] = $"{fileType} deleted successfully!";

            }
            else
            {
                TempData["Message"] = $"{fileType} not found."; 
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Download(string fileName, FileType fileType)
        {
            var folderPath = fileType == FileType.Image ? "wwwroot/images" : "wwwroot/pdfs";
           var filePath = Path.Combine(folderPath, fileName);
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            if (System.IO.File.Exists(filePath))
            {
                  return File(fileBytes, "application/octet-stream", fileName);
            }
            return RedirectToAction("Index");
        }



       /*

        [HttpGet]
        public IActionResult DownloadFile(string fileName, FileType fileType)
        {
            string folderPath = fileType == FileType.Image ? "wwwroot/images" : "wwwroot/pdfs";
            string filePath = Path.Combine(folderPath, fileName);

            if (System.IO.File.Exists(filePath))
            {
                var fileBytes = System.IO.File.ReadAllBytes(filePath);
                return File(fileBytes, "application/octet-stream", fileName);
            }
            return NotFound("File not found");

        }
               */
        public IEnumerable<FileViewModel> GetFiles(string folderPath, FileType fileType)
        {
            var fileProvider = new PhysicalFileProvider(Path.Combine(_environment.ContentRootPath, folderPath));
            var files = fileProvider.GetDirectoryContents(string.Empty)
                .Where(f => f.IsDirectory == false && (fileType == FileType.Image || f.Name.EndsWith(".pdf")))
                .Select(f => new FileViewModel
                {
                    FileName = f.Name,
                    FilePath = Path.Combine(folderPath, f.Name)
                });

            return files;
        }

        public enum FileType
        {
            Image,
            Pdf
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
