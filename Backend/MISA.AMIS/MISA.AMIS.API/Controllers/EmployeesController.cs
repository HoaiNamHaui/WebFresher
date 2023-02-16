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
        public IActionResult DownLoadExcel()
        {
            try
            {
                List<Employee> data = _employeeBL.ExportToExcel();
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                // Create Excel package
                using (var package = new ExcelPackage())
                {
                    // Add a new worksheet to the workbook
                    var worksheet = package.Workbook.Worksheets.Add("Employees");

                    // Get properties of data model
                    var properties = typeof(Employee).GetProperties();

                    // Add header row
                    for (var i = 0; i < properties.Length; i++)
                    {
                        worksheet.Cells[1, i + 1].Value = properties[i].Name;
                    }
                    // Format header
                    var headerStyle = worksheet.Cells[1, 1, 1, properties.Length].Style;
                    headerStyle.Font.Bold = true;
                    headerStyle.Fill.PatternType = ExcelFillStyle.Solid;
                    headerStyle.Fill.BackgroundColor.SetColor(Color.LightGray);
                    headerStyle.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    // Add data rows
                    for (var i = 0; i < data.Count; i++)
                    {
                        for (var j = 0; j < properties.Length; j++)
                        {
                            worksheet.Cells[i + 2, j + 1].Value = properties[j].GetValue(data[i]);
                        }
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
