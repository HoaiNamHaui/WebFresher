using MISA.AMIS.Common.Entities;
using MISA.AMIS.Common.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.BL.EmployeeBL
{
    public interface IEmployeeBL
    {

        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        string GetNewEmployeeCode();

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
        PagingResult GetEmployeesByFilter(int pageNumber, int pageSize, string keyword);

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
