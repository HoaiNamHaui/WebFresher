var MISAResource = {
    "vi":{
        ErrorMsg:"Có lỗi xảy ra vui lòng liên hệ MISA",
        delete: "Xóa thành công",
        add: "Thêm thành công",
        update: "Sửa thành công",
        error: {
            emptyCode: "Mã nhân viên không được bỏ trống",
            emptyName: "Tên nhân viên không được bỏ trống",
            lengthName: "Tên Nhân viên dưới 255 kí tự",
            emptyDepartment: "Đơn vị không được bỏ trống",
            age: "Tuổi nhân viên phải trên 18",
            phone: "Số điện thoại không đúng định dạng",
            email: "Email không đúng định dạng",
            identityDate: "Ngày cấp phải nhỏ hơn ngày hiện tại"
        },
        errorServerResponse:{
            500: "Có lỗi xảy ra, vui lòng liên hệ MISA để được trợ giúp!",
            400: "Dữ liệu nhập không hợp lệ!",
            404: "Không tìm thấy trang",
            otherCode: "Có lỗi xảy ra vui lòng thử lại!"
        },
        account:{
            parentMessage: "Bạn phải xóa tất cả các tài khoản con trước khi xóa tài khoản cha",
            referenceMessage: "Tài khoản đã có phát sinh trên các danh mục, chứng từ. không thể xóa."
        },
        errorAccount:{
            accountNumberEmpty: "Số tài khoản không được để trống",
            accountNameEmpty: "Tên tài khoản không được để trống",
            typeEmpty: "Tính chất không được để trống",
            duplicateAccount: "Số tài khoản đã tồn tại",
            childrenAccount: "Số tài khoản chi tiết phải bắt đầu bằng số của tài khoản tổng hợp",
            minLength: "Số tài khoản phải có độ dài >= 3 ký tự",
            maxLength: "Số tài khoản phải có độ dài <= 25 ký tự",
        },
        formAccountMode: {
            Add: "Thêm tài khoản",
            Edit: "Sửa tài khoản",
            Duplicate: "Nhân bản tài khoản"
        },
        GUID_EMPTY: "00000000-0000-0000-0000-000000000000",
        payment: {
            reason: "Chi tiền cho "
        },
        errorPayment:{
            postedDate: "Ngày hạch toán không được để trống",
            refDate : "Ngày phiếu chi không được để trống",
            invalidDate: "Ngày hạch toán phải lớn hơn ngày phiếu chi",
            refNo: "Số phiếu chi không được để trống",
            debitAccount: "Tài khoản nợ không được bỏ trống",
            creditAccount: "Tài khoản có không được bỏ trống",
            refNoMaxLength: "Độ dài số phiếu chi không được quá 20 ký tự"
        }
    },
    "en":{
        ErrorMsg: "Error!"
    },
}
export default MISAResource;