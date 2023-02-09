using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.BL.BaseBL;
using MISA.AMIS.Common.Entities;

namespace MISA.AMIS.API.Controllers
{
    public class TestEmployeesController : BasesController<Employee>
    {
        public TestEmployeesController(IBaseBL<Employee> baseBL) : base(baseBL) { 
            
        }
    }
}
