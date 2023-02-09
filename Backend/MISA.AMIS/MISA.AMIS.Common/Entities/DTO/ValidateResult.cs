using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Common.Entities.DTO
{
    public class ValidateResult
    {
        /// <summary>
        /// Kết quả validate: true là không có lỗi
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Danh sách lỗi
        /// </summary>
        public List<string> ListError { get; set; } = new List<string>();

    }
}
