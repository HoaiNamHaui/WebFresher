using MISA.AMIS.Common.Entities;
using MISA.AMIS.DL.DepartmentDL;
using MISA.AMIS.DL.EmployeeDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.BL.DepartmentBL
{
    public class DepartmentBL : IDepartmentBL
    {
        #region Field

        private IDepartmentDL _departmentDL;

        #endregion

        #region Constructor

        public DepartmentBL(IDepartmentDL department)
        {
            _departmentDL = department;
        }

        #endregion


        /// <summary>
        /// Lấy danh sách phòng ban
        /// </summary>
        /// <returns></returns>
        public List<Department> GetAllDepartment()
        {
            var list = new List<Department>();
            list = _departmentDL.GetAllDepartment();
            return list;
        }

        /// <summary>
        /// Tìm phòng ban theo Id
        /// </summary>
        /// <param name="id">id phòng ban</param>
        /// <returns>thông tin phòng ban</returns>
        public Department GetById(Guid id)
        {
            var department = _departmentDL.GetById(id);
            return department;
        }
    }
}
