using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.BL.AccountBL;
using MISA.AMIS.BL.PaymentBL;
using MISA.AMIS.Common.Entities;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Drawing;

namespace MISA.AMIS.API.Controllers
{
    public class PaymentsController : BasesController<Payment>
    {
        #region Feild

        private IPaymentBL _paymentBL;

        #endregion

        #region Constructor

        public PaymentsController(IPaymentBL paymentBL) : base(paymentBL)
        {
            _paymentBL = paymentBL;
        }

        #endregion

        /// <summary>
        /// Lấy số phiếu chi mới
        /// </summary>
        /// <returns>số mới</returns>
        /// Created by: NHNam (8/1/2023)
        [HttpGet("NewRefNo")]
        public IActionResult GetNewEmployeeCode()
        {
            try
            {
                string newRefNo = _paymentBL.GetNewNoRef();
                return StatusCode(StatusCodes.Status200OK, newRefNo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex);

            }

        }

        /// <summary>
        /// Xóa hàng loạt
        /// </summary>
        /// <param name="ids">Mảng id</param>
        /// <returns>số bản ghi xóa</returns>
        /// CreatedBy: NHNam (14/2/2023)
        [HttpDelete("DeleteMultiple")]
        public ActionResult<int> DeleteMultiple([FromBody] IEnumerable<Guid> ids)
        {
            var numDeleted = 0;
            try
            {
                // Gọi phương thức xóa hàng loạt trong tầng Business layer
                numDeleted = _paymentBL.DeleteMultiple(ids);
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu xảy ra lỗi trong quá trình xóa
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return Ok(numDeleted);

        }

        /// <summary>
        /// Hàm xuất danh sách ra file Excel
        /// </summary>
        /// <returns>File Excel</returns>
        /// Created by: NHNam (15/2/2023)
        /// 
        [HttpGet("ExportExcel")]
        public IActionResult DownLoadExcel(
            [FromQuery] string? keyword = "",

            [FromQuery] int pageSize = 10,

            [FromQuery] int pageNumber = 1)
        {
            try
            {
                List<Payment> data = _paymentBL.ExportToExcel(pageNumber, pageSize, keyword);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                // Tạo Excel package
                using (var package = new ExcelPackage())
                {
                    // Tạo mới 1 worksheet
                    var worksheet = package.Workbook.Worksheets.Add("Payments");

                    // Căn chỉnh chung
                    worksheet.Cells["A:I"].Style.Font.SetFromFont("Times New Roman", 11);
                    worksheet.Cells["A:I"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Cells["A:I"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    // Căn chỉnh Tiêu đề
                    worksheet.Cells["A1:I1"].Merge = true;
                    worksheet.Cells["A1"].Value = "THU CHI TIỀN MẶT";
                    worksheet.Row(1).Height = 24;
                    worksheet.Cells["A1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    worksheet.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells["A1"].Style.Font.Bold = true;
                    worksheet.Cells["A1"].Style.Font.Size = 16;
                    worksheet.Cells["A1"].Style.Font.Name = "Arial";
                    worksheet.Cells["A2:I2"].Merge = true;
                    worksheet.Cells["A3:I3"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells["A3:I3"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                    // Căn chỉnh header
                    List<string> lstHeader = new List<string>()
                    {
                        "A3","B3","C3","D3","E3","F3","G3","H3","I3"
                    };
                    lstHeader.ForEach(c =>
                    {
                        worksheet.Cells[c].Style.Font.Bold = true;
                        worksheet.Cells[c].Style.Font.Name = "Arial";
                        worksheet.Cells[c].Style.Font.Size = 10;
                        worksheet.Cells[c].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells[c].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[c].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[c].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[c].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                    });
                    //for(int i =1; i <= lstHeader.Count(); i++)
                    //{
                    //    worksheet.Column(i).Width = 20;
                    //}

                    // Thêm hàng Header
                    worksheet.Cells[3, 1].Value = "STT";
                    worksheet.Cells[3, 2].Value = "Ngày hạch toán";
                    worksheet.Cells[3, 3].Value = "Ngày chứng từ";
                    worksheet.Cells[3, 4].Value = "Số chứng từ";
                    worksheet.Cells[3, 5].Value = "Diễn giải";
                    worksheet.Cells[3, 6].Value = "Số tiền";
                    worksheet.Cells[3, 7].Value = "Mã đối tượng";
                    worksheet.Cells[3, 8].Value = "Đối tượng";
                    worksheet.Cells[3, 9].Value = "Địa chỉ";


                    // Thêm dữ liệu vào các hàng tiếp theo
                    for (var i = 0; i < data.Count; i++)
                    {
                        worksheet.Row(i + 4).Height = 25;
                        worksheet.Cells[i + 4, 1].Value = i + 1;
                        worksheet.Cells[i + 4, 2].Value = data[i].PostedDate;
                        worksheet.Cells[i + 4, 3].Value = data[i].RefDate;
                        worksheet.Cells[i + 4, 4].Value = data[i].RefNo;
                        worksheet.Cells[i + 4, 5].Value = data[i].Reason;
                        worksheet.Cells[i + 4, 6].Value = data[i].TotalAmount;
                        worksheet.Cells[i + 4, 7].Value = data[i].ObjectCode;
                        worksheet.Cells[i + 4, 8].Value = data[i].ObjectName;
                        worksheet.Cells[i + 4, 9].Value = data[i].Address;

                        worksheet.Cells[i + 4, 2].Style.Numberformat.Format = "dd/mm/yyyy";
                        worksheet.Cells[i + 4, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells[i + 4, 3].Style.Numberformat.Format = "dd/mm/yyyy";
                        worksheet.Cells[i + 4, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells[i + 4, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        for (var col = 1; col <= 9; col++)
                        {
                            var cell = worksheet.Cells[i + 4, col];
                            cell.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            cell.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            cell.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            cell.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        }
                    }

                    // Auto-fit columns
                    worksheet.Cells.AutoFitColumns();

                    // Return the Excel file as a byte array
                    var fileContents = package.GetAsByteArray();
                    var fileName = "Payment.xlsx";

                    return File(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
