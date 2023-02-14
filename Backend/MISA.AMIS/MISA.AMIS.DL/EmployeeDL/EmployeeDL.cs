using Dapper;
using MISA.AMIS.Common.Constans;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.Common.Entities.DTO;
using MISA.AMIS.DL.BaseDL;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.AMIS.DL.EmployeeDL
{
    public class EmployeeDL : BaseDL<Employee>, IEmployeeDL
    {
        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// CreatedBy: NHNam(3/2/2023)
        public string GetMaxEmployeeCode()
        {
            var sqlCommand = "SELECT MAX(EmployeeCode) FROM Employee";
            var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);
            string employeeCodeMax = mySqlConnection.QueryFirstOrDefault<String>(sqlCommand).ToString();
            return employeeCodeMax;
        }

        /// <summary>
        /// Tìm mã nhân viên đã tồn tại
        /// </summary>
        /// <param name="employeeCode">mã nhân viên</param>
        /// <returns>1 nếu đã tồn tại</returns>
        /// CreatedBy: NHNam(3/2/2023)
        public int GetByEmployeeCode(string employeeCode)
        {
            // Khởi tạo Employee
            var result = 0;
            string storedProcedureName = "Proc_Employee_GetByCode";
            var parameters = new DynamicParameters();
            parameters.Add("p_EmployeeCode", employeeCode);
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                //mySqlConnection.Open();
                result = mySqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                //mySqlConnection.Close();
            }
            return result;
        }

        public dynamic ExportToExcel()
        {
            var data = new List<Employee>();
            var sqlCommand = "SELECT * FROM Employee";
            var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);
            data = mySqlConnection.Query<Employee>(sqlCommand).ToList();
            return data;
        }
    }
}
