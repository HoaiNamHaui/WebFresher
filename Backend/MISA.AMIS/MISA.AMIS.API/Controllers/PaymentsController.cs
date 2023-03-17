using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.BL.AccountBL;
using MISA.AMIS.BL.PaymentBL;
using MISA.AMIS.Common.Entities;

namespace MISA.AMIS.API.Controllers
{
    public class PaymentsController : BasesController<Payment>
    {
        #region Feild

        private IPaymentBL _paymentBL;

        #endregion

        #region Constructor

        public PaymentsController(IPaymentBL paymentBL) : base(paymentBL)
        {
            _paymentBL = paymentBL;
        }

        #endregion
    }
}
