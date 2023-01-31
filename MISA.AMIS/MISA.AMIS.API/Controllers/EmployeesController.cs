using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.API.Entities;
using MISA.AMIS.API.Entities.DTO;
using MISA.AMIS.API.Enums;

namespace MISA.AMIS.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        public List<Employee> Employees = new List<Employee>()
        {
            new Employee
            {
                EmployeeId = Guid.Parse("a16372ad-0865-4e8a-9f73-e848e46ae06d"),
                EmployeeCode = "NV-0001",
                FullName = "Nguyễn Hoài Nam",
            },
            new Employee
            {
                EmployeeId = Guid.Parse("1d37cb7b-d83f-4523-96b3-9c974549b647"),
                EmployeeCode = "NV-0002",
                FullName = "Nguyễn Văn Dương",
            },
            new Employee
            {
                EmployeeId = Guid.Parse("28b91461-65bd-4378-91a5-4fc3669e56a8"),
                EmployeeCode = "NV-0003",
                FullName = "Nguyễn Hữu Giang",
            },
            new Employee
            {
                EmployeeId = Guid.Parse("7debed63-c8d7-42b3-b221-122dbdc2f0f6"),
                EmployeeCode = "NV-0004",
                FullName = "Quách Công Thành",
            },
            new Employee
            {
                EmployeeId = Guid.Parse("05f187eb-fe12-4d3a-b30c-572cec9d33b6"),
                EmployeeCode = "NV-0005",
                FullName = "Lê Huy Hưng",
            },
            new Employee
            {
                EmployeeId = Guid.Parse("e8fdb695-8e7a-4acb-ba59-06c88dabfd58"),
                EmployeeCode = "NV-0006",
                FullName = "Nguyễn Văn Ngọc",
            },
            new Employee
            {
                EmployeeId = Guid.Parse("6aba8feb-c869-434d-a8ef-8842c0c97c6c"),
                EmployeeCode = "NV-0007",
                FullName = "Phạm Văn Biển",
            },
        };
        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        [HttpGet("NewEmployeeCode")]
        public IActionResult GetNewEmployeeCode()
        {
            return StatusCode(StatusCodes.Status200OK, "NV-0010");
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
            Employee employee = new Employee();
            foreach( Employee item in Employees)
            {
                if(item.EmployeeId == EmployeeId)
                {
                    employee = item;
                }
            }
            return StatusCode(StatusCodes.Status200OK, employee);
        }
        /// <summary>
        /// API lấy danh sách nhân viên lọc theo trang
        /// </summary>
        /// <returns>Danh sách nhân viên</returns
        /// CreatedBy: NHNam (16/1/2023)
        [HttpGet("EmployeeFilter")]
        public PagingResult GetEmployeesByFilterAndPaging(
            [FromQuery] string? keyword,

            [FromQuery] Guid? departmentId,

            [FromQuery] int pageSize = 10,

            [FromQuery] int pageNumber = 1)
        {
            return new PagingResult
            {
                Total = 0,
                Data = Employees,
            };
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
            Employees.Add(employee);
            return StatusCode(StatusCodes.Status201Created, employee);
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
            foreach( Employee item in Employees)
            {
                if(item.EmployeeId == EmployeeId)
                {
                    item.EmployeeCode = employee.EmployeeCode;
                    item.FullName= employee.FullName;
                }
            }
            return StatusCode(StatusCodes.Status200OK, Employees);
        }

        /// <summary>
        /// Xóa 1 nhân viên
        /// </summary>
        /// <param name="EmployeeId">ID nhân viên</param>
        /// <returns>Danh sách nhân viên mới</returns>
        
        [HttpDelete("{EmployeeId}")]
        public IActionResult DeleteEmployee([FromRoute] Guid EmployeeId)
        {
            foreach (Employee item in Employees)
            {
                if (item.EmployeeId == EmployeeId)
                {
                    Employees.Remove(item);
                    return StatusCode(StatusCodes.Status200OK, Employees);
                }

            }
            return StatusCode(StatusCodes.Status200OK, Employees);
        }


    }
}
