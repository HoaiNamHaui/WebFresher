using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.AMIS.Common.Entities;
namespace MISA.AMIS.DL.ObjectDL
{
    /// <summary>
    /// Lấy danh sách Object
    /// </summary>
    public interface IMObjectDL
    {
        List<MObject> GetAll();
    }
}
