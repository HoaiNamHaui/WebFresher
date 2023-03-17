using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Common.Entities
{
    /// <summary>
    /// Thông tin đối tượng
    /// </summary>
    public class MObject:BaseEntities
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Key]
        public Guid ObjectId { get; set; }

        /// <summary>
        /// Tên đối tượng
        /// </summary>
        public string? ObjectName { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string? PhoneNumber { get; set; }
    }
}
