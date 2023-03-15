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

    }
}
