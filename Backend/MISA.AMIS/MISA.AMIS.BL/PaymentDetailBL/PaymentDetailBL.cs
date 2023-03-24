using MISA.AMIS.BL.BaseBL;
using MISA.AMIS.BL.PaymentBL;
using MISA.AMIS.Common.Entities;
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
    }
}
