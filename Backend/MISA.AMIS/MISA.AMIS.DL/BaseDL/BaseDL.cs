using Dapper;
using MISA.AMIS.Common.Constans;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.Common.Entities.DTO;
using MISA.AMIS.DL.EmployeeDL;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.DL.BaseDL   
{
    public class BaseDL<T> : IBaseDL<T>
    {
        /// <summary>
        /// Thêm mới 1 bản ghi
        /// </summary>
        /// <param name="record">Bản ghi cần thêm</param>
        /// <returns>1 nếu thêm thành công</returns>
        /// CreatedBy: NHNam(3/2/2023)
        public int InsertRecord(T record)
        {
            string storedProcedureName = String.Format(ProcedureName.Insert, typeof(T).Name, "");
            var parameters = new DynamicParameters();
            foreach (var prop in record.GetType().GetProperties())
            {
                parameters.Add("p_" + prop.Name, prop.GetValue(record, null));
            }
            int numberOfAffectedRow = 0;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();
                numberOfAffectedRow = mySqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                mySqlConnection.Close();
            }
            return numberOfAffectedRow;
        }

        /// <summary>
        /// Sửa thông tin bản ghi
        /// </summary>
        /// <param name="record">bản ghi cần sửa</param>
        /// <param name="recordId">Id của bản ghi</param>
        /// <returns>1 nếu thêm thành công</returns>
        /// CreatedBy: NHNam(3/2/2023)
        public int UpdateRecord(T record, Guid recordId)
        {
            try
            {
                string storedProcedureName = String.Format(ProcedureName.Update, typeof(T).Name);
                var parameters = new DynamicParameters();
                foreach (var prop in record.GetType().GetProperties())
                {
                    parameters.Add("p_" + prop.Name, prop.GetValue(record, null));
                }
                int numberOfAffectedRow = 0;
                using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
                {
                    numberOfAffectedRow = mySqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                }
                return numberOfAffectedRow;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        /// <summary>
        /// Lọc phân trang
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
        /// CreatedBy: NHNam(3/2/2023)
        public PagingResult<T> GetByFilter(int pageNumber, int pageSize, string keyword)
        {
            var result = new PagingResult<T>();
            string storedProcedureName = String.Format(ProcedureName.GetByFilter, typeof(T).Name, "");
            var parameters = new DynamicParameters();
            parameters.Add("p_PageNumber", pageNumber);
            parameters.Add("p_PageSize", pageSize);
            parameters.Add("p_TextSearch", keyword);
            parameters.Add("p_TotalRecord", direction: System.Data.ParameterDirection.Output);
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();
                var multy = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                result.Data = multy.Read<T>().ToList();
                result.TotalRecord = parameters.Get<int>("p_TotalRecord");
                mySqlConnection.Close();
                result.CurrentPageRecords = result.Data.Count;
                result.CurrentPage = pageNumber;   
            }
            return result;
        }

        /// <summary>
        /// Tìm bản ghi theo id
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <returns>bản ghi tìm được</returns>
        /// CreatedBy: NHNam(3/2/2023)
        public T GetById(Guid id)
        {
            // Khởi tạo Employee
            T record;
            string storedProcedureName = String.Format(ProcedureName.GetById, typeof(T).Name, "");
            var parameters = new DynamicParameters();
            parameters.Add($"p_{typeof(T).Name}Id", id);
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                record = mySqlConnection.QueryFirstOrDefault<T>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
            return record;
        }

        /// <summary>
        /// Xóa 1 bản ghi
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <returns></returns>
        /// CreatedBy: NHNam(3/2/2023)
        public int DeleteRecord(Guid id)
        {
            int result;
            string storedProcedureName = String.Format(ProcedureName.Delete, typeof(T).Name);
            var parameters = new DynamicParameters();
            parameters.Add($"p_{typeof(T).Name}Id", id);
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();
                var sqlDel = mySqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                result = sqlDel;
                mySqlConnection.Close();
            }
            return result;
        }
    }
}
