using MISA.AMIS.Common.Entities;
using MISA.AMIS.Common.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.DL.BaseDL
{
    public interface IBaseDL<T>
    {
        /// <summary>
        /// Thêm 1 bản ghi
        /// </summary>
        /// <param name="record">bản ghi cần thêm</param>
        /// <returns>Trả về 1 nếu thành công</returns>
        /// Created by; NHNam (8/1/2023)
        int InsertRecord(T record);

        /// <summary>
        /// Sửa thông tin bản ghi
        /// </summary>
        /// <param name="record">bản ghi cần sửa</param>
        /// <param name="recordId">Id của bản ghi</param>
        /// <returns></returns>
        int UpdateRecord(T record, Guid recordId);

        /// <summary>
        /// Lọc phân trang
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
        PagingResult<T> GetByFilter(int pageNumber, int pageSize, string keyword);

        /// <summary>
        /// Xóa 1 bản ghi
        /// </summary>
        /// <param name="id">id của bản ghi</param>
        /// <returns>1 xóa thành công</returns>
        int DeleteRecord(Guid id);

        /// <summary>
        /// Tìm bản ghi theo id
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <returns>bản ghi tìm được</returns>
        T GetById(Guid id);
    }
}
