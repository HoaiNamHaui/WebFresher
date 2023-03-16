using MISA.AMIS.BL.BaseBL;
using MISA.AMIS.BL.EmployeeBL;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.DL.AccountDL;
using MISA.AMIS.DL.EmployeeDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.BL.AccountBL
{
    public class AccountBL : BaseBL<Account>, IAccountBL
    {
        #region Field

        private IAccountDL _accountDL;

        #endregion

        #region Constructor

        public AccountBL(IAccountDL accountDL) : base(accountDL)
        {
            _accountDL = accountDL;
        }

        #endregion

        /// <summary>
        /// Lấy tài khoản con
        /// </summary>
        /// <returns></returns>
        public List<Account> GetChildAccount()
        {
            return _accountDL.GetChildAccount();
        }
    }
}
