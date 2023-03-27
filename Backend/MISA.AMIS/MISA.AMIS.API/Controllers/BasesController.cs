using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.BL.BaseBL;
using MISA.AMIS.Common;
using MISA.AMIS.Common.Entities;
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

        /// <summary>
        /// Thêm mới 1 bản ghi
        /// </summary>
        /// <param name="record">Bản ghi cần thêm</param>
        /// <returns></returns>
        /// Created by: NHNam (8/1/2023)
        [HttpPost]
        public virtual IActionResult InsertRecord(T record)
        {
            try
            {
                var result = _baseBL.InsertRecord(record);
                if (result.IsSuccess)
                {
                    return StatusCode(StatusCodes.Status201Created, result.Id);
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
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Sửa bản ghi
        /// </summary>
        /// <param name="record">Bản ghi cần Sửa</param>
        /// <returns></returns>
        /// Created by: NHNam (8/1/2023)
        [HttpPut("{id}")]
        public virtual IActionResult UpdateRecord([FromRoute] Guid id, [FromBody] T record)
        {
            try
            {
                var result = _baseBL.UpdateRecord(record, id);
                if (result.IsSuccess)
                {
                    return StatusCode(StatusCodes.Status200OK, 1);
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
                return HandleException(ex);
            }
        }

        /// <summary>
        /// API lấy danh sách lọc theo trang
        /// </summary>
        /// <returns>Danh sách</returns
        /// CreatedBy: NHNam (16/1/2023)
        [HttpGet("Filter")]
        public IActionResult GetByFilter(
            [FromQuery] string? keyword = "",

            [FromQuery] int pageSize = 10,

            [FromQuery] int pageNumber = 1)
        {
            try
            {
                var result = new PagingResult<T>();
                result = _baseBL.GetByFilter(pageNumber, pageSize, keyword);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Tìm nhân viên theo ID
        /// </summary>
        /// <param name="id">ID record</param>
        /// <returns>Employee</returns>
        /// Created by: NHNam (16/1/2023)
        [HttpGet("{id}")]
        public IActionResult GetEmployeeById([FromRoute] Guid id)
        {
            try
            {
                T record;
                record = _baseBL.GetById(id);

                return StatusCode(StatusCodes.Status200OK, record);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Xóa 1 nhân viên
        /// </summary>
        /// <param name="id">ID bản ghi</param>
        /// <returns>Danh sách mới</returns>
        /// Created by: NHNam (8/1/2023)
        [HttpDelete("{id}")]
        public virtual IActionResult DeleteRecord([FromRoute] Guid id)
        {
            try
            {
                int result;

                result = _baseBL.DeleteRecord(id);

                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        /// <summary>
        /// Xử lý ngoại lệ trả về lỗi
        /// </summary>
        /// <param name="ex">ngoại lệ</param>
        /// <returns>devMsg và userMsg</returns>
        /// Created by: NHNam (8/1/2023)
        private IActionResult HandleException(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            var res = new ErrorResult
            {
                DevMsg = ex.Message,
                UserMsg = Resource.MISAError
            };
            return StatusCode(StatusCodes.Status500InternalServerError, res);
        }
    }
}
