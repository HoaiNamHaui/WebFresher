using MISA.AMIS.BL.BaseBL;
using MISA.AMIS.BL.EmployeeBL;
using MISA.AMIS.Common;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.Common.Entities.DTO;
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

        /// <summary>
        /// Lấy tất cả tài khoản
        /// </summary>
        /// <returns></returns>
        public List<Account> GetAll()
        {
            return _accountDL.GetAll();
        }

        /// <summary>
        /// Validate custom theo đối tượng cụ thể
        /// </summary>
        /// <param name="account">Tài khoản validate</param>
        /// <returns>Kết quả validate</returns>
        /// CreatedBy: NHNam(22/3/2023)
        protected override ValidateResult ValidateCustom(Account account, Guid? id)
        {
            ValidateResult validateResult = new ValidateResult();
            validateResult.IsSuccess = true;
            // Trùng số tài khoản
            var acc = _accountDL.GetAccountByAccountNumber(id, account.AccountNumber);
            if (acc != null)
            {
                validateResult.ListError.Add(Resource.DuplicateAccountNumber);
                validateResult.IsSuccess = false;
            }

            // Số tài khoản >= 3 ký tự
            if(account.AccountNumber.Length < 3)
            {
                validateResult.ListError.Add(Resource.inValidLengthAccount);
                validateResult.IsSuccess = false;
            }

            // Số tài khoản con phải bắt đầu bằng tài khoản cha
            string parentAccountNumber = "";
            if(account.ParentId != Guid.Empty)
            {
                var parent = _accountDL.GetById(account.ParentId);
                if(parent != null)
                {
                    parentAccountNumber = parent.AccountNumber;
                }
            }
            if (!string.IsNullOrEmpty(parentAccountNumber))
            {
                if(account.AccountNumber.IndexOf(parentAccountNumber) != 0)
                {
                    validateResult.ListError.Add(Resource.ChildAccount);
                    validateResult.IsSuccess = false;
                }
                
            }
            return validateResult;
        }
    }
}
