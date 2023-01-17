namespace MISA.AMIS.API.Entities.DTO
{
    /// <summary>
    /// 
    /// </summary>
    public class PagingResult
    {
        /// <summary>
        /// Tổng số bản ghi
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Danh sách dữ liệu
        /// </summary>
        public List<Employee> Data { get; set; }
    }
}
