using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.Common.Entities.DTO;
using MISA.AMIS.DL.BaseDL;

namespace MISA.AMIS.DL.EmployeeDL
{
    public interface IEmployeeDL : IBaseDL<Employee>
    {
        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên lớn nhất</returns>
        /// CreatedBy: NHNam(3/2/2023)
        string GetMaxEmployeeCode();

        /// <summary>
        /// Export dữ liệu ra file excel
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: NHNam(12/2/2023)
        dynamic ExportToExcel();

        /// <summary>
        /// Lấy nhân viên theo mã nhân viên
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employeeCode"></param>
        /// <returns>Nhân viên</returns>
        /// CreatedBy: NHNam(12/2/2023)
        Employee GetEmployeeByCode(Guid? id,string employeeCode);
    }
}
