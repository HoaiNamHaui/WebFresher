using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Common.Entities
{
    /// <summary>
    /// Thông tin phòng ban
    /// </summary>
    /// Created by: NHNam (16/1/2023)
    public class Department
    {
        /// <summary>
        /// ID phòng ban
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string DepartmentName { get; set; }

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
