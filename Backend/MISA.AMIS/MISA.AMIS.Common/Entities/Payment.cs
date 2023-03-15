using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Common.Entities
{
    /// <summary>
    /// Thông tin phiếu chi
    /// </summary>
    public class Payment:BaseEntities
    {
        /// <summary>
        /// khóa chính
        /// </summary>
        public Guid PaymentId { get; set; }

        /// <summary>
        /// Khóa ngoại
        /// </summary>
        public Guid ObjectId { get; set; }

        /// <summary>
        /// Người nhận
        /// </summary>
        public string? Reciver { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Lí do chi
        /// </summary>
        public string? Reason { get; set; }


        /// <summary>
        /// Khóa ngoại
        /// </summary>
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// kèm theo
        /// </summary>
        public int Attachment { get; set; }

        /// <summary>
        /// Loai chi
        /// </summary>
        public int ReasonType { get; set; }

        /// <summary>
        /// Ngày chi
        /// </summary>
        public DateTime RefDate { get; set; }

        /// <summary>
        /// Ngày hạch toán
        /// </summary>
        public DateTime PostedDate { get; set;}

        /// <summary>
        /// Số phiếu chi
        /// </summary>
        public string? RefNo { get; set; }

        /// <summary>
        /// Tổng tiền tất cả phiếu chi
        /// </summary>
        public decimal TotalAmount { get; set; }
    }
}
