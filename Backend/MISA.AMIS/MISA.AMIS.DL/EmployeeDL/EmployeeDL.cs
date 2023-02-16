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
        /// Lấy data từ DB
        /// </summary>
        /// <returns>data</returns>
        public dynamic ExportToExcel()
        {
            var data = new List<Employee>();
            var sqlCommand = "SELECT * FROM Employee";
            var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);
            data = mySqlConnection.Query<Employee>(sqlCommand).ToList();
            return data;
        }

        /// <summary>
        /// Lấy nhân viên theo mã nhân viên
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employeeCode"></param>
        /// <returns>Nhân viên</returns>
        /// CreatedBy: NHNam(12/2/2023)
        public Employee GetEmployeeByCode(Guid? id, string employeeCode)
        {
            // Khởi tạo Employee
            var result = new Employee();
            string storedProcedureName = "Proc_Employee_CheckDuplicate";
            var parameters = new DynamicParameters();
            parameters.Add("p_EmployeeCode", employeeCode);
            parameters.Add("p_EmployeeId", id);
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                result = mySqlConnection.QueryFirstOrDefault<Employee>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
            return result;
        }

        /// <summary>
        /// Xóa hoàng loạt
        /// </summary>
        /// <param name="ids">mảng các id</param>
        /// <returns>Số bản ghi xóa</returns>
        /// CreatedBy: NHNam(12/2/2023)
        public int DeleteMultiple(IEnumerable<Guid> ids)
        {
            using( var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();
                using(var transaction = mySqlConnection.BeginTransaction())
                {
                    try
                    {
                        var sql = "DELETE FROM Employee WHERE EmployeeId = @id";
                        foreach (var id in ids)
                        {
                            var affectedRows = mySqlConnection.Execute(sql, new { id }, transaction);
                        }

                        transaction.Commit();

                        return ids.Count();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }

            }
        }
    }
}
