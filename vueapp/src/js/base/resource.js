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
            accountNumberEmpty: "Số tài khoản không được phép bỏ trống",
            accountNameEmpty: "Tên tài khoản không được phép bỏ trống",
            typeEmpty: "Tính chất không được bỏ trống",
            duplicateAccount: "Số tài khoản đã tồn tại",
            childrenAccount: "Số tài khoản chi tiết phải bắt đầu bằng số của tài khoản tổng hợp"
        },
        formAccountMode: {
            Add: "Thêm tài khoản",
            Edit: "Sửa tài khoản",
            Duplicate: "Nhân bản tài khoản"
        },
        GUID_EMPTY: "00000000-0000-0000-0000-000000000000"
    },
    "en":{
        ErrorMsg: "Error!"
    },
}
export default MISAResource;