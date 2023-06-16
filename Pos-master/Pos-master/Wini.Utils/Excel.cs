

using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Pos;
using Wini.Simple;
using Wini.Utils;
namespace Wini.Utils
{
    public class Excel
    {
        public static HttpResponseMessage ExportListOrderDetail(StorageProductItem lst)
        {
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Sheet1");
            var properties = new[]
                    {
                        "STT",
                        "Tên sản phẩm",
                        "Mã sản phẩm",
                        "Đơn vị",
                        "Giá nhập",
                        "Giá bán",
                        "Barcode",
                        "Số lượng",

                    };
            worksheet.Range("A1:B1").Merge();
            worksheet.Cell(1, 8).Value = "CHI TIẾT PHIẾU NHẬP KHO NGÀY " + lst.DateImport.DecimalToString("dd/MM/yyyy");

            worksheet.Cell(1,8).Style.Font.Bold = true;
            worksheet.Cell(1, 8).Style.Font.FontSize = 16;
            worksheet.Cell(1, 8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Cell(1, 8).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            var row = 3;
            for (var i = 0; i < properties.Length; i++)
            {
                worksheet.Cell(row, i++).Value = properties[i];
                worksheet.Cell(row,i++).Style.Font.Bold = true;
                worksheet.Cell(row,i++).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            }

            row++;
            var stt = 1;
            foreach (var item in lst.ImportProductItem)
            {
                
                var col = 1;
                worksheet.Cell(row, col).Value = stt++;
                worksheet.Cell(row, col++).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Cell(row, col).Value = item.Name;
                worksheet.Cell(row, col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell(row, col).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                worksheet.Cell(row, col++).Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                worksheet.Cell(row, col++).Style.Border.OutsideBorderColor = XLColor.Black;

                worksheet.Cell(row, col).Value = item.CodeSku;
                worksheet.Cell(row, col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell(row, col).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                worksheet.Cell(row, col++).Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                worksheet.Cell(row, col++).Style.Border.OutsideBorderColor = XLColor.Black;

                worksheet.Cell(row, col).Value = item.UnitName;
                worksheet.Cell(row, col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell(row, col).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                worksheet.Cell(row, col++).Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                worksheet.Cell(row, col++).Style.Border.OutsideBorderColor = XLColor.Black;

                worksheet.Cell(row, col).Value = item.Price.Money();
                worksheet.Cell(row, col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell(row, col).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                worksheet.Cell(row, col++).Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                worksheet.Cell(row, col++).Style.Border.OutsideBorderColor = XLColor.Black;

                worksheet.Cell(row, col).Value = item.PriceNew.Money();
                worksheet.Cell(row, col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell(row, col).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                worksheet.Cell(row, col++).Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                worksheet.Cell(row, col++).Style.Border.OutsideBorderColor = XLColor.Black;

                worksheet.Cell(row, col).Value = item.BarCode;
                worksheet.Cell(row, col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell(row, col).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                worksheet.Cell(row, col++).Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                worksheet.Cell(row, col++).Style.Border.OutsideBorderColor = XLColor.Black;

                worksheet.Cell(row, col).Value = item.Quantity.Quantity("0:0.####");
                worksheet.Cell(row, col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell(row, col).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                row++;
            }
            var nameexcel = "Chi tiết phiếu nhập kho ngày" + lst.DateImport.DecimalToString("dd/MM/yyyy");
            workbook.Properties.Title = string.Format("{0} reports", nameexcel);
            workbook.Properties.Author = "Admin-IT";
            workbook.Properties.Subject = "reports";
            workbook.Properties.Category = "Report";
            workbook.Properties.Company = "WINI";
            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Seek(0, SeekOrigin.Begin);
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "chi-tiet-phieu-nhap-ngay-"+ lst.DateImport.DecimalToString("dd/MM/yyyy") + ".xlsx"
            };
            return result;
            //}
        }
    }
}
