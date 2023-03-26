using Dapper;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.DL.ObjectDL;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.DL.Object
{
    public class MObjectDL : IMObjectDL
    {
        /// <summary>
        /// Lấy danh sách Object
        /// </summary>
        public List<MObject> GetAll()
        {
            List<MObject> result = new List<MObject>();
            string storedProcedureName = "Proc_MObject_GetAll";
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                result = mySqlConnection.Query<MObject>(storedProcedureName, commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
            return result;
        }
    }
}
