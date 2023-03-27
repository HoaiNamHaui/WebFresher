﻿using MISA.AMIS.BL.BaseBL;
using MISA.AMIS.Common;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.Common.Entities.DTO;
using MISA.AMIS.DL.AccountDL;
using MISA.AMIS.DL.BaseDL;
using MISA.AMIS.DL.EmployeeDL;
using MISA.AMIS.DL.PaymentDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.BL.PaymentBL
{
    public class PaymentBL : BaseBL<Payment>, IPaymentBL
    {
        #region Field

        private IPaymentDL _paymentDL;

        #endregion

        #region Constructor

        public PaymentBL(IPaymentDL paymentDL) : base(paymentDL)
        {
            _paymentDL = paymentDL;
        }
        #endregion


        /// <summary>
        /// Xóa hàng loạt
        /// </summary>
        /// <param name="ids">mảng id</param>
        /// <returns>Số bản ghi xóa</returns>
        /// CreatedBy: NHNam(3/2/2023)
        public int DeleteMultiple(IEnumerable<Guid> ids)
        {
            if (ids == null || !ids.Any())
            {
                throw new ArgumentException(Resource.ListIdInvalid);
            }

            var numDeleted = 0;
            try
            {
                // Gọi phương thức xóa hàng loạt trong một transaction
                numDeleted = _paymentDL.DeleteMultiple(ids);
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu xảy ra lỗi trong quá trình xóa
                throw new Exception(Resource.DeleteMultipleError + ": ", ex);
            }

            return numDeleted;
        }

        /// <summary>
        /// lấy phiếu chi mới
        /// </summary>
        /// <returns>số phiếu chi mới</returns>
        /// CreatedBy: NHNam(3/2/2023)
        public string GetNewNoRef()
        {
            string refNoMax = _paymentDL.GetMaxRefNo();
            string refNo = refNoMax.Replace("PC", "");
            string newRefNo = "PC" + (int.Parse(refNo) + 1);
            return newRefNo;
        }

        /// <summary>
        /// Xuất khẩu dữ liệu ra excel
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<Payment> ExportToExcel(int pageNumber, int pageSize, string keyword)
        {
            var data = _paymentDL.GetByFilter(pageNumber, pageSize, keyword).Data;
            return data;
        }

        /// <summary>
        /// Validate custom theo đối tượng cụ thể
        /// </summary>
        /// <param name="account">Tài khoản validate</param>
        /// <returns>Kết quả validate</returns>
        /// CreatedBy: NHNam(22/3/2023)
        protected override ValidateResult ValidateCustom(Payment payment, Guid? id)
        {
            ValidateResult validateResult = new ValidateResult();
            validateResult.IsSuccess = true;
            // Trùng số tài khoản
            var acc = _paymentDL.GetPaymentByRefNo(id, payment.RefNo);
            if (acc != null)
            {
                validateResult.ListError.Add(Resource.DuplicateRefNo);
                validateResult.IsSuccess = false;
            }

            // Bỏ trống số phiếu chi
            if (string.IsNullOrEmpty(payment.RefNo))
            {
                validateResult.ListError.Add(Resource.EmptyRefNo);
                validateResult.IsSuccess = false;
            }

            // Bỏ trống ngày hạch toán
            if (payment.PostedDate  == null)
            {
                validateResult.ListError.Add(Resource.EmptyPostedDate);
                validateResult.IsSuccess = false;
            }

            // Bỏ trống ngày chứng từ
            if (payment.RefDate == null)
            {
                validateResult.ListError.Add(Resource.EmptyRefDate);
                validateResult.IsSuccess = false;
            }
            return validateResult;
        }

    }
}
