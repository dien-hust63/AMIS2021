var apiList = {
  baseApi: "https://localhost:44350/api/v1/",
  departmentApi: "https://localhost:44350/api/v1/Departments/",
  positionApi: "https://localhost:44350/api/v1/Positions/",
  employeeApi: "https://localhost:44350/api/v1/Employees/",
  customerApi: "https://localhost:44350/api/v1/Custoemers/",
  warehouseGetAll: "https://localhost:44350/api/v1/Warehouses",
  accountvoucherPagingFilter:"https://localhost:44350/api/v1/AccountVouchers/filter?searchData={0}&pageIndex={1}&pageSize={2}",
  accountobjectPagingFilter:"https://localhost:44350/api/v1/AccountObjects/filter?searchData={0}&pageIndex={1}&pageSize={2}",
  employeePagingFilter:"https://localhost:44350/api/v1/Employees/filter?searchData={0}&pageIndex={1}&pageSize={2}",
  warehousePagingFilter:"https://localhost:44350/api/v1/Warehouses/filter?searchData={0}&pageIndex={1}&pageSize={2}",
  commodityPagingFilter:"https://localhost:44350/api/v1/Commoditys/filter?searchData={0}&pageIndex={1}&pageSize={2}",
  accountPagingFilter:"https://localhost:44350/api/v1/Accounts/filter?searchData={0}&pageIndex={1}&pageSize={2}",
  departmentPagingFilter:"https://localhost:44350/api/v1/Departments/filter?searchData={0}&pageIndex={1}&pageSize={2}",
  commodityGroupFilterPaging: "https://localhost:44350/api/v1/CommodityGroups/filter?searchData={0}&pageIndex={1}&pageSize={2}",
  commodityUnitFilterPaging:"https://localhost:44350/api/v1/Units/filter?searchData={0}&pageIndex={1}&pageSize={2}"
}
var tableCustomerHeaders = [
  { fieldName: 'account_object_code', label: 'Mã khách hàng', textAlign: 'text-left', type: "normal", width: '100px'},
  { fieldName: 'account_object_name', label: 'Tên khách hàng', textAlign: 'text-left', type: "normal", width: '200px' },
  { fieldName: 'contact_address', label: 'Địa chỉ', textAlign: 'text-left', type: "normal" ,width: '250px'},
];
var departmentComboboxHeaders = [
  { fieldName: 'department_code', label: 'Mã đơn vị', textAlign: 'text-left', type: "normal", width: '150px'},
  { fieldName: 'department_name', label: 'Tên đơn vị', textAlign: 'text-left', type: "normal", width: '200px' },
]
var commodityGroupComboboxHeaders =  [
  { fieldName: 'commodity_group_code', label: 'Mã nhóm vật tư, hàng hóa, dịch vụ', textAlign: 'text-left', type: "normal", width: '250px'},
  { fieldName: 'commodity_group_name', label: 'Tên nhóm vật tư, hàng hóa, dịch vụ', textAlign: 'text-left', type: "normal", width: '250px' },
]
var commodityUnitComboboxHeaders =  [
  { fieldName: 'unit_name', label: 'Tên đơn vị', textAlign: 'text-left', type: "normal", width: '145px'},
]
var warehouseComboboxHeaders = [
  { fieldName: 'warehouse_code', label: 'Mã kho', textAlign: 'text-left', type: "normal", width: '100px'},
  { fieldName: 'warehouse_name', label: 'Tên kho', textAlign: 'text-left', type: "normal", width: '200px' },
]
var commodityComboboxHeaders = [
  { fieldName: 'commodity_code', label: 'Mã hàng', textAlign: 'text-left', type: "normal", width: '100px'},
  { fieldName: 'commodity_name', label: 'Tên hàng', textAlign: 'text-left', type: "normal", width: '200px' },
]
var accountComboboxHeaders = [
  { fieldName: 'account_number', label: 'Mã tài khoản', textAlign: 'text-left', type: "normal", width: '100px'},
  { fieldName: 'account_name', label: 'Tên tài khoản', textAlign: 'text-left', type: "normal", width: '200px' },
]
var  CommodityUnitComboboxProps = {
  tableHeaders: commodityUnitComboboxHeaders,
  api: apiList['commodityUnitFilterPaging'],
  tableObject: "Units",
  valueField: "unit_name",
  mode:"api",
  hasHeader:false
}
var comboboxAccountProps = {
  tableHeaders: accountComboboxHeaders,
  api: apiList['accountPagingFilter'],
  tableObject: "Accounts",
  isRequired: true,
  mode:"api",
  fieldName: "Tài khoản",
  addForm:"AccountDetail"
}
var comboboxWarehouseProps = {
  tableHeaders: warehouseComboboxHeaders,
  api: apiList['warehousePagingFilter'],
  tableObject: "Warehouses",
  isRequired: true,
  mode:"api",
  fieldName: "Kho",
  addForm:"WarehouseAdd"
}
var comboboxCommodityProps = {
  tableHeaders: commodityComboboxHeaders,
  api: apiList['commodityPagingFilter'],
  tableObject: "Commoditys",
  isRequired: true,
  mode:"api",
  fieldName: "Mã hàng",
  addForm:"CommodityGroup"
}
var comboboxUnitProps = {
  tableHeaders: [{ fieldName: 'unit_name', label: 'Tên đơn vị', textAlign: 'text-left', type: "normal", width: '100px'}],
  tableContents: [],
  mode:"manual",
  contentFields:"units"
}
var calculationList =[
  {"calculate_name": "Phép nhân", "calculate_value":1},
  {"calculate_name":"Phép chia", "calculate_value":2}
]
var comboboxCalculateProps = {
  tableHeaders: [{ fieldName: 'calculate_name', label: 'Phép tính', textAlign: 'text-left', type: "normal", width: '150px'}],
  tableContents: calculationList,
  mode:"manual",
  contentFields:"calculatation"
}

