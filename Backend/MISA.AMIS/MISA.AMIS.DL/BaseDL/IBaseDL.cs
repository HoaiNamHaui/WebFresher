using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.DL.BaseDL
{
    public interface IBaseDL<T>
    {
        /// <summary>
        /// Thêm 1 bản ghi
        /// </summary>
        /// <param name="record">bản ghi cần thêm</param>
        /// <returns>Trả về 1 nếu thành công</returns>
        /// Created by; NHNam (8/1/2023)
        int InsertRecord(T record);
    }
}
