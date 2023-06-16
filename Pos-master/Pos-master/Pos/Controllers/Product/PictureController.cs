using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Web;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Wini.DA;
using Wini.DA.Cache;
using Wini.Database;
using Wini.Simple;
using Wini.Utils;

namespace Pos.Controllers
{
    public class PictureController : BaseController
    {
        private readonly ILogger<PictureController> _logger;
        private IPictureDa _pictureDa;
        private readonly ICacheService _cacheService;
        public PictureController(ILogger<PictureController> logger, IPictureDa pictureDa)
        {
            _logger = logger;
            _pictureDa = pictureDa;
        }
        [HttpPost]
        public async Task<BaseResponse<IList<PictureItem>>> GetAll([FromBody] BaseRequest request)
        {
            var model = await _pictureDa.GetListSimpleByRequest(request);
            return model;
        }
        [HttpPost]
        public async Task<BaseResponse<string>> Add([FromBody] PictureItem data)
        {
            var date = DateTime.Now;
            var response = new BaseResponse<string>() { Code = (int)ResponseCode.Nodata, Message = "Không có hình ảnh nào được thêm mới." };
            var folder = date.Year + "\\" + date.Month + "\\" + date.Day + "\\";
            var fileinsert = date.Year + "/" + date.Month + "/" + date.Day + "/";
            var folderinsert = fileinsert;
            var urlFolder = Directory.GetCurrentDirectory() + ConfigData.Temp;

            var lstP = JsonConvert.DeserializeObject<List<FileUploadItem>>(data.LstFile);
            try
            {
                foreach (var item in lstP)
                {
                    var fileName = item.Name;
                    CreateForder(ConfigData.Originals);
                    var urlSave = Directory.GetCurrentDirectory() + ConfigData.Originals + folder + fileName;
                    System.IO.File.Copy(urlFolder + fileName, urlSave);
                    var picture = new Picture
                    {
                        CategoryId =
                            data.CategoryId ?? 1,
                        Folder = folderinsert,
                        Name = item.Name,
                        DateCreated = DateTime.Now.TotalSeconds(),
                        IsShow = true,
                        Url = fileName,
                        IsDeleted = false
                    };
                    _pictureDa.Add(picture);
                }
                _pictureDa.Save();
                try
                {
                    var di = new DirectoryInfo(urlFolder);
                    foreach (var file in di.GetFiles())
                    {
                        file.Delete();
                    }
                    foreach (var dir in di.GetDirectories())
                    {
                        dir.Delete(true);
                    }
                }
                catch { }
                return BasiResponse.Success(string.Empty);
            }
            catch (Exception e)
            {
            return BasiResponse.Error(string.Empty );

            }
        }
        [HttpPost]
        public async Task<BaseResponse<List<FileObj>>> UploadFiles(IList<IFormFile> files)
        {
            var lst = new string[] { "jpg", "png" };
            var item = new List<FileObj>();
            var url = Directory.GetCurrentDirectory() + ConfigData.Temp;

            if (!Directory.Exists(url)) Directory.CreateDirectory(url);
            foreach (var file in files)
            {
                if (file.Length > 0 && file.FileName.Length > 0)
                {
                    var fileLocal = file.FileName.Split('.');

                    var fileName = FomatString.Slug(fileLocal[0]) + "-" + DateTime.Now.ToString("MMddHHmmss") + "." + fileLocal[1];

                    var filePath = Path.Combine((url), Path.GetFileName(fileName));

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    var tmp = new FileObj
                    {
                        Name = fileName,
                        NameRoot = fileLocal[0],
                        Forder = ConfigData.TempSouce,

                        Icon = "/Images/Icons/file/" + fileLocal[1] + ".png",
                        Error = false
                    };
                    item.Add(tmp);
                }
            }
            return new BaseResponse<List<FileObj>> { Code = (int)ResponseCode.Success, Data = item, Message = "Upload File thành công" };
        }
        public static void CreateForder(string link)
        {
            var forderyear = link + DateTime.Now.Year;
            var fordermonth = forderyear + "\\" + DateTime.Now.Month;
            var forderdate = fordermonth + "\\" + DateTime.Now.Day;
            if (!Directory.Exists(Directory.GetCurrentDirectory() + forderyear))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + forderyear);
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + fordermonth);
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + forderdate);
            }
            else
            {
                if (!Directory.Exists(Directory.GetCurrentDirectory() + fordermonth))
                {
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + fordermonth);
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + forderdate);
                }
                else
                {
                    if (!Directory.Exists(Directory.GetCurrentDirectory() + forderdate))
                    {
                        Directory.CreateDirectory(Directory.GetCurrentDirectory() + forderdate);
                    }
                }
            }
        }
        [HttpPost]
        public BaseResponse<Picture> Update([FromBody] Picture data)
        {
            var model = _pictureDa.Update(data);
            return model;
        }
        [HttpPost]

        public BaseResponse<int> Delete(int id)
        {
            var model = _pictureDa.GetbyId(id);
            if (model != null)
            {
                model.IsDeleted = true;
                _pictureDa.Save();
                return BasiResponse.Success(id);
            }
                                                return BasiResponse.Nodata(0);
        }
    }
}
