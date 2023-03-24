using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.BL.PaymentBL;
using MISA.AMIS.BL.PaymentDetailBL;
using MISA.AMIS.Common.Entities;

namespace MISA.AMIS.API.Controllers
{
    public class PaymentDetailsController : BasesController<PaymentDetail>
    {
        #region Feild

        private IPaymentDetailBL _paymentDetailBL;

        #endregion

        #region Constructor

        public PaymentDetailsController(IPaymentDetailBL paymentDetailBL) : base(paymentDetailBL)
        {
            _paymentDetailBL = paymentDetailBL;
        }

        #endregion
    }
}
