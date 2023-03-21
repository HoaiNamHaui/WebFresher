using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Common.Entities
{
    /// <summary>
    /// Thông tin chi tiết chi
    /// </summary>
    public class PaymentDetail:BaseEntities
    {
        /// <summary>
        /// khóa ngoại
        /// </summary>
        public Guid PaymentId { get; set; }

        /// <summary>
        /// Khóa ngoại
        /// </summary>
        public Guid ObjectId { get; set; }

        /// <summary>
        /// Tổng chi phiếu
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Tài khoản nợ
        /// </summary>
        public string? DebitAccount { get; set; }

        /// <summary>
        /// Tài khoản có
        /// </summary>
        public string? CreditAccount { get; set; }

        /// <summary>
        /// Diễn giải
        /// </summary>
        public string? Description { get; set; }
    }
}
