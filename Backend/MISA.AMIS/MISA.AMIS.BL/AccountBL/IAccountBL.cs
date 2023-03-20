using MISA.AMIS.BL.BaseBL;
using MISA.AMIS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.BL.AccountBL
{
    public interface IAccountBL : IBaseBL<Account>
    {
        /// <summary>
        /// Lấy tài khoản con
        /// </summary>
        /// <returns></returns>
        List<Account> GetChildAccount();

        /// <summary>
        /// Lấy tất cả tài khoản
        /// </summary>
        /// <returns></returns>
        List<Account> GetAll();
    }
}
