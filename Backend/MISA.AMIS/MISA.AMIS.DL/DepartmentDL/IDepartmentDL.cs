using MISA.AMIS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.DL.DepartmentDL
{
    public interface IDepartmentDL
    {
        /// <summary>
        /// Lấy danh sách phòng ban
        /// </summary>
        /// <returns>
        /// Danh sách phòng ban
        /// </returns>
        List<Department> GetAllDepartment();

        /// <summary>
        /// Tìm phòng ban theo Id
        /// </summary>
        /// <param name="id">id phòng ban</param>
        /// <returns>thông tin phòng ban</returns>
        Department GetById(Guid id);
    }
}
