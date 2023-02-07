using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Common.Entities.DTO
{
    /// <summary>
    /// Kết quả lỗi
    /// </summary>
    public class ErrorResult
    {
        /// <summary>
        /// Thông báo cho dev
        /// </summary>
        public string DevMsg { get; set; }

        /// <summary>
        /// Thông báo cho người dùng
        /// </summary>
        public string UserMsg { get; set; }
    }
}
