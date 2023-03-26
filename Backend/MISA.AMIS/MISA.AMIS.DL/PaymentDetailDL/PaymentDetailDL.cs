using Dapper;
using MISA.AMIS.Common.Constans;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.Common.Entities.DTO;
using MISA.AMIS.DL.BaseDL;
using MISA.AMIS.DL.PaymentDL;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.DL.PaymentDetailDL
{
    public class PaymentDetailDL: BaseDL<PaymentDetail>, IPaymentDetailDL
    {
        /// <summary>
        /// Lấy payment detail theo payment id
        /// </summary>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        public List<PaymentDetail> GetByPaymentId(Guid paymentId)
        {
            var result = new List<PaymentDetail>();
            string storedProcedureName = "Proc_PaymentDetail_GetByPaymentId";
            var parameters = new DynamicParameters();
            parameters.Add("p_PaymentId", paymentId);
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                result = mySqlConnection.Query<PaymentDetail>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
            return result;
        }

        /// <summary>
        /// Lọc phân trang
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
        /// CreatedBy: NHNam(3/2/2023)
        public PagingResult<PaymentDetail> GetByPaymentIdAndPaging(int pageNumber, int pageSize, Guid PaymentId)
        {
            var result = new PagingResult<PaymentDetail>();
            string storedProcedureName = "Proc_PaymentDetail_GetByPaymentIdAndPaging";
            var parameters = new DynamicParameters();
            parameters.Add("p_PageNumber", pageNumber);
            parameters.Add("p_PageSize", pageSize);
            parameters.Add("p_PaymentId", PaymentId);
            parameters.Add("p_TotalRecord", direction: System.Data.ParameterDirection.Output);
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();
                var multy = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                result.Data = multy.Read<PaymentDetail>().ToList();
                result.TotalRecord = parameters.Get<int>("p_TotalRecord");
                mySqlConnection.Close();
                result.CurrentPageRecords = result.Data.Count;
                result.CurrentPage = pageNumber;
            }
            return result;
        }
    }
}
