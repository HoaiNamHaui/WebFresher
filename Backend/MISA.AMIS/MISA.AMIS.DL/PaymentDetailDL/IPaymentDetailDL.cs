using MISA.AMIS.Common.Entities;
using MISA.AMIS.Common.Entities.DTO;
using MISA.AMIS.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.DL.PaymentDetailDL
{
    public interface IPaymentDetailDL : IBaseDL<PaymentDetail>
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
        PagingResult<PaymentDetail> GetByPaymentIdAndPaging(int pageNumber, int pageSize, Guid  PaymentId);
    }
}
