module.exports = {
  apiList: {
    baseApi: "https://localhost:44350/api/v1/",
    departmentApi: "https://localhost:44350/api/v1/Departments/",
    positionApi: "https://localhost:44350/api/v1/Positions/",
    employeeApi: "https://localhost:44350/api/v1/Employees/",
    customerApi: "https://localhost:44350/api/v1/Custoemers/"
  },

  /**
   * mảng chứa thông tin các tiêu đề của bảng nhân viên
   * type = 0 : dữ liệu căn trái
   * type = 1 : dữ liệu căn giữa
   * type = 2 : dữ liệu căn phải
   */
  tableEmployeeHeaders: [
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
  ],
  message: {
    messageConfirmChange: "Dữ liệu đã bị thay đổi. Bạn có muốn cất không?",
    messageRequired: "{0} không được để trống",
    messageDuplication: `Mã nhân viên <{0}> đã tồn tại trong hệ thống, vui lòng kiểm tra lại.`,
    messageEmailSyntax: "Email không đúng định dạng",
    messageDateSyntax: `Ngày chưa đúng định dạng`,
    messageDateFuture: "{0} vượt quá ngày hiện tại",
  },
  mode: {
    ADD: 0,
    INSERT: 1,
    BUTTONCALLBACK: 2,
    BUTTONNORMAL: 3,
  },
  // WAREHOUSE
  WAREHOUSE_TUTORIAL_TITLE: 'Nghiệp vụ kho',
  WAREHOUSE_TABS: {
    Process: 'Quy trình',
    InwardOutwardList: 'Nhập, xuất kho',
    Report: 'Báo cáo'
  },
  WAREHOUSE_REPORT: {
    Title: 'Báo cáo',
    All: 'Tất cả báo cáo'
  },
  WAREHOUSE_FUNCTION_LABELS: {
    Warehouse: 'Kho',
    GoodsAndMaterials: 'Vật tư hàng hoá',
    Unit: 'Đơn vị tính',
    Utilities: 'Tiện ích'
  },
  WAREHOUSE_MAP_LABELS: {
    ProductionOrder: 'Lệnh sản xuất',
    AssembleDismantlingOrder: 'Lắp đặt, tháo dỡ',
    ExportInventory: 'Xuất kho',
    ImportInventory: 'Nhập kho',
    Transfer: 'Chuyển kho',
    OutwardPrice: 'Tính giá xuất kho',
    Audit: 'Kiểm kê'
  },
  listMenu: [
    { href: "/", menuIcon: "mi-sidebar-dashboard", menuItemTitle: "Tổng quan" },
    { href: "/employees", menuIcon: "mi-sidebar-cash", menuItemTitle: "Tiền mặt" },
    { href: "/", menuIcon: "mi-sidebar-bank", menuItemTitle: "Tiền gửi" },
    { href: "/", menuIcon: "mi-sidebar-pu", menuItemTitle: "Mua hàng" },
    { href: "/", menuIcon: "mi-sidebar-sale", menuItemTitle: "Bán hàng" },
    { href: "/", menuIcon: "mi-sidebar-invoice", menuItemTitle: "Quản lý hóa đơn" },
    { href: "/warehouse", menuIcon: "mi-sidebar-stock", menuItemTitle: "Kho" },
    { href: "/", menuIcon: "mi-sidebar-tools", menuItemTitle: "Công cụ dụng cụ" },
    { href: "/", menuIcon: "mi-sidebar-fixed-assets", menuItemTitle: "Tài sản cố định" },
    { href: "/", menuIcon: "mi-sidebar-tax", menuItemTitle: "Thuế" },
    { href: "/", menuIcon: "mi-sidebar-price", menuItemTitle: "Giá thành" },
    { href: "/", menuIcon: "mi-sidebar-budget", menuItemTitle: "Ngân sách" },
    { href: "/", menuIcon: "mi-sidebar-report", menuItemTitle: "Báo cáo" },
    { href: "/", menuIcon: "mi-sidebar-finance", menuItemTitle: "Phân tích tài chính" },
  ]
}

