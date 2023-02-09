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
        /// Mã lỗi
        /// </summary>
        public int ErrorCode { get; set; }
        
        /// <summary>
        /// Thông báo cho dev
        /// </summary>
        public string DevMsg { get; set; }

        /// <summary>
        /// Thông báo cho người dùng
        /// </summary>
        public string UserMsg { get; set; }
       
        /// <summary>
        /// Thông tin thêm
        /// </summary>
        public object MoreInfo { get; set; }

        /// <summary>
        /// Id log lỗi
        /// </summary>
        public string TraceId { get; set; }
    }
}
