using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.BL.MObjectBL;

namespace MISA.AMIS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MObjectsController : ControllerBase
    {
        #region Feild

        private IMObjectBL _mObjectBL;

        #endregion

        #region Constructor

        public MObjectsController(IMObjectBL mObjectBL)
        {
            _mObjectBL = mObjectBL;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Lấy toàn bộ Object
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var data = _mObjectBL.GetAll();
                return StatusCode(StatusCodes.Status200OK, data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex);

            }
        }

        #endregion
    }
}
