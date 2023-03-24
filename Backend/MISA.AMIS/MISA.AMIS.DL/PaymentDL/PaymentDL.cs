using Dapper;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.DL.AccountDL;
using MISA.AMIS.DL.BaseDL;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.DL.PaymentDL
{
    public class PaymentDL : BaseDL<Payment>, IPaymentDL
    {
        /// <summary>
        /// Lấy số phiếu chi max
        /// </summary>
        /// <returns>Số phiếu chi lớn nhất</returns>
        /// CreatedBy: NHNam(3/2/2023)
        public string GetMaxRefNo()
        {
            var sqlCommand = "SELECT MAX(RefNo) FROM Payment";
            var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);
            string refNoMax = mySqlConnection.QueryFirstOrDefault<String>(sqlCommand).ToString();
            return refNoMax;
        }

        /// <summary>
        /// Xóa hoàng loạt
        /// </summary>
        /// <param name="ids">mảng các id</param>
        /// <returns>Số bản ghi xóa</returns>
        /// CreatedBy: NHNam(12/2/2023)
        public int DeleteMultiple(IEnumerable<Guid> ids)
        {
            var storeName = "Proc_Payment_DeleteMultiple";
            int rowsEffect = 0;
            var parameters = new DynamicParameters();
            var listRecordsId = $"{String.Join(",", ids)}";
            parameters.Add("p_PaymentIds", listRecordsId);
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();
                using (var transaction = mySqlConnection.BeginTransaction())
                {
                    try
                    {
                        rowsEffect = mySqlConnection.Execute(storeName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                        if (rowsEffect == ids?.Count()) transaction.Commit();
                        else transaction.Rollback();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception(ex.Message);
                    }
                }
            }
            return rowsEffect;
        }
    }
}
