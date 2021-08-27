using Misa.ApplicationCore.Attributes;
using Misa.ApplicationCore.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Entities
{
    public class Employee : BaseEntity
    {
        #region Property

        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [MisaRequired]
        [MisaDisplayName("Mã nhân viên")]
        [MisaUnique]
        [MisaExport("Mã nhân viên")]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        [MisaRequired]
        [MisaDisplayName("Tên nhân viên")]
        [MisaExport("Tên nhân viên")]
        public string FullName { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// Thông tin giới tính
        /// </summary>
        [MisaNotMap]
        [MisaExport("Giới tính")]
        public string GenderName
        {
            get
            {
                switch (Gender)
                {
                    case Gender.Male:
                        return "Nam";
                    case Gender.Female:
                        return "Nữ";
                    case Gender.Other:
                        return "Khác";
                    default:
                        return null;
                }
            }
        }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        [MisaExport("Ngày sinh")]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Số điện thoại cố định
        /// </summary>
        public string FixPhoneNumber { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Căn cước công dân
        /// </summary>
        public string IdentityNumber { get; set; }

        /// <summary>
        /// Ngày cấp căn cước công dân
        /// </summary>
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Nơi cấp
        /// </summary>
        public string IdentityPlace { get; set; }

        /// <summary>
        /// Chức danh
        /// </summary>
        [MisaExport("Chức danh")]
        public string PositionName { get; set; }

        /// <summary>
        /// Id phòng ban
        /// </summary>
        [MisaRequired]
        [MisaDisplayName("Đơn vị")]
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        /// [MisaExport]
        [MisaExport("Tên đơn vị")]
        [MisaNotMap]
        public string DepartmentName { get; set; }

        /// <summary>
        /// Tài khoản ngân hàng
        /// </summary>
        [MisaExport("Số tài khoản")]
        public string BankAccount { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        [MisaExport("Tên ngân hàng")]
        public string BankName { get; set; }

        /// <summary>
        /// Chi nhánh ngân hàng
        /// </summary>
        public string BankBranch { get; set; }



        #endregion
    }
}
