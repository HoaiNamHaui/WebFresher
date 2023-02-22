using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Common.Entities.DTO;
using MISA.AMIS.Common.Entities;
using MySqlConnector;
using System.Text.RegularExpressions;
using MISA.AMIS.BL.EmployeeBL;
using MISA.AMIS.BL.BaseBL;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace MISA.AMIS.API.Controllers
{
    //[Route("api/v1/[controller]")]
    //[ApiController]
    public class EmployeesController : BasesController<Employee>
    {
        #region Field

        private IEmployeeBL _employeeBL;

        #endregion

        #region Constructor

        public EmployeesController(IEmployeeBL employeeBL) : base(employeeBL)
        {
            _employeeBL = employeeBL;
        }

        #endregion

        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// Created by: NHNam (8/1/2023)
        [HttpGet("NewEmployeeCode")]
        public IActionResult GetNewEmployeeCode()
        {
            try
            {
                string newEmployeeCode = _employeeBL.GetNewEmployeeCode();
                return StatusCode(StatusCodes.Status200OK, newEmployeeCode);
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
                numDeleted = _employeeBL.DeleteMultiple(ids);
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
                List<Employee> data = _employeeBL.ExportToExcel(pageNumber, pageSize, keyword);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                // Tạo Excel package
                using (var package = new ExcelPackage())
                {
                    // Tạo mới 1 worksheet
                    var worksheet = package.Workbook.Worksheets.Add("Employees");

                    // Căn chỉnh chung
                    worksheet.Cells["A:I"].Style.Font.SetFromFont("Times New Roman", 11);
                    worksheet.Cells["A:I"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Cells["A:I"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    // Căn chỉnh Tiêu đề
                    worksheet.Cells["A1:I1"].Merge = true;
                    worksheet.Cells["A1"].Value = "DANH SÁCH NHÂN VIÊN";
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

                    // Thêm hàng Header
                    worksheet.Cells[3, 1].Value = "STT";
                    worksheet.Cells[3, 2].Value = "Mã nhân viên";
                    worksheet.Cells[3, 3].Value = "Tên nhân viên";
                    worksheet.Cells[3, 4].Value = "Giới tính";
                    worksheet.Cells[3, 5].Value = "Ngày sinh";
                    worksheet.Cells[3, 6].Value = "Chức danh";
                    worksheet.Cells[3, 7].Value = "Tên đơn vị";
                    worksheet.Cells[3, 8].Value = "Số tài khoản";
                    worksheet.Cells[3, 9].Value = "Tên ngân hàng";

                    //double rowHeight = 14;
                    // Thêm dữ liệu vào các hàng tiếp theo
                    for (var i = 0; i < data.Count; i++)
                    {
                        worksheet.Cells[i + 4, 1].Value = i + 1;
                        worksheet.Cells[i + 4, 2].Value = data[i].EmployeeCode;
                        worksheet.Cells[i + 4, 3].Value = data[i].FullName;
                        worksheet.Cells[i + 4, 4].Value = data[i].GenderName;
                        worksheet.Cells[i + 4, 5].Value = data[i].DateOfBirth;
                        worksheet.Cells[i + 4, 6].Value = data[i].PositionName;
                        worksheet.Cells[i + 4, 7].Value = data[i].DepartmentName;
                        worksheet.Cells[i + 4, 8].Value = data[i].BankAccount;
                        worksheet.Cells[i + 4, 9].Value = data[i].BankName;
                        worksheet.Cells[i + 4, 5].Style.Numberformat.Format = "dd/mm/yyyy";
                        worksheet.Cells[i + 4, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }

                    // Auto-fit columns
                    worksheet.Cells.AutoFitColumns();

                    // Return the Excel file as a byte array
                    var fileContents = package.GetAsByteArray();
                    var fileName = "Employees.xlsx";

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
