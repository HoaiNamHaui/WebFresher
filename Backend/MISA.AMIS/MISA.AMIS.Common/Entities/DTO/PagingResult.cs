using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Common.Entities.DTO
{
    /// <summary>
    /// Phân trang dữ liệu
    /// </summary>
    public class PagingResult<T>
    {
        /// <summary>
        /// Tổng số trang thỏa mãn điều kiện
        /// </summary>
        public int TotalPage { get; set; }

        /// <summary>
        /// Tổng số bản ghi thỏa mãn điều kiện
        /// </summary>
        public int TotalRecord { get; set; }

        /// <summary>
        /// Trang hiện tại
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Số bản ghi trên 1 trang hiện tại
        /// </summary>
        public int CurrentPageRecords { get; set; }



        /// <summary>
        /// Danh sách dữ liệu
        /// </summary>
        public List<T> Data { get; set; }
    }
}
