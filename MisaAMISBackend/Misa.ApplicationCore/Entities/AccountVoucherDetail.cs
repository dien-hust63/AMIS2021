using Misa.ApplicationCore.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Entities
{
    public class AccountVoucherDetail:BaseEntity
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid accountvoucherdetail_id { get; set; }

        /// <summary>
        /// Mã khách hàng
        /// </summary>
        [MisaRequired]
        [MisaDisplayName("Mã hàng")]
        [MisaUnique]
        [MisaExport("Mã hàng")]
        public Guid commodity_id { get; set; }

        /// <summary>
        /// Id chứng từ
        /// </summary>
        public Guid accountvoucher_id { get; set; }

        /// <summary>
        /// Id kho
        /// </summary>
        [MisaRequired]
        [MisaDisplayName("ID kho")]
        public Guid warehouse_id { get; set; }

        /// <summary>
        /// số lượng
        /// </summary>
        public int quantity { get; set; }

        /// <summary>
        /// Đơn giá
        /// </summary>
        public decimal debit_amount { get; set; }

        /// <summary>
        /// Tài khoản nợ
        /// </summary>
        [MisaRequired]
        [MisaDisplayName("Tài khoản nợ")]
        public Guid debit_account_id { get; set; }

        /// <summary>
        /// Tài khoản có
        /// </summary>
        [MisaRequired]
        [MisaDisplayName("Tài khoản có")]
        public Guid credit_account_id { get; set; }

        /// <summary>
        /// số lô
        /// </summary>
        public string lot_number { get; set; }

        /// <summary>
        /// Hạn sử dụng
        /// </summary>
        public DateTime? expiry { get; set; }

        /// <summary>
        /// Thành tiền
        /// </summary>
        public decimal total_price { get; set; }

        /// <summary>
        /// Tên hàng hóa
        /// </summary>
        public string commodity_name { get; set; }

        /// <summary>
        /// Mã hàng hóa
        /// </summary>
        [MisaNotMap]
        public string commodity_code { get; set; }

        /// <summary>
        /// Mã kho
        /// </summary>
        [MisaNotMap]
        public string warehouse_code { get; set; }

        /// <summary>
        /// Tên kho
        /// </summary>
        [MisaNotMap]
        public string warehouse_name { get; set; }

        /// <summary>
        /// Số Tk nợ
        /// </summary>
        [MisaNotMap]
        public string debit_account_number { get; set; }

        /// <summary>
        /// Tên TK nợ
        /// </summary>
        [MisaNotMap]
        public string debit_account_name { get; set; }

        /// <summary>
        /// Số TK có
        /// </summary>
        [MisaNotMap]
        public string credit_account_number { get; set; }

        /// <summary>
        /// Tên TK có
        /// </summary>
        [MisaNotMap]
        public string credit_account_name { get; set; }

        /// <summary>
        /// Danh sách các đơn vị tính
        /// </summary>
        [MisaNotMap]
        public List<CommodityUnit> units { get; set; }
        /// <summary>
        /// ID đơn vị hiện tại
        /// </summary>
        public Guid? unit_id { get; set; }
        /// <summary>
        /// Tên đơn vị tính hiện tại
        /// </summary>
        [MisaNotMap]
        public string unit_name { get; set; }
        /// <summary>
        /// Trạng thái đơn vị tính
        /// </summary>
        [MisaNotMap]
        public int is_main_unit { get; set; }

        /// <summary>
        /// Tỉ lệ so với đơn vị tính chính
        /// </summary>
        [MisaNotMap]
        public int rate { get; set; }
        /// <summary>
        /// Đơn vị tính chính
        /// </summary>
        [MisaNotMap]
        public string main_unit { get; set; }
        /// <summary>
        /// Trạng thái 
        /// </summary>
        [MisaNotMap]
        public int state { get; set; }
    }
}
