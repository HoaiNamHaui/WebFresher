using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Common.Entities
{
    /// <summary>
    /// Thông tin tài khoản
    /// </summary>
    public class Account : BaseEntities
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Key]
        public Guid AccountId { get; set; }
        
        /// <summary>
        /// Id tài khoản cha
        /// </summary>
        public Guid ParentId { get; set; }
        
        /// <summary>
        /// Số tài khoản
        /// </summary>
        public string AccountNumber { get; set; }
        
        /// <summary>
        /// Tên tài khoản
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// Tên tiếng anh
        /// </summary>
        public string EnglishName { get; set; }

        /// <summary>
        /// Tính chất
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Tên tính chất
        /// </summary>
        public string? TypeName { get; set; }

        /// <summary>
        /// Diễn giải
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Có hạch toán ngoại tệ
        /// </summary>
        public bool HasForeignCurrenCyAccounting { get; set; }

        /// <summary>
        /// Trạng thái sử dụng
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public string Active { get; set; }

        /// <summary>
        /// Là tài khoản cha
        /// </summary>
        public bool IsParent { get; set; }

        /// <summary>
        /// Theo dõi theo đối tượng
        /// </summary>
        public bool IsTrackObject { get; set; }

        /// <summary>
        /// Theo dõi theo đối tượng THCP
        /// </summary>
        public bool IsTrackJob { get; set; }

        /// <summary>
        /// Theo dõi theo đơn hàng
        /// </summary>
        public bool IsTrackOrder { get; set; }

        /// <summary>
        /// Theo dõi theo hợp đồng mua
        /// </summary>
        public bool IsTrackPurchaseContract { get; set; }

        /// <summary>
        /// Theo dõi theo đơn vị
        /// </summary>
        public bool IsTrackOrganizationUnit { get; set; }


        /// <summary>
        /// Theo dõi theo tài khoản ngân hàng
        /// </summary>
        public bool IsTrackBankAccount { get; set; }

        /// <summary>
        /// Theo dõi theo công trình
        /// </summary>
        public bool IsTrackProjectWork { get; set; }

        /// <summary>
        /// Theo dõi theo hợp đồng bán
        /// </summary>
        public bool IsTrackSaleContract { get; set; }

        /// <summary>
        /// Theo dõi theo khoản mục chi phí
        /// </summary>
        public bool IsTrackExpenseItem { get; set; }

        /// <summary>
        /// Theo dõi mã thống kê
        /// </summary>
        public bool IsTrackItem { get; set; }

        /// <summary>
        /// Đối tượng
        /// </summary>
        public int Object { get; set; }

        /// <summary>
        /// Tổng hợp chi phí
        /// </summary>
        public int Job { get; set; }

        /// <summary>
        /// Đơn hàng
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Hợp đồng mua
        /// </summary>
        public int PurchaseContract { get; set; }


        /// <summary>
        /// Đơn vị
        /// </summary>
        public int Unit { get; set; }

        /// <summary>
        /// Tài khoản ngân hàng
        /// </summary>
        public int BankAccount { get; set; }

        /// <summary>
        /// Công trình
        /// </summary>
        public int ProjectWork { get; set; }

        /// <summary>
        /// Hợp đồng bán
        /// </summary>
        public int SaleContract { get; set; }

        /// <summary>
        /// Khoản mục chi phí
        /// </summary>
        public int ExpenseItem { get; set; }

        /// <summary>
        /// Mã thống kê
        /// </summary>
        public int Item { get; set; }

        /// <summary>
        /// Bậc tài khoản
        /// </summary>
        public int Grade { get; set; }
    }
}
