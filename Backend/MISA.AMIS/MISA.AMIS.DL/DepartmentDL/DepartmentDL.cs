using Dapper;
using MISA.AMIS.Common.Entities;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.DL.DepartmentDL
{
    public class DepartmentDL : IDepartmentDL
    {
        /// <summary>
        /// Thêm mới nhân viên
        /// </summary>
        /// <param name="employee">Đối tượng nhân viên cần thêm mới</param>
        /// <returns>
        /// Danh sách phòng ban
        /// </returns>
        public List<Department> GetAllDepartment()
        {
            // Khởi tạo list 
            var list = new List<Department>();
            //Stored Procedure
            string storedProcedureName = "Proc_Department_GetAll";
            // Khởi tạo kết nối tới DB
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                // kết nối db
                mySqlConnection.Open();

                // Gọi proc 

                var multy = mySqlConnection.QueryMultiple(storedProcedureName, commandType: System.Data.CommandType.StoredProcedure);
                // lấy kết quả

                list = multy.Read<Department>().ToList();

                // đóng kết nối db
                mySqlConnection.Close();

            }
            return list;
        }

        /// <summary>
        /// Tìm phòng ban theo Id
        /// </summary>
        /// <param name="id">id phòng ban</param>
        /// <returns>thông tin phòng ban</returns>
        public Department GetById(Guid id)
        {
            var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);
            var sqlCommand = $"SELECT * FROM Department WHERE DepartmentId = '{id.ToString()}'";
            var department = mySqlConnection.QueryFirstOrDefault<Department>(sqlCommand);

            return department;
        }
    }
}
