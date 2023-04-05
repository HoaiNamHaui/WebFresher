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
using System.Net.WebSockets;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
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

        /// <summary>
        /// Insert nhiều detail
        /// </summary>
        /// <param name="paymentDetail"></param>
        /// <returns></returns>
        public List<Guid> InsertPaymentDetails(IEnumerable<PaymentDetail> paymentDetails)
        {
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();
                using( var transaction = mySqlConnection.BeginTransaction())
                {
                    try
                    {
                        int rowsEffected = 0;
                        List<Guid> paymentDetailIds = new List<Guid>();
                        foreach (var paymentDetail in paymentDetails)
                        {
                            var storedName = "Proc_PaymentDetail_Insert";
                            var parameters = new DynamicParameters();
                            parameters.Add("PaymentDetailId", Guid.NewGuid());
                            parameters.Add("ModifiedBy", "Nguyễn Hoài Nam");
                            parameters.Add("CreatedBy", "Nguyễn Hoài Nam");
                            var listProps = typeof(PaymentDetail).GetProperties();
                            foreach (var prop in listProps)
                            {
                                parameters.Add($"p_{prop.Name}", prop.GetValue(paymentDetail));
                            }
                            parameters.Add("p_PaymentDetailId", Guid.NewGuid(), System.Data.DbType.String);
                            var numberOfAffectedRow = mySqlConnection.Execute(storedName, parameters, commandType: System.Data.CommandType.StoredProcedure, transaction:transaction);
                            rowsEffected += numberOfAffectedRow;
                            paymentDetailIds.Add(parameters.Get<Guid>("p_PaymentDetailId"));
                        }
                        if (rowsEffected != paymentDetails.Count()) transaction.Rollback();
                        transaction.Commit();
                        return paymentDetailIds;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception(ex.Message);
                    }
                } 
            }
        }

        /// <summary>
        /// Update nhiều payment detail
        /// </summary>
        /// <param name="paymentDetails">Mảng các payment detail</param>
        /// <returns></returns>
        public int UpdatePaymentDetails(IEnumerable<PaymentDetail> paymentDetails)
        {
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();
                using (var transaction = mySqlConnection.BeginTransaction())
                {
                    try
                    {
                        int rowsEffected = 0;
                        foreach (var paymentDetail in paymentDetails)
                        {
                            var storedName = "Proc_PaymentDetail_Update";
                            var parameters = new DynamicParameters();
                            parameters.Add("PaymentDetailId", Guid.NewGuid());
                            parameters.Add("ModifiedBy", "Nguyễn Hoài Nam");
                            var listProps = typeof(PaymentDetail).GetProperties();
                            foreach (var prop in listProps)
                            {
                                parameters.Add($"p_{prop.Name}", prop.GetValue(paymentDetail));
                            }
                            var numberOfAffectedRow = mySqlConnection.Execute(storedName, parameters, commandType: System.Data.CommandType.StoredProcedure, transaction: transaction);
                            rowsEffected ++;  
                        }
                        if (rowsEffected != paymentDetails.Count()) transaction.Rollback();
                        transaction.Commit();
                        return rowsEffected;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception(ex.Message);
                    }
                }
            }
        }

        /// <summary>
        /// Xóa nhiều detail
        /// </summary>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        public int DeletePaymentDetails(Guid paymentId)
        {
            var storeName = "Proc_PaymentDetail_DeleteByPaymentId";
            var parameters = new DynamicParameters();
            parameters.Add("p_PaymentId", paymentId);
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();
                using (var transaction = mySqlConnection.BeginTransaction())
                {
                    try
                    {
                        int rowsEffected = 0;
                        rowsEffected = mySqlConnection.Execute(storeName, parameters, commandType: System.Data.CommandType.StoredProcedure, transaction: transaction);
                        transaction.Commit();
                        return rowsEffected;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception(ex.Message);
                    }
                }
            }
        }
    }
}
