using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.BL.AccountBL;
using MISA.AMIS.BL.BaseBL;
using MISA.AMIS.BL.EmployeeBL;
using MISA.AMIS.Common.Entities;

namespace MISA.AMIS.API.Controllers
{
    public class AccountsController : BasesController<Account>
    {
        #region Feild

        private IAccountBL _accountBL;

        #endregion

        #region Constructor

        public AccountsController(IAccountBL accountBL) : base(accountBL)
        {
            _accountBL = accountBL;
        }

        #endregion

        [HttpGet("GetChildrenAccount")]
        public IActionResult GetChildAccount()
        {
            try
            {
                var data = _accountBL.GetChildAccount();
                return StatusCode(StatusCodes.Status200OK, data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex);

            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var data = _accountBL.GetAll();
                return StatusCode(StatusCodes.Status200OK, data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex);

            }
        }
    }
}
