﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Enum
{
    /// <summary>
    /// Giới tính
    /// </summary>
    /// CreatedBy: nvdien(27/8/2021)
    /// ModifiedBy: nvdien(27/8/2021)
    public enum gender
    {
        Male = 1,
        Female = 0,
        Other = 2
    }

    /// <summary>
    /// Mode thêm sửa xóa
    /// </summary>
    public enum Mode
    {
        Add = 0,
        Update = 1,
        Delete = 2,
        Duplicate = 3,
        Success = 0,
    }
}
