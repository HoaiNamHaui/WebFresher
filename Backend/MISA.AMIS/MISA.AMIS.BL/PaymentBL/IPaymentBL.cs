using MISA.AMIS.BL.BaseBL;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.DL.AccountDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.BL.PaymentBL
{
    public interface IPaymentBL : IBaseBL<Payment>
    {
        /// <summary>
        /// Xóa hàng loạt
        /// </summary>
        /// <param name="ids">mảng id</param>
        /// <returns>Số bản ghi xóa</returns>
        /// CreatedBy: NHNam(3/2/2023)
        int DeleteMultiple(IEnumerable<Guid> ids);

        /// <summary>
        /// lấy phiếu chi mới
        /// </summary>
        /// <returns>số phiếu chi mới</returns>
        /// CreatedBy: NHNam(3/2/2023)
        string GetNewNoRef();

        /// <summary>
        /// Lấy data từ DL
        /// </summary>
        /// <returns>data</returns>
        /// CreatedBy: NHNam(3/2/2023)
        List<Payment> ExportToExcel(int pageNumber, int pageSize, string keyword);
    }
}
