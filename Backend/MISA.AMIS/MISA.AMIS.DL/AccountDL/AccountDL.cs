using Dapper;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.DL.BaseDL;
using MISA.AMIS.DL.EmployeeDL;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.DL.AccountDL
{
    public class AccountDL : BaseDL<Account>, IAccountDL
    {
        /// <summary>
        /// Lấy tài khoản con
        /// </summary>
        /// <returns></returns>
        public List<Account> GetChildAccount()
        {
            var listAccount = new List<Account>();
            string storedProcedureName = "Proc_Account_GetChildren";
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                listAccount = mySqlConnection.Query<Account>(storedProcedureName, commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
            return listAccount;
        }
    }
}
