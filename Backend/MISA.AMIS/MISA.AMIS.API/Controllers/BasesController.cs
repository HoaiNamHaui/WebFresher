using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.BL.BaseBL;
using MISA.AMIS.Common;
using MISA.AMIS.Common.Entities.DTO;
using MISA.AMIS.Common.Enums;

namespace MISA.AMIS.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasesController<T> : ControllerBase
    {
        #region Field

        private IBaseBL<T> _baseBL;

        #endregion

        #region Constructor

        public BasesController(IBaseBL<T> baseBL)
        {
            _baseBL = baseBL;
        }

        #endregion

        [HttpPost]
        public IActionResult InserRecord(T record)
        {
            try
            {
                var result = _baseBL.InsertRecord(record);
                if (result.IsSuccess)
                {
                    return StatusCode(StatusCodes.Status201Created, 1);
                }
                else if (!result.IsSuccess && result.ErrorCode == ErrorCode.BAD_REQUEST)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = ErrorCode.BAD_REQUEST,
                        DevMsg = result.message,
                        UserMsg = Resource.MISAError,
                        MoreInfo = result.Data,
                        TraceId = HttpContext.TraceIdentifier

                    });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                    {
                        ErrorCode = ErrorCode.SERVER_ERROR,
                        DevMsg = Resource.ServerError,
                        UserMsg = Resource.MISAError,
                        MoreInfo = result.Data,
                        TraceId = HttpContext.TraceIdentifier

                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            
        }
    }
}
