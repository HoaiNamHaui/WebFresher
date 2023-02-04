using MISA.AMIS.API.Enums;
using System.ComponentModel.DataAnnotations;

namespace MISA.AMIS.API.Entities
{
    /// <summary>
    /// Thông tin nhân viên
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Key]
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [Required(ErrorMessage ="Mã nhân viên không được bỏ trống")]        
        
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        [Required(ErrorMessage = "Tên nhân viên không được bỏ trống")]
        [StringLength(maximumLength: 255)]
        public string FullName { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public Gender? Gender { get; set; }
        
        /// <summary>
        /// Tên giới tính
        /// </summary>
        public string? GenderName { get; set; }

        /// <summary>
        /// Khóa ngoại
        /// </summary>
        [Required]
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string? DepartmentName { get; set; }

        /// <summary>
        /// Số CMND
        /// </summary>
        /// 
        public string? IdentityNumber { get; set; }
        
        /// <summary>
        /// Ngày cấp
        /// </summary>
        public DateTime? IdentityDate { get; set; }
        
        /// <summary>
        /// Nơi cấp
        /// </summary>
        public string? IdentityPlace { get; set; }
        
        /// <summary>
        /// Vị trí
        /// </summary>
        public string? PositionName { get; set; }
        
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string? Address { get; set; }
        
        /// <summary>
        /// Điện thoại di động
        /// </summary>
        public string? PhoneNumber { get; set; }
        
        /// <summary>
        /// Điện thoại cố định
        /// </summary>
        public string? TelephoneNumber { get; set; }
        
        /// <summary>
        /// Email
        /// </summary>
        public string? Email { get; set; }
        
        /// <summary>
        /// Số tài khoản ngân hàng
        /// </summary>
        public string? BankAccount { get; set; }
        
        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public string? BankName { get; set; }
        
        /// <summary>
        /// Chi nhánh
        /// </summary>
        public string? BankBranchName { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Người thay đổi cuối cùng
        /// </summary>
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// Ngày thay đổi gần nhất
        /// </summary>
        public DateTime ModifiedDate { get; set; }
    }
}