module.exports = {
  apiList: apiList,

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
    { fieldName: 'voucher_code', label: 'SỐ CHỨNG TỪ', textAlign: 'text-left', type: "normal" , hasClick:"VoucherCode"},
    { fieldName: 'description', label: 'DIỄN GIẢI', textAlign: 'text-left', type: "normal" },
    { fieldName: 'total_price', label: 'TỔNG TIỀN', textAlign: 'text-right', type: "normal" , footerValue:'0' , format: "number"},
    { fieldName: 'contact_name', label: 'NGƯỜI GIAO/NGƯỜI NHẬN', textAlign: 'text-left', type: "normal" },
    { fieldName: 'voucher_type_name', label: 'LOẠI CHỨNG TỪ', textAlign: 'text-left', type: "normal" },
    { label: 'CHỨC NĂNG', type:'contextmenu' }
  ],
 
  /**mảng chứa thông tin tiêu đề bảng hàng tiền trong phiếu nhập kho */
  tableInwardDetailHeaders: [
    { label: '#',type: "number" },
    { fieldName: 'commodity_code', dataField:'commodity_id', label: 'MÃ HÀNG', textAlign: 'text-left', type: "comboboxapi", combobox:comboboxCommodityProps},
    { fieldName: 'commodity_name', label: 'TÊN HÀNG', textAlign: 'text-left', type: "input" },
    { fieldName: 'warehouse_code',dataField:'warehouse_id' ,label: 'KHO', textAlign: 'text-left', type: "comboboxapi" , combobox:comboboxWarehouseProps},
    { fieldName: 'debit_account_number',dataField:'debit_account_id', label: 'TK NỢ', textAlign: 'text-left', type: "comboboxapi", combobox:comboboxAccountProps},
    { fieldName: 'credit_account_number', dataField:'credit_account_id',label: 'TK CÓ', textAlign: 'text-left', type: "comboboxapi",combobox:comboboxAccountProps },
    { fieldName: 'unit_name',dataField:"unit_id", label: 'DVT', textAlign: 'text-left', type: "comboboxmanual" , combobox:comboboxUnitProps},
    { fieldName: 'quantity', label: 'SỐ LƯỢNG', textAlign: 'text-right', type: "input",format: "number"},
    { fieldName: 'debit_amount', label: 'ĐƠN GIÁ', textAlign: 'text-right', type: "input" ,format: "number"},
    { fieldName: 'total_price', label: 'THÀNH TIỀN', textAlign: 'text-right', type: "input" ,format: "number"},
    { fieldName: 'lot_number', label: 'SỐ LÔ', textAlign: 'text-left', type: "input"},
    { fieldName: 'expiry', label: 'HẠN SỬ DỤNG', textAlign: 'text-center', type: "date" ,format:"date"},
    { type:'delete' }

  ],
  /**mảng chứa thông tin tiêu đề bảng unit trong form thêm hàng hóa */
  tableUnitHeaders:[
    { fieldName: 'unit_name', dataField:'unit_id', label: 'ĐƠN VỊ CHUYỂN ĐÔI', textAlign: 'text-left', type: "comboboxapi", combobox:CommodityUnitComboboxProps},
    { fieldName: 'rate', label: 'TỶ LỆ CHUYỂN ĐỔI', textAlign: 'text-right', type: "input" },
    { fieldName: 'calculate_name' ,dataField:'calculate_value',label: 'PHÉP TÍNH', textAlign: 'text-left', type: "comboboxmanual" , combobox:comboboxCalculateProps },
    { fieldName: 'description', label: 'MÔ TẢ', textAlign: 'text-left', type:"input"},
    { type:'delete' }
  ],
  /**mảng chứa thông tin tiêu đề của combo dropdown khách hàng */
  tableCustomerHeaders:tableCustomerHeaders,
  /**mảng chứa thông tin tiêu đề của combo dropdown nhân viên */
  tableEmployeeComboboxHeaders:[
    { fieldName: 'employee_code', label: 'Mã nhân viên', textAlign: 'text-left', type: "normal", width: '100px'},
    { fieldName: 'employee_name', label: 'Tên nhân viên', textAlign: 'text-left', type: "normal", width: '200px' },
  ],
  InwardEmployeeComboboxHeaders:[
    { fieldName: 'employee_code', label: 'Mã nhân viên', textAlign: 'text-left', type: "normal", width: '100px'},
    { fieldName: 'employee_name', label: 'Tên nhân viên', textAlign: 'text-left', type: "normal", width: '200px' },
    { fieldName: 'department_name', label: 'Tên đơn vị', textAlign: 'text-left', type: "normal", width: '200px' },
  ],
  customerEmployeeComboboxHeaders:[
    { fieldName: 'employee_code', label: 'Mã nhân viên', textAlign: 'text-left', type: "normal", width: '170px'},
    { fieldName: 'employee_name', label: 'Tên nhân viên', textAlign: 'text-left', type: "normal", width: '200px' },
  ],
  /**combobox hàng hóa*/
  customerComboboxProps: {
    tableHeaders: tableCustomerHeaders,
    api: apiList['accountobjectPagingFilter'],
    tableObject: "AccountObjects",
    valueField: "account_object_name",
  },
  commodityGroupComboboxProps: {
    tableHeaders: commodityGroupComboboxHeaders,
    api: apiList['commodityGroupFilterPaging'],
    tableObject: "CommodityGroups",
    valueField: "commodity_group_name",
    mode:"api"
  },
  comboboxAccountProps:comboboxAccountProps,
  CommodityUnitComboboxProps:CommodityUnitComboboxProps,
  comboboxWarehouseProps:comboboxWarehouseProps,
  tableCommodityComboboxHeaders:[
    { fieldName: 'commodity_code', label: 'Mã hàng', textAlign: 'text-left', type: "normal", width: '100px'},
    { fieldName: 'commodity_name', label: 'Tên hàng', textAlign: 'text-left', type: "normal", width: '300px' },
  ],
  
  /**combobox đơn vị */
  departmentComboboxProps:{
    tableHeaders: departmentComboboxHeaders,
    api: apiList['departmentPagingFilter'],
    tableObject: "Departments",
    valueField: "department_name",
    mode: "api"
  },
  /**table unit content */
  tableUnitContents:[
    {
      'unit_name':'',
      'rate':1,
      'calculate_name':'Phép nhân',
      'calculate_value':1,
      'description':'',
      'calculatation':calculationList,
    }
  ],
  /**table inward detail */
  tableInwardDetailContents:[
    {
      'commodity_code':'',
      'commodity_name':'',
      'warehouse_code':'',
      'debit_account_code':'',
      'credit_account_code':'',
      'units':[],
      'quantity':1,
      'debit_amount':0,
      'total_price':0,
      'lot_number':'',
      'expiry':null,
      'state':0
    }
  ],
  inwardDetailContentsDefault:
    {
      'commodity_code':'',
      'commodity_name':'',
      'warehouse_code':'',
      'debit_account_code':'',
      'credit_account_code':'',
      'units':[],
      'quantity':1,
      'debit_amount':0,
      'total_price':0,
      'lot_number':'',
      'expiry':null,
      'state':0
    }
  ,
  
  message: {
    messageConfirmChange: "Dữ liệu đã bị thay đổi. Bạn có muốn cất không?",
    messageRequired: "<{0}> không được để trống",
    messageDuplication: `Mã nhân viên <{0}> đã tồn tại trong hệ thống, vui lòng kiểm tra lại.`,
    messageEmailSyntax: "Email không đúng định dạng",
    messageDateSyntax: `Ngày chưa đúng định dạng`,
    messageDateFuture: "{0} vượt quá ngày hiện tại",
    messageDeleteWarning: "Bạn có chắc muốn xóa {0} không?",
    messageDeleteSuccess: "xóa {0} thành công",
    messageVoucherDate: "Ngày hạch toán phải lớn hơn hoặc bằng ngày chứng từ. Xin vui lòng kiểm tra lại.",
    messageSaveSuccess: "Cất thành công dữ liệu",
    messgaeDeleteAllMention: "Các chứng từ bạn chọn đều đã ghi sổ"
  },
  mode: {
    ADD: 0,
    EDIT: 1,
    DELETE:2,
    DUPLICATE: 4,
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
    { data: "NK001", value: "1. Thành phẩm sản xuất" },
    { data: "NK002", value: "2. Hàng bán bị trả lại" },
    { data: "NK003", value: "3. Khác (NVL thừa, HH thuê gia công, ...)" },
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

