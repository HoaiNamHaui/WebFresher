using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.API.Entities;

namespace MISA.AMIS.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        public List<Department> Departments = new List<Department>()
        {
            new Department{
                DepartmentId = Guid.Parse("3f8e6896-4c7d-15f5-a018-75d8bd200d7c"),
                DepartmentName = "Phòng Công Nghệ Thông Tin",
                CreatedBy = "Norris Calderon",
                ModifiedBy = "Elwood Ligon" 
            },
            new Department{
                DepartmentId = Guid.Parse("9af69954-847f-4766-af7c-446c77e490f8"),
                DepartmentName = "Phòng Hành Chính",
                CreatedBy = "Norris Calderon",
                ModifiedBy = "Elwood Ligon" 
            },
            new Department{
                DepartmentId = Guid.Parse("c883e7fc-510f-493a-9f2a-5dd8b8e2b295"),
                DepartmentName = "Phòng Nhân Sự",
                CreatedBy = "Norris Calderon",
                ModifiedBy = "Elwood Ligon" 
            },
            new Department{
                DepartmentId = Guid.Parse("8d2350f0-06e3-4cc1-bce5-39c021e91523"),
                DepartmentName = "Phòng Kế toán",
                CreatedBy = "Norris Calderon",
                ModifiedBy = "Elwood Ligon" 
            },
        };  
       /// <summary>
       /// Lấy danh sách phòng ban
       /// </summary>
       /// <returns>Danh sách phfong ban</returns>
       /// Created by: NHNam (16/1/2023)
        [HttpGet]
        public IActionResult GetAllDepartment()
        {
            return StatusCode(StatusCodes.Status200OK, Departments);
        }
    }
}
