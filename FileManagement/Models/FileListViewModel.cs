namespace FileManagement.Models
{
    public class FileListViewModel
    {
        public IEnumerable<FileViewModel> ImageFiles { get; set; }
        public IEnumerable<FileViewModel> PdfFiles { get; set; }
    }
}
