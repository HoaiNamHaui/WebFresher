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
    //[Route("api/v1/[controller]")]
    //[ApiController]
    public class EmployeesController : BasesController<Employee>
    {
        #region Field

        private IEmployeeBL _employeeBL;

        #endregion

        #region Constructor

        public EmployeesController(IEmployeeBL employeeBL):base(employeeBL)
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
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
                Console.WriteLine(ex.ToString());
            }

        }

    }
}
