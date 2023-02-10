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
        
        /// <summary>
        /// sửa bản ghi
        /// </summary>
        /// <param name="record">bản ghi cần sửa</param>
        /// <returns>Trả về 1 nếu thành công</returns>
        /// Created by: NHNam (8/1/2023)
        ServiceResult UpdateRecord(T record, Guid recordId);

        /// <summary>
        /// API lấy danh sách lọc theo trang
        /// </summary>
        /// <returns>Danh sách</returns
        PagingResult<T> GetByFilter(int pageNumber, int pageSize, string keyword);

        /// <summary>
        /// Xóa 1 nhân viên
        /// </summary>
        /// <param name="EmployeeId">ID nhân viên</param>
        /// <returns>1 nếu thành công</returns>
        int DeleteRecord(Guid id);

        /// <summary>
        /// Tìm bản ghi theo id
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <returns>bản ghi tìm được</returns>
        T GetById(Guid id);
        #endregion
    }
}
