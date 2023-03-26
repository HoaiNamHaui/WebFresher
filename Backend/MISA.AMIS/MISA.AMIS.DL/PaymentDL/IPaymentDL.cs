using MISA.AMIS.Common.Entities;
using MISA.AMIS.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.DL.PaymentDL
{
    public interface IPaymentDL : IBaseDL<Payment>
    {
        /// <summary>
        /// Xóa hoàng loạt
        /// </summary>
        /// <param name="ids">mảng các id</param>
        /// <returns>Số bản ghi xóa</returns>
        /// CreatedBy: NHNam(12/2/2023)
        int DeleteMultiple(IEnumerable<Guid> ids);

        /// <summary>
        /// Lấy số phiếu chi max
        /// </summary>
        /// <returns>Số phiếu chi lớn nhất</returns>
        /// CreatedBy: NHNam(3/2/2023)
        string GetMaxRefNo();

        /// <summary>
        /// Lấy phiếu chi theo số phiếu chi
        /// </summary>
        /// <param name="id"></param>
        /// <param name="refNo">Số phiếu chi</param>
        /// <returns>Tài khỏan</returns>
        /// CreatedBy: NHNam(12/2/2023)
        Payment GetPaymentByRefNo(Guid? id, string refNo);
    }
}
