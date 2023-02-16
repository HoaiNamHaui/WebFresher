using MISA.AMIS.BL.BaseBL;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.Common.Entities.DTO;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.BL.EmployeeBL
{
    public interface IEmployeeBL : IBaseBL<Employee>
    {

        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// CreatedBy: NHNam(3/2/2023)
        string GetNewEmployeeCode();

        /// <summary>
        /// Lấy data từ DL
        /// </summary>
        /// <returns>data</returns>
        /// CreatedBy: NHNam(3/2/2023)
        List<Employee> ExportToExcel();

        /// <summary>
        /// Xóa hàng loạt
        /// </summary>
        /// <param name="ids">mảng id</param>
        /// <returns>Số bản ghi xóa</returns>
        /// CreatedBy: NHNam(3/2/2023)
        int DeleteMultiple(IEnumerable<Guid> ids);
    }
}
