using MISA.AMIS.Common.Entities;
using MISA.AMIS.DL.ObjectDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.BL.MObjectBL
{
    public class MObjectBL : IMObjectBL
    {
        #region Field

        private IMObjectDL _mObjectDL;

        #endregion

        #region Constructor

        public MObjectBL(IMObjectDL MObjectDL)
        {
            _mObjectDL = MObjectDL;
        }
        #endregion

        #region Methods

        public List<MObject> GetAll()
        {
            return _mObjectDL.GetAll();
        }

        #endregion
    }
}
