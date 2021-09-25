module.exports = {
  apiList: {
    baseApi: "https://localhost:44350/api/v1/",
    departmentApi: "https://localhost:44350/api/v1/Departments/",
    positionApi: "https://localhost:44350/api/v1/Positions/",
    employeeApi: "https://localhost:44350/api/v1/Employees/",
    customerApi: "https://localhost:44350/api/v1/Custoemers/"
  },

  /**
   * table
   */
  tableEmployeeHeaders: [
    { type: "checkbox" },
    { fieldName: 'employee_code', label: 'MÃ NHÂN VIÊN', textAlign: 'text-left', type: "normal" },
    { fieldName: 'employee_name', label: 'TÊN NHÂN VIÊN', textAlign: 'text-left', type: "normal" },
    { fieldName: 'gender', label: 'GIỚI TÍNH', textAlign: 'text-left', type: "normal" },
    { fieldName: 'date_of_birth', label: 'NGÀY SINH', textAlign: 'text-center', type: "normal" },
    { fieldName: 'identity_number', label: 'SỐ CMND', textAlign: 'text-left', type: "normal" },
    { fieldName: 'position', label: 'CHỨC DANH', textAlign: 'text-left', type: "normal" },
    { fieldName: 'department_name', label: 'TÊN ĐƠN VỊ', textAlign: 'text-left', type: "normal" },
    { label: 'CHỨC NĂNG', type:'contextmenu' }
  ],
  /**
   * mảng chứa thông tin tiêu đề bảng chứng từ nhập kho
   */
  tableInwardListHeaders: [
    { type: "checkbox" },
    { fieldName: 'mathematics_date', label: 'NGÀY HẠCH TOÁN', textAlign: 'text-center', type: "normal", footerValue:"Tổng", format:"date"},
    { fieldName: 'voucher_code', label: 'SỐ CHỨNG TỪ', textAlign: 'text-left', type: "normal" },
    { fieldName: 'description', label: 'DIỄN GIẢI', textAlign: 'text-left', type: "normal" },
    { fieldName: 'total_price', label: 'TỔNG TIỀN', textAlign: 'text-right', type: "normal" , footerValue:0},
    { fieldName: 'contact_name', label: 'NGƯỜI GIAO/NGƯỜI NHẬN', textAlign: 'text-left', type: "normal" },
    { fieldName: 'voucher_type', label: 'LOẠI CHỨNG TỪ', textAlign: 'text-left', type: "normal" },
    { label: 'CHỨC NĂNG', type:'contextmenu' }
  ],
 
  /**mảng chứa thông tin tiêu đề bảng hàng tiền trong phiếu nhập kho */
  tableInwardDetailHeaders: [
    { commodity_code: "MÃ HÀNG", type: "0" },
    { commodity_name: "TÊN HÀNG", type: "0" },
    { warehouse_code: "KHO", type: "0" },
    { debit_account: "TK NỢ", type: "0" },
    { credit_account: "TK CÓ", type: "0" },
    { unit: "DVT", type: "0" },
    { quantity: "SỐ LƯỢNG", type: "2" },
    { debit_amount: "ĐƠN GIÁ", type: "2" },
    { price: "THÀNH TIỀN", type: "2" },

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
  },

  WAREHOUSE_FUNCTION_LABELS: {
    Warehouse: 'Kho',
    GoodsAndMaterials: 'Vật tư hàng hoá',
    Unit: 'Đơn vị tính',
    Utilities: 'Tiện ích'
  },
  //MENU
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
  ],
  //DATA DROPDOWN
  pagingDropdownList: [
    { data: 10, value: "10 bản ghi trên trang" },
    { data: 20, value: "20 bản ghi trên trang" },
    { data: 30, value: "30 bản ghi trên trang" },
    { data: 50, value: "50 bản ghi trên trang" },
    { data: 100, value: "100 bản ghi trên trang" },
  ],
  //Inward Type List
  inwardTypeList: [
    { data: "Nhập kho thành phẩm sản xuất", value: "1. Thành phẩm sản xuất" },
    { data: "Nhập kho từ hàng bán trả lại", value: "2. Hàng bán bị trả lại" },
    { data: "Nhập kho khác", value: "3. Khác (NVL thừa, HH thuê gia công, ...)" },
  ],
  inwardMethodList: [
    { data: "Nhập đơn giá bằng tay", value: "Nhập đơn giá bằng tay" },
    { data: "Lấy từ giá xuất kho", value: "Lấy từ giá xuất kho" },
  ],
  /**tooltip */
  tooltipType: {
    NORMAL: 0,
    ADVANCED: 1
  },
  /**
   * metion state dropdown list
   */
  mentionStateDropdownList: [
    {data: "", value: "Tất cả"},
    {data: 1, value: "Đã ghi sổ"},
    {data: 0, value: "Chưa ghi sổ"}
  ],
  /**
   * loại nhập kho
   */
  inwardTypeDropdownList: [
    {data: "", value: "Tất cả"},
    {data: "NK001", value: "Nhập kho thành phẩm sản xuất"},
    {data: "NK002", value: "Nhập kho từ hàng bán trả lại"},
    {data: "NK003", value:"Nhập kho khác"}
  ],
  /**
   * Kỳ báo cáo
   */
  timeReportDropdownList: [
    {data: "today", value: "Hôm nay"},
    {data: "week", value: "Tuần này"},
    {data: "month", value: "Tháng này"},
    {data: "year", value: "Đầu năm đến hiện tại"},
    {data: "jan", value: "Tháng 1"},
    {data: "feb", value: "Tháng 2"},
    {data: "mar", value: "Tháng 3"},
    {data: "apr", value: "Tháng 4"},
    {data: "may", value: "Tháng 5"},
    {data: "june", value: "Tháng 6"},
    {data: "july", value: "Tháng 7"},
    {data: "aug", value: "Tháng 8"},
    {data: "sep", value: "Tháng 9"},
    {data: "oct", value: "Tháng 10"},
    {data: "nov", value: "Tháng 11"},
    {data: "dec", value: "Tháng 12"},

  ]
}

