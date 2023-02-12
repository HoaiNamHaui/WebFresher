using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.BL.BaseBL;
using MISA.AMIS.BL.DepartmentBL;
using MISA.AMIS.BL.EmployeeBL;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.DL;
using MySqlConnector;

namespace MISA.AMIS.API.Controllers
{
    public class DepartmentsController : BasesController<Department>
    {
        public DepartmentsController(IBaseBL<Department> baseBL) : base(baseBL)
        {
        }

        /// <summary>
        /// Ghi đè lại InsertRecord
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public override IActionResult InsertRecord(Department record)
        {
            return StatusCode(StatusCodes.Status405MethodNotAllowed);
        }

        /// <summary>
        /// Ghi đè lại InsertRecord
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        [HttpPut("{Id}")]
        public override IActionResult UpdateRecord([FromRoute] Guid DepartmentId, [FromBody] Department record)
        {
            return StatusCode(StatusCodes.Status405MethodNotAllowed);
        }

        /// <summary>
        /// Ghi đè lại InsertRecord
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public override IActionResult DeleteRecord([FromRoute] Guid id)
        {
            return StatusCode(StatusCodes.Status405MethodNotAllowed);
        }
    }
}
