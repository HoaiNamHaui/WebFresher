using Dapper;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.Common.Entities.DTO;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.AMIS.DL.EmployeeDL
{
    public class EmployeeDL : IEmployeeDL
    {
        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        public string GetNewEmployeeCode()
        {
            var sqlCommand = "SELECT MAX(EmployeeCode) FROM Employee";
            var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);
            string employeeCodeMax = mySqlConnection.QueryFirstOrDefault<String>(sqlCommand).ToString();
            return employeeCodeMax;
        }

        /// <summary>
        /// Tìm nhân viên theo id
        /// </summary>
        /// <param name="id">Id nhân viên</param>
        /// <returns>đối tượng nhân viên tìm được</returns>
        public Employee GetEmployeeById(Guid id)
        {
            // Khởi tạo Employee
            var employee = new Employee();
            string storedProcedureName = "Proc_Employee_GetById";
            var parameters = new DynamicParameters();
            parameters.Add("p_EmployeeId", id);
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();
                employee = mySqlConnection.QueryFirstOrDefault<Employee>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                mySqlConnection.Close();
            }
            return employee;
        }

        /// <summary>
        /// API lấy danh sách nhân viên lọc theo trang
        /// </summary>
        /// <returns>Danh sách nhân viên</returns
        public PagingResult GetEmployeesByFilter(int pageNumber, int pageSize, string keyword)
        {
            var result = new PagingResult();
            string storedProcedureName = "Proc_Employee_GetByFilter";
            var parameters = new DynamicParameters();
            parameters.Add("p_PageNumber", pageNumber);
            parameters.Add("p_PageSize", pageSize);
            parameters.Add("p_TextSearch", keyword);
            parameters.Add("p_TotalRecord", direction: System.Data.ParameterDirection.Output);
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();
                var multy = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                result.Data = multy.Read<Employee>().ToList();
                result.TotalRecord = parameters.Get<int>("p_TotalRecord");
                mySqlConnection.Close();
                result.CurrentPageRecords = result.Data.Count;
                result.CurrentPage = pageNumber;
                
            }
            return result;
        }

        /// <summary>
        /// Thêm mới nhân viên
        /// </summary>
        /// <param name="employee">Đối tượng nhân viên cần thêm mới</param>
        /// <returns>
        /// 1: insert thành công
        /// 0: insert thất bại
        /// </returns>
        public dynamic InsertEmployee(Employee employee)
        {
            // proc
            string procName = "Proc_Employee_Insert";

            //Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            foreach (var prop in employee.GetType().GetProperties())
            {
                parameters.Add("p_" + prop.Name, prop.GetValue(employee, null));
            }
            int numberOfAffectedRow = 0;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();
                numberOfAffectedRow = mySqlConnection.Execute(procName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                mySqlConnection.Close();
            }
            return numberOfAffectedRow;
        }

        /// <summary>
        /// Sửa thông tin nhân viên
        /// </summary>
        /// <param name="id">Id nhân viên</param>
        /// <returns>
        /// 1: Sửa thành công
        /// 0: Sửa thất bại
        /// </returns>
        public dynamic UpdateEmployee(Guid EmployeeId, Employee employee)
        {
            string procName = "Proc_Employee_Update";
            var parameters = new DynamicParameters();
            foreach (var prop in employee.GetType().GetProperties())
            {
                parameters.Add("p_" + prop.Name, prop.GetValue(employee, null));
            }
            // Khởi tạo kết nối DB
            var numberOfAffectedRows = 0;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();
                numberOfAffectedRows = mySqlConnection.Execute(procName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                mySqlConnection.Close();
            }
            if (numberOfAffectedRows > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Xóa 1 nhân viên
        /// </summary>
        /// <param name="EmployeeId">ID nhân viên</param>
        /// <returns></returns>
        public int DeleteEmployee(Guid EmployeeId)
        {
            int result;
            string storedProcedureName = "Proc_Employee_Delete";
            var parameters = new DynamicParameters();
            parameters.Add("p_EmployeeId", EmployeeId);
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
