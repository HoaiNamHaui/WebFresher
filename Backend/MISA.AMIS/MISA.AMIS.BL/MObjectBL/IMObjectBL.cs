using MISA.AMIS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.BL.MObjectBL
{
    public interface IMObjectBL
    {
        /// <summary>
        /// Lấy danh sách Object
        /// </summary>
        List<MObject> GetAll();
    }
}
