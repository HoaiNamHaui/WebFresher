﻿using MISA.AMIS.Common.Entities;
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
    }
}