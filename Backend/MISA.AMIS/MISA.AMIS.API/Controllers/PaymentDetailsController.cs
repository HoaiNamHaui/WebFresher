using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.BL.PaymentBL;
using MISA.AMIS.BL.PaymentDetailBL;
using MISA.AMIS.Common;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.Common.Entities.DTO;

namespace MISA.AMIS.API.Controllers
{
    public class PaymentDetailsController : BasesController<PaymentDetail>
    {
        #region Feild

        private IPaymentDetailBL _paymentDetailBL;

        #endregion

        #region Constructor

        public PaymentDetailsController(IPaymentDetailBL paymentDetailBL) : base(paymentDetailBL)
        {
            _paymentDetailBL = paymentDetailBL;
        }

        #endregion


        /// <summary>
        /// API lấy danh sách lọc theo trang
        /// </summary>
        /// <returns>Danh sách</returns
        /// CreatedBy: NHNam (16/1/2023)
        [HttpGet("GetByPaymentIdAndPaging")]
        public IActionResult GetByPaymentIdAndPaging(
            [FromQuery] Guid ParentId,

            [FromQuery] int pageSize = 10,

            [FromQuery] int pageNumber = 1)
        {
            try
            {
                var result = new PagingResult<PaymentDetail>();
                result = _paymentDetailBL.GetByPaymentIdAndPaging(pageNumber, pageSize, ParentId);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
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

        /// <summary>
        /// Tìm nhân viên theo ID
        /// </summary>
        /// <param name="id">ID record</param>
        /// <returns>Employee</returns>
        /// Created by: NHNam (16/1/2023)
        [HttpGet("{id}")]
        public IActionResult GetByPaymentId([FromRoute] Guid id)
        {
            try
            {
                var result = new List<PaymentDetail>();
                result = _paymentDetailBL.GetByPaymentId(id);

                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
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
}
