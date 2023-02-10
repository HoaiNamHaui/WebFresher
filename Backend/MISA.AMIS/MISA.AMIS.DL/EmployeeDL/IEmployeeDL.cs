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
    public interface IEmployeeDL
    {
        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên lớn nhất</returns>
        string GetMaxEmployeeCode();

        /// <summary>
        /// Lấy nhân viên theo mã nhân viên
        /// </summary>
        /// <returns>trả về 1 nếu tìm thấy</returns>
        int GetByEmployeeCode(string employeeCode);

        /// <summary>
        /// Tìm nhân viên theo id
        /// </summary>
        /// <param name="id">Id nhân viên</param>
        /// <returns>đối tượng nhân viên tìm được</returns>
        Employee GetEmployeeById(Guid id);

        /// <summary>
        /// API lấy danh sách nhân viên lọc theo trang
        /// </summary>
        /// <returns>Danh sách nhân viên</returns
        PagingResult<Employee> GetEmployeesByFilter(int pageNumber, int pageSize, string keyword);

        /// <summary>
        /// Thêm mới nhân viên
        /// </summary>
        /// <param name="employee">Đối tượng nhân viên cần thêm mới</param>
        /// <returns>
        /// 1: insert thành công
        /// 0: insert thất bại
        /// </returns>
        dynamic InsertEmployee(Employee employee);

        /// <summary>
        /// Sửa thông tin nhân viên
        /// </summary>
        /// <param name="id">Id nhân viên</param>
        /// <returns>
        /// 1: Sửa thành công
        /// 0: Sửa thất bại
        /// </returns>
        dynamic UpdateEmployee(Guid EmployeeId, Employee employee);

        /// <summary>
        /// Xóa 1 nhân viên
        /// </summary>
        /// <param name="EmployeeId">ID nhân viên</param>
        /// <returns></returns>
        int DeleteEmployee(Guid EmployeeId);
    }
}
