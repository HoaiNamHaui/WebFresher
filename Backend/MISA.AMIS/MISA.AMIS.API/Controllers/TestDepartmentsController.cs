using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.BL.BaseBL;
using MISA.AMIS.Common.Entities;

namespace MISA.AMIS.API.Controllers
{
    public class TestDepartmentsController : BasesController<Department>
    {
        public TestDepartmentsController(IBaseBL<Department> baseBL) : base(baseBL)
        {

        }
    }
}
