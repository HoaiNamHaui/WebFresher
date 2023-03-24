using MISA.AMIS.Common.Entities;
using MISA.AMIS.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.DL.AccountDL
{
    public interface IAccountDL:IBaseDL<Account>
    {
        /// <summary>
        /// lấy tài khoản con
        /// </summary>
        /// <returns></returns>
        List<Account> GetChildAccount();

        /// <summary>
        /// Lấy tất cả tài khoản
        /// </summary>
        /// <returns></returns>
        List<Account> GetAll();

        /// <summary>
        /// Lấy tài khoản theo số tài khoản
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accountNumber">Số tài khoản</param>
        /// <returns>Tài khỏan</returns>
        /// CreatedBy: NHNam(12/2/2023)
        Account GetAccountByAccountNumber(Guid? id, string accountNumber);

        /// <summary>
        /// Update isActive của các tài khoản trong list id
        /// </summary>
        /// <param name="accoundIds">list id</param>
        /// <param name="isActive">trang thái</param>
        /// <returns></returns>
        int UpdateIsActiveAccount(IEnumerable<Guid> accoundIds, bool isActive);
    }
}
