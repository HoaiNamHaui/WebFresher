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
        /// Khóa chính
        /// </summary>
        public Guid PaymentDetailId { get; set; }

        /// <summary>
        /// khóa ngoại
        /// </summary>
        public Guid PaymentId { get; set; }

        /// <summary>
        /// Khóa ngoại
        /// </summary>
        public Guid ObjectId { get; set; }

        /// <summary>
        /// Id đối tượng
        /// </summary>
        public String? ObjectName { get; set; }

        /// <summary>
        /// Mã đối tượng
        /// </summary>
        public String? ObjectCode { get; set; }

        /// <summary>
        /// Tổng chi phiếu
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Tài khoản nợ
        /// </summary>
        public Guid DebitAccount { get; set; }
        /// <summary>
        /// Số tài khoản nợ
        /// </summary>
        public String? DebitAccountNumber { get; set; }

        /// <summary>
        /// Tài khoản có
        /// </summary>
        public Guid CreditAccount { get; set; }
        /// <summary>
        /// Số tài khoản có
        /// </summary>
        public String? CreditAccountNumber { get; set; }

        /// <summary>
        /// Diễn giải
        /// </summary>
        public string? Description { get; set; }
    }
}
