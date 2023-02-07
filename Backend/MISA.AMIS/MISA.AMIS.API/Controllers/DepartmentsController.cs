using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.BL.DepartmentBL;
using MISA.AMIS.BL.EmployeeBL;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.DL;
using MySqlConnector;

namespace MISA.AMIS.API.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        #region Field

        private IDepartmentBL _departmentBL;

        #endregion

        #region Constructor

        public DepartmentsController(IDepartmentBL departmentBL)
        {
            _departmentBL = departmentBL;
        }

        #endregion

        /// <summary>
        /// Lấy danh sách phòng ban
        /// </summary>
        /// <returns>Danh sách phfong ban</returns>
        /// Created by: NHNam (16/1/2023)
        [HttpGet]
        public IActionResult GetAllDepartment()
        {
            try
            {
                // Khởi tạo list 
                var list = new List<Department>();
                list = _departmentBL.GetAllDepartment();

                return StatusCode(StatusCodes.Status200OK, list);
            }
            catch (Exception ex)
            {
                var res = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng thử lại"
                };
                return StatusCode(500, res);
            }

        }

        /// <summary>
        /// Tìm phòng ban theo Id
        /// </summary>
        /// <param name="id">id phòng ban</param>
        /// <returns>thông tin phòng ban</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var department = _departmentBL.GetById(id);
                return Ok(department);
            }
            catch (Exception ex)
            {
                var res = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng thử lại"
                };
                return StatusCode(500, res);
            }
        }
    }
}
