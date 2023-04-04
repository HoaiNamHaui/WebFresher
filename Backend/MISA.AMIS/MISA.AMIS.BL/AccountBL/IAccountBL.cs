using MISA.AMIS.BL.BaseBL;
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

        /// <summary>
        /// Update isActive của các tài khoản trong list id
        /// </summary>
        /// <param name="accoundIds">list id</param>
        /// <param name="isActive">trang thái</param>
        /// <returns></returns>
        int UpdateIsActiveAccount(IEnumerable<Guid> accoundIds, bool isActive);

        /// <summary>
        /// Xóa tài khoản
        /// </summary>
        /// <param name="id">Id tài khoản</param>
        /// <param name="parentId">Id tài khoản cha</param>
        /// <returns></returns>
        int DeleteAccount(Guid id);
    }
}
