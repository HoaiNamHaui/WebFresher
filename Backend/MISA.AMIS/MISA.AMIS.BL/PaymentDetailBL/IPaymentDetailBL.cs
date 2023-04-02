using MISA.AMIS.BL.BaseBL;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.Common.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.BL.PaymentDetailBL
{
    public interface IPaymentDetailBL : IBaseBL<PaymentDetail>
    {
        /// <summary>
        /// Lấy payment detail theo payment id
        /// </summary>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        List<PaymentDetail> GetByPaymentId(Guid paymentId);

        /// <summary>
        /// Lọc phân trang
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
        /// CreatedBy: NHNam(3/2/2023)
        PagingResult<PaymentDetail> GetByPaymentIdAndPaging(int pageNumber, int pageSize, Guid PaymentId);

        /// <summary>
        /// Insert nhiều detail
        /// </summary>
        /// <param name="paymentDetail"></param>
        /// <returns></returns>
        List<Guid> InsertPaymentDetails(IEnumerable<PaymentDetail> paymentDetails);

        /// <summary>
        /// Update nhiều payment detail
        /// </summary>
        /// <param name="paymentDetails">Mảng các payment detail</param>
        /// <returns></returns>
        int UpdatePaymentDetails(IEnumerable<PaymentDetail> paymentDetails);
    }
}
