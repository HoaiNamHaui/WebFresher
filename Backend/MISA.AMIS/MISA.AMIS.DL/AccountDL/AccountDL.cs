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

        /// <summary>
        /// Lấy tất cả tài khoản
        /// </summary>
        /// <returns></returns>
        public List<Account> GetAll()
        {
            var listAccount = new List<Account>();
            string storedProcedureName = "Proc_Account_GetAll";
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                listAccount = mySqlConnection.Query<Account>(storedProcedureName, commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
            return listAccount;
        }

        /// <summary>
        /// Lấy tài khoản theo số tài khoản
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accountNumber">Số tài khoản</param>
        /// <returns>Tài khỏan</returns>
        /// CreatedBy: NHNam(12/2/2023)
        public Account GetAccountByAccountNumber(Guid? id, string accountNumber)
        {
            var result = new Account();
            string storedProcedureName = "Proc_Account_CheckDuplicate";
            var parameters = new DynamicParameters();
            parameters.Add("p_AccountNumber", accountNumber);
            parameters.Add("p_AccountId", id);
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                result = mySqlConnection.QueryFirstOrDefault<Account>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
            return result;
        }
    }
}
