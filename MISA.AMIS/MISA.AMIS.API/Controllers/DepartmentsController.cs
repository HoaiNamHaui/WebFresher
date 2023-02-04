using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.API.Entities;
using MySqlConnector;

namespace MISA.AMIS.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
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
                //Stored Procedure
                string storedProcedureName = "Proc_Department_GetAll";
                // Khởi tạo kết nối tới DB
                string connectionString = "Server=18.179.16.166;Port=3306;Database=MISA.AMIS.MF1234.NHNAM;uid=nvmanh;pwd =12345678";
                using (var mySqlConnection = new MySqlConnection(connectionString))
                {
                    // kết nối db
                    mySqlConnection.Open();

                    // Gọi proc 

                    var multy = mySqlConnection.QueryMultiple(storedProcedureName, commandType: System.Data.CommandType.StoredProcedure);
                    // lấy kết quả
                    
                    list = multy.Read<Department>().ToList();
                    
                    // đóng kết nối db
                    mySqlConnection.Close();
                }

                return StatusCode(StatusCodes.Status200OK, list);
            }
            catch(Exception ex)
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
                string connectionString = "Server=18.179.16.166;Port=3306;Database=MISA.AMIS.MF1234.NHNAM;uid=nvmanh;pwd =12345678";
                var mySqlConnection = new MySqlConnection(connectionString);
                var sqlCommand = $"SELECT * FROM Department WHERE DepartmentId = '{id.ToString()}'";
                var department = mySqlConnection.QueryFirstOrDefault<Department>(sqlCommand);
                
                return Ok(department);
            }
            catch(Exception ex)
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
