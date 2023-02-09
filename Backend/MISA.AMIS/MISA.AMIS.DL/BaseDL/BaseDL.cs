using Dapper;
using MISA.AMIS.Common.Constans;
using MISA.AMIS.Common.Entities;
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
    }
}
