using MISA.AMIS.BL.BaseBL;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.DL.AccountDL;
using MISA.AMIS.DL.BaseDL;
using MISA.AMIS.DL.PaymentDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.BL.PaymentBL
{
    public class PaymentBL : BaseBL<Payment>, IPaymentBL
    {
        #region Field

        private IPaymentDL _paymentDL;

        #endregion
        public PaymentBL(IPaymentDL paymentDL) : base(paymentDL)
        {
            _paymentDL = paymentDL;
        }
    }
}
