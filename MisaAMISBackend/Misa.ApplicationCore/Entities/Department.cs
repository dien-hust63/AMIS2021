using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Entities
{
    public class Department
    {
        #region Property
        /// <summary>
        /// Id đơn vị
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Mã đơn vị
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Mô tả đơn vị
        /// </summary>
        public string Description { get; set; }
        #endregion
    }
}
