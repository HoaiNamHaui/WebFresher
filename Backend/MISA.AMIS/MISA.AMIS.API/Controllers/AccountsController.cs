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

        /// <summary>
        /// Lấy các tài khoản con
        /// </summary>
        /// <returns></returns>
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



        /// <summary>
        /// Lấy tất cả các tài khoản
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Xóa tài khoản
        /// </summary>
        /// <param name="id">id tài khoản</param>
        /// Created by: NHNam (8/1/2023)
        [HttpDelete("DeleteChildAccount/{id}")]
        public virtual IActionResult DeleteAccount( [FromRoute] Guid id)
        {
            try
            {
                int result;

                result = _accountBL.DeleteAccount(id);

                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Update active cho list id
        /// </summary>
        /// <param name="accountIds"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        [HttpPut("UpdateIsActive")]
        public IActionResult UpdateIsActive([FromBody] IEnumerable<Guid> accountIds, [FromQuery] bool isActive) {
            var numberOfUpdate = 0;
            try
            {
                numberOfUpdate = _accountBL.UpdateIsActiveAccount(accountIds, isActive);    
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            return Ok(numberOfUpdate);
        }
    }
}
