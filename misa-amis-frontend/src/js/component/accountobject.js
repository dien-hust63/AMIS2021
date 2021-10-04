import BaseAPI from "../base/baseapi";
import axios from "axios";
class AccountObjectAPI extends BaseAPI {
    constructor() {
        super();
        this.controller = "AccountObjects"
    }
    /**
    * lọc và phân trang khách hàng
    * url phân trang
    * @returns promise
    * CreatedBy: nvdien(24/9/2021)
    */
    getAccountObjectPagingFilter(searchData,pageIndex, pageSize) {
        let url = this.baseApiUrl + this.controller + `/filter?searchData=${searchData}&pageIndex=${pageIndex}&pageSize=${pageSize}`;
        return axios.get(url);
    }
    /**
     * Tạo mã mới
     * @returns promis
     * CreatedByL nvdien(3/10/2021)
     */
    getNewCode(){
        let url = this.baseApiUrl + this.controller + "/NewCode";
        return axios.get(url);
    }
}
export default new AccountObjectAPI();