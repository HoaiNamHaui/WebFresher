using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Common.Entities
{
    public class BaseEntities
    {
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Người thay đổi cuối cùng
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Ngày thay đổi gần nhất
        /// </summary>
        public DateTime ModifiedDate { get; set; }
    }
}
