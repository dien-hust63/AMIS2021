using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Entities
{
    public class CommodityGroup:BaseEntity
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid commoditygroup_id { get; set; }

        /// <summary>
        /// Mã nhóm hàng hóa
        /// </summary>
        public string commodity_group_code { get; set; }

        /// <summary>
        /// Tên nhóm hàng hóa
        /// </summary>
        public string commodity_group_name { get; set; }

        /// <summary>
        /// Nhóm hàng hóa chính
        /// </summary>
        public int is_main { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string description { get; set; }
    }
}
