using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.API.Entities;
using MISA.AMIS.API.Entities.DTO;
using MISA.AMIS.API.Enums;
using MySqlConnector;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace MISA.AMIS.API.Controllers
{
    
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        public string connectionString = "Server=18.179.16.166;Port=3306;Database=MISA.AMIS.MF1234.NHNAM;uid=nvmanh;pwd =12345678";

        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        [HttpGet("NewEmployeeCode")]
        public IActionResult GetNewEmployeeCode()
        {
            try
            {
                var mySqlConnection = new MySqlConnection(connectionString);
                //var sqlCommand = "SELECT EmployeeCode FROM Employee ORDER BY EmployeeCode DESC";
                var sqlCommand = "SELECT MAX(EmployeeCode) FROM Employee";
                string emptCode = mySqlConnection.QueryFirstOrDefault<String>(sqlCommand).ToString();
                string Code = emptCode.Replace("NV-", "");
                string newEmployeeCode = "NV-" + (int.Parse(Code) + 1);
                return StatusCode(StatusCodes.Status200OK, newEmployeeCode);
            }
            catch(Exception ex)
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
                // Khởi tạo Employee
                var employee = new Employee();

                //Chuẩn bị tên Stored Procedure
                string storedProcedureName = "Proc_Employee_GetById";

                // Chuẩn bị tham số đầu vào cho proc
                var parameters = new DynamicParameters();
                parameters.Add("p_EmployeeId", EmployeeId);


                // Khởi taho kết nối db

                using (var mySqlConnection = new MySqlConnection(connectionString))
                {
                    // kết nối db
                    mySqlConnection.Open();

                    // Gọi proc 

                    employee = mySqlConnection.QueryFirstOrDefault<Employee>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                    // đóng kết nối db
                    mySqlConnection.Close();
                }

                return StatusCode(StatusCodes.Status200OK, employee);
            }
            catch (Exception ex)
            {
                // Bắn lỗi
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
            [FromQuery] string? keyword,

            [FromQuery] int pageSize = 10,

            [FromQuery] int pageNumber = 1)
        {
            try
            {
                // Khởi tạo kêt quả trả về
                var result = new PagingResult();

                //Chuẩn bị tên Stored Procedure
                string storedProcedureName = "Proc_Employee_GetByFilter";

                // Chuẩn bị tham số đầu vào cho proc
                var parameters = new DynamicParameters();
                parameters.Add("p_PageNumber", pageNumber);
                parameters.Add("p_PageSize", pageSize);
                parameters.Add("p_TextSearch", keyword);
                parameters.Add("p_TotalRecord", direction: System.Data.ParameterDirection.Output);

                // Khởi taho kết nối db
                
                using (var mySqlConnection = new MySqlConnection(connectionString))
                {
                    // kết nối db
                    mySqlConnection.Open();

                    // Gọi proc 

                    var multy = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                    // lấy kết quả gán cho result
                    result.Data = multy.Read<Employee>().ToList();
                    result.CurrentPageRecords = result.Data.Count;
                    result.CurrentPage = pageNumber;
                    // TÍnh toán số bản ghi, tổng số trang
                    //result.TotalRecord = mySqlConnection.Query("Proc_Employee_GetAll", commandType: System.Data.CommandType.StoredProcedure).ToList().Count();
                    result.TotalRecord = parameters.Get<int>("p_TotalRecord");
                    if (result.TotalRecord % pageSize == 0)
                    {
                        result.TotalPage = result.TotalRecord / pageSize;
                    }
                    else
                    {
                        result.TotalPage = (result.TotalRecord / pageSize) + 1;
                    }
                    // đóng kết nối db
                    mySqlConnection.Close();
                }

                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch(Exception ex)
            {
                // Bắn lỗi
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
            try{
                // Validate
                // 1. Mã nhân viên
                // Bỏ trống
                if (string.IsNullOrEmpty(employee.EmployeeCode))
                {
                    var res = new
                    {
                        devMsg = "Dữ liệu đầu vào không hợp lệ",
                        userMsg = "Mã nhân viên không được bỏ trống"
                    };
                    return StatusCode(400, res);
                }

                // 2. Tên nhân viên
                if (string.IsNullOrEmpty(employee.FullName))
                {
                    var res = new
                    {
                        devMsg = "Dữ liệu đầu vào không hợp lệ",
                        userMsg = "Tên nhân viên không được bỏ trống"
                    };
                    return StatusCode(400, res);
                }

                // 3. Phòng ban
                if (employee.EmployeeId == null)
                {
                    var res = new
                    {
                        devMsg = "Dữ liệu đầu vào không hợp lệ",
                        userMsg = "Phòng ban không được bỏ trống"
                    };
                    return StatusCode(400, res);
                }

                // 4.Tuổi trên 18
                if (employee.DateOfBirth != null)
                {
                    DateTime now = DateTime.Now;

                    if (now.Year - employee.DateOfBirth.Value.Year < 18)
                    {
                        var res = new
                        {
                            devMsg = "Dữ liệu đầu vào không hợp lệ",
                            userMsg = "Tuổi nhân viên phải trên 18"
                        };
                        return StatusCode(400, res);
                    }

                }

                // 5. Số điện thoại không đúng định dạng
                if (!string.IsNullOrEmpty(employee.PhoneNumber))
                {
                    string motif = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";
                    if (!Regex.IsMatch(employee.PhoneNumber.Trim(), motif))
                    {
                        var res = new
                        {
                            devMsg = "Dữ liệu đầu vào không hợp lệ",
                            userMsg = "Số điện thoại không đúng định dạng"
                        };
                        return StatusCode(400, res);
                    }
                }

                // 6. Email không đúng định dạng
                if (!string.IsNullOrEmpty(employee.Email))
                {
                    string motif = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
                    if (!Regex.IsMatch(employee.Email.Trim(), motif))
                    {
                        var res = new
                        {
                            devMsg = "Dữ liệu đầu vào không hợp lệ",
                            userMsg = "Email không đúng định dạng"
                        };
                        return StatusCode(400, res);
                    }
                }

                // Thêm dữ liệu cần thiết
                employee.CreatedDate= DateTime.Now;
                employee.ModifiedDate= DateTime.Now;
                employee.EmployeeId = Guid.NewGuid();

                // proc
                string procName = "Proc_Employee_Insert";
                
                //Chuẩn bị tham số đầu vào
                var parameters = new DynamicParameters();
                foreach (var prop in employee.GetType().GetProperties())
                {
                    parameters.Add("p_"+prop.Name, prop.GetValue(employee, null));
                }
                // Khởi tạo kết nối DB
                using(var mySqlConnection = new MySqlConnection(connectionString))
                {
                    // Kết nối
                    mySqlConnection.Open();

                    // Gọi proc
                    var numberOfRowsAffect = mySqlConnection.Execute(procName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                    if(numberOfRowsAffect > 0)
                    {
                        return StatusCode(StatusCodes.Status201Created, employee);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError);
                    }
                }
            }
            catch(Exception ex)
            {
                return HandleException(ex);
            }
            
        }

        /// <summary>
        /// Sửa thông tin nhân viên
        /// </summary>
        /// <param name="EmployeeId">ID nhân viên</param>
        /// <param name="Employees">Nhân viên</param>
        /// <returns>Danh sách nhân viên mới</returns>
        /// Created by: NHNam (16/1/2023)

        [HttpPut("{EmployeeId}")]
        public IActionResult UpdateEmployee([FromRoute] Guid EmployeeId, [FromBody] Employee employee)
        {
            try
            {
                // Validate
                // 1. Mã nhân viên
                // Bỏ trống
                if (string.IsNullOrEmpty(employee.EmployeeCode.Trim()))
                {
                    var res = new
                    {
                        devMsg = "Dữ liệu đầu vào không hợp lệ",
                        userMsg = "Mã nhân viên không được bỏ trống"
                    };
                    return StatusCode(400, res);
                }

                // 2. Tên nhân viên
                if (string.IsNullOrEmpty(employee.FullName.Trim()))
                {
                    var res = new
                    {
                        devMsg = "Dữ liệu đầu vào không hợp lệ",
                        userMsg = "Tên nhân viên không được bỏ trống"
                    };
                    return StatusCode(400, res);
                }

                // 3. Phòng ban
                if (employee.DepartmentId == null)
                {
                    var res = new
                    {
                        devMsg = "Dữ liệu đầu vào không hợp lệ",
                        userMsg = "Phòng ban không được bỏ trống"
                    };
                    return StatusCode(400, res);
                }

                // 4.Tuổi trên 18
                if (employee.DateOfBirth != null)
                {
                    DateTime now = DateTime.Now;

                    if (now.Year - employee.DateOfBirth.Value.Year < 18)
                    {
                        var res = new
                        {
                            devMsg = "Dữ liệu đầu vào không hợp lệ",
                            userMsg = "Tuổi nhân viên phải trên 18"
                        };
                        return StatusCode(400, res);
                    }

                }

                // 5. Số điện thoại không đúng định dạng
                if (!string.IsNullOrEmpty(employee.PhoneNumber.Trim()))
                {
                    string motif = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";
                    if (!Regex.IsMatch(employee.PhoneNumber.Trim(), motif))
                    {
                        var res = new
                        {
                            devMsg = "Dữ liệu đầu vào không hợp lệ",
                            userMsg = "Số điện thoại không đúng định dạng"
                        };
                        return StatusCode(400, res);
                    }
                }

                // 6. Email không đúng định dạng
                if (!string.IsNullOrEmpty(employee.Email.Trim()))
                {
                    string motif = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
                    if (!Regex.IsMatch(employee.Email.Trim(), motif))
                    {
                        var res = new
                        {
                            devMsg = "Dữ liệu đầu vào không hợp lệ",
                            userMsg = "Email không đúng định dạng"
                        };
                        return StatusCode(400, res);
                    }
                }
                //Thêm dữ liệu
                employee.ModifiedDate = DateTime.Now;
                employee.EmployeeId = EmployeeId;
                // proc
                string procName = "Proc_Employee_Update";

                //Chuẩn bị tham số đầu vào
                var parameters = new DynamicParameters();
                foreach (var prop in employee.GetType().GetProperties())
                {
                    parameters.Add("p_" + prop.Name, prop.GetValue(employee, null));
                }
                // Khởi tạo kết nối DB
                using (var mySqlConnection = new MySqlConnection(connectionString))
                {
                    // Kết nối
                    mySqlConnection.Open();

                    // Tìm Employee theo id xem có tồn tại
                    int employeeSearch = mySqlConnection.Query("Proc_Employee_GetById", parameters, commandType: System.Data.CommandType.StoredProcedure).ToList().Count();
                    if(employeeSearch == 0)
                    {
                        return StatusCode(StatusCodes.Status400BadRequest);
                    }
                    else
                    {
                        // Gọi proc
                        var numberOfRowsAffect = mySqlConnection.Execute(procName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                        if (numberOfRowsAffect > 0)
                        {
                            return StatusCode(StatusCodes.Status200OK, employee);
                        }
                        else
                        {
                            return StatusCode(StatusCodes.Status500InternalServerError);
                        }
                    }
                    
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

                //Chuẩn bị tên Stored Procedure
                string storedProcedureName = "Proc_Employee_Delete";

                // Chuẩn bị tham số đầu vào cho proc
                var parameters = new DynamicParameters();
                parameters.Add("p_EmployeeId", EmployeeId);
                

                // Khởi taho kết nối db

                using (var mySqlConnection = new MySqlConnection(connectionString))
                {
                    // kết nối db
                    mySqlConnection.Open();

                    // Gọi proc 

                    var sqlDel = mySqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                    result = sqlDel;
                    // đóng kết nối db
                    mySqlConnection.Close();
                }

                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                // Bắn lỗi
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
            var res = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ MISA để được trợ giúp"
                };
                return StatusCode(StatusCodes.Status500InternalServerError, res);
        }
    }
}
