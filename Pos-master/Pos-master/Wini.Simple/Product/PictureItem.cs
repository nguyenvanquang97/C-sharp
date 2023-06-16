using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Wini.Simple
{
    public class PictureItem
    {
        public int Id { get; set; }
        public int? AlbumId { get; set; }
        public int? CategoryId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool IsShow { get; set; }
        public bool? IsDeleted { get; set; }
        public int? SourceId { get; set; }
        public string Folder { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public string CateName { get; set; }
        public CategoryItem Category { get; set; }
        public int? AgencyId { get; set; }
        public IFormFile File { get; set; }
        public string LstFile { get; set; }
    }
    public class FileObj
    {
        public string FilePath { get; set; }
        public string Name { get; set; }
        public string NameRoot { get; set; }
        public string Forder { get; set; }
        public int Size { get; set; }
        public string Type { get; set; }
        public string Icon { get; set; }
        public bool Error { get; set; }
    }

    public class FileUploadItem
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }

}
