var apiList = {
  baseApi: "https://localhost:44350/api/v1/",
  departmentApi: "https://localhost:44350/api/v1/Departments/",
  positionApi: "https://localhost:44350/api/v1/Positions/",
  employeeApi: "https://localhost:44350/api/v1/Employees/",
  customerApi: "https://localhost:44350/api/v1/Custoemers/"
}

/**
 * mảng chứa thông tin các tiêu đề của bảng nhân viên
 * type = 0 : dữ liệu căn trái
 * type = 1 : dữ liệu căn giữa
 * type = 2 : dữ liệu căn phải
 */
var tableEmployeeHeaders = [
  { EmployeeCode: "MÃ NHÂN VIÊN", type: "0" },
  { FullName: "TÊN NHÂN VIÊN", type: "0" },
  { Gender: "GIỚI TÍNH", type: "0" },
  { DateOfBirth: "NGÀY SINH", type: "1" },
  { IdentityNumber: "SỐ CMND", type: "0" },
  { PositionName: "CHỨC DANH", type: "0" },
  { DepartmentName: "TÊN ĐƠN VỊ", type: "0" },
  { BankAccount: "SỐ TÀI KHOẢN", type: "0" },
  { BankName: "TÊN NGÂN HÀNG", type: "0" },
  { BankBranch: "CHI NHÁNH TK NGÂN HÀNG", type: "0" },
]
var message = {
   messageConfirmChange : "Dữ liệu đã bị thay đổi. Bạn có muốn cất không?",
   messageRequired: "{0} không được để trống",
   messageDuplication: `Mã nhân viên <{0}> đã tồn tại trong hệ thống, vui lòng kiểm tra lại.`,
   messageEmailSyntax: "Email không đúng định dạng",
   messageDateSyntax: `Ngày chưa đúng định dạng`,
   messageDateFuture: "{0} vượt quá ngày hiện tại",
}
var mode = {
  ADD : 0,
  INSERT: 1,
  BUTTONCALLBACK: 2,
  BUTTONNORMAL: 3,
}

export { apiList, tableEmployeeHeaders, mode, message};