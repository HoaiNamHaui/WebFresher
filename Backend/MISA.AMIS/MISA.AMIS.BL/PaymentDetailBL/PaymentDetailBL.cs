using MISA.AMIS.BL.BaseBL;
using MISA.AMIS.BL.PaymentBL;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.Common.Entities.DTO;
using MISA.AMIS.DL.PaymentDetailDL;
using MISA.AMIS.DL.PaymentDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.BL.PaymentDetailBL
{
    public class PaymentDetailBL: BaseBL<PaymentDetail>, IPaymentDetailBL
    {
        #region Field

        private IPaymentDetailDL _paymentDetailDL;

        #endregion
        public PaymentDetailBL(IPaymentDetailDL paymentDetailDL) : base(paymentDetailDL)
        {
            _paymentDetailDL = paymentDetailDL;
        }

        /// <summary>
        /// Lấy payment detail theo payment id
        /// </summary>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        public List<PaymentDetail> GetByPaymentId(Guid paymentId)
        {
            return _paymentDetailDL.GetByPaymentId(paymentId);
        }


        /// <summary>
        /// Lọc phân trang
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
        /// CreatedBy: NHNam(3/2/2023)
        public PagingResult<PaymentDetail> GetByPaymentIdAndPaging(int pageNumber, int pageSize, Guid PaymentId)
        {
            var result = new PagingResult<PaymentDetail>();
            result = _paymentDetailDL.GetByPaymentIdAndPaging(pageNumber, pageSize, PaymentId);
            // TÍnh toán số bản ghi, tổng số trang
            if (result.TotalRecord % pageSize == 0)
            {
                result.TotalPage = result.TotalRecord / pageSize;
            }
            else
            {
                result.TotalPage = (result.TotalRecord / pageSize) + 1;
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
            return _paymentDetailDL.InsertPaymentDetails(paymentDetails);
        }

        /// <summary>
        /// Update nhiều payment detail
        /// </summary>
        /// <param name="paymentDetails">Mảng các payment detail</param>
        /// <returns></returns>
        public int UpdatePaymentDetails(IEnumerable<PaymentDetail> paymentDetails)
        {
            var paymentId = paymentDetails.First().PaymentId;
            _paymentDetailDL.DeletePaymentDetails(paymentId);
            var result = _paymentDetailDL.InsertPaymentDetails(paymentDetails);
            return result.Count();
        }
    }
}
