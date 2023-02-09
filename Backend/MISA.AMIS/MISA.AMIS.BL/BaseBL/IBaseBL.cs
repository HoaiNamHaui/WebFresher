using MISA.AMIS.Common.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.BL.BaseBL
{
    public interface IBaseBL<T>
    {
        #region Method

        /// <summary>
        /// Thêm 1 bản ghi
        /// </summary>
        /// <param name="record">bản ghi cần thêm</param>
        /// <returns>Trả về 1 nếu thành công</returns>
        /// Created by; NHNam (8/1/2023)
        ServiceResult InsertRecord(T record);

        #endregion
    }
}
