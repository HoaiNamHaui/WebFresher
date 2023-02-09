using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Common.Entities.DTO;
using MISA.AMIS.Common.Entities;
using MySqlConnector;
using System.Text.RegularExpressions;
using MISA.AMIS.BL.EmployeeBL;
using MISA.AMIS.BL.BaseBL;

namespace MISA.AMIS.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        #region Field

        private IEmployeeBL _employeeBL;

        #endregion

        #region Constructor

        public EmployeesController(IEmployeeBL employeeBL)
        {
            _employeeBL = employeeBL; 
        }

        #endregion

        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
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
                return HandleException(ex);
            }

        }

        /// <summary>
        /// Tìm nhân viên theo ID
        /// </summary>
        /// <param name="EmployeeId">ID nhân viên</param>
        /// <returns>Employee</returns>
        /// Created by: NHNam (16/1/2023)
        [HttpGet("{EmployeeId}")]
        public IActionResult GetEmployeeById([FromRoute] Guid EmployeeId)
        {
            try
            {
                var employee = new Employee();
                employee = _employeeBL.GetEmployeeById(EmployeeId);

                return StatusCode(StatusCodes.Status200OK, employee);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// API lấy danh sách nhân viên lọc theo trang
        /// </summary>
        /// <returns>Danh sách nhân viên</returns
        /// CreatedBy: NHNam (16/1/2023)
        [HttpGet("Filter")]
        public IActionResult GetEmployeesByFilter(
            [FromQuery] string? keyword ="",

            [FromQuery] int pageSize = 10,

            [FromQuery] int pageNumber = 1)
        {
            try
            {
                var result = new PagingResult();
                result = _employeeBL.GetEmployeesByFilter(pageNumber, pageSize, keyword);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Thêm mới 1 nhân viên
        /// </summary>
        /// <param name="employee">Nhân viên</param>
        /// <returns>Nhân viên</returns>
        /// Created by: NHNam (16/1/2023)
        [HttpPost]
        public IActionResult InsertEmployee([FromBody] Employee employee)
        {
            try
            {
                var response = _employeeBL.InsertEmployee(employee);
                if(response is ValidateResult)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }
                else if (response > 0)
                {
                    return StatusCode(StatusCodes.Status201Created, response);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }

            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

        }

        /// <summary>
        /// Sửa thông tin nhân viên
        /// </summary>
        /// <param name="id">Id nhân viên</param>
        /// <returns>
        /// 1: Sửa thành công
        /// 0: Sửa thất bại
        /// </returns>
        [HttpPut("{EmployeeId}")]
        public IActionResult UpdateEmployee([FromRoute] Guid EmployeeId, [FromBody] Employee employee)
        {
            try
            {
                var response = _employeeBL.UpdateEmployee(EmployeeId, employee);
                if (response is ValidateResult)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }
                else if(response > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, response);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Xóa 1 nhân viên
        /// </summary>
        /// <param name="EmployeeId">ID nhân viên</param>
        /// <returns>Danh sách nhân viên mới</returns>

        [HttpDelete("{EmployeeId}")]
        public IActionResult DeleteEmployee([FromRoute] Guid EmployeeId)
        {
            try
            {
                int result;

                result = _employeeBL.DeleteEmployee(EmployeeId);

                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Xử lý ngoại lệ trả về lỗi
        /// </summary>
        /// <param name="ex">ngoại lệ</param>
        /// <returns>devMsg và userMsg</returns>
        private IActionResult HandleException(Exception ex)
        {
            var res = new ErrorResult
            {
                DevMsg= ex.Message,
                UserMsg= "Có lỗi xảy ra, vui lòng liên hệ MISA để được trợ giúp!"
            };
            return StatusCode(StatusCodes.Status500InternalServerError, res);
        }
    }
}
