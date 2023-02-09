using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Common.Constans
{
    public static class ProcedureName
    {
        /// <summary>
        /// Thêm mới
        /// </summary>
        public static string Insert = "Proc_{0}_Insert{1}";
        
        /// <summary>
        /// Sửa
        /// </summary>
        public static string Update = "Proc_{0}_Update";
        
        /// <summary>
        /// Xóa
        /// </summary>
        public static string Delete = "Proc_{0}_Delete";
        
        /// <summary>
        /// Tìm theo id
        /// </summary>
        public static string GetById = "Proc_{0}_GetById";
        
        /// <summary>
        /// Lấy tất cả
        /// </summary>
        public static string GetAll = "Proc_{0}_GetAll";
        
        /// <summary>
        /// Lấy theo lọc phân trang
        /// </summary>
        public static string GetByFilter = "Proc_{0}_GetByFilter";
        
        /// <summary>
        /// lấy theo mã
        /// </summary>
        public static string GetByCode = "Proc_{0}_GetByCode";

    }
}
