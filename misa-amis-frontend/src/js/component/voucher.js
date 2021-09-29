import BaseAPI from "../base/baseapi";
import axios from "axios";
class VoucherAPI extends BaseAPI {
    constructor() {
        super();
        this.controller = "AccountVouchers"
    }
    /**
    * lọc và phân trang chứng từ
    * url phân trang
    * @returns promise
    * CreatedBy: nvdien(24/9/2021)
    */
    getAccountVoucherPagingFilter(searchData, mentionState, voucherType, startDate, endDate, pageIndex, pageSize) {
        let url = this.baseApiUrl + this.controller + `/filter?searchData=${searchData}&mentionState=${mentionState}`;
        url += `&voucherType=${voucherType}&startDate=${startDate}&endDate=${endDate}&pageIndex=${pageIndex}&pageSize=${pageSize}`;
        return axios.get(url);
    }

    /**
     * Lấy chi tiết chứng từ
     * @param {} voucherId 
     * @returns 
     */
    getVoucherDetail(voucherId){
        let url = this.baseApiUrl + this.controller + `/Detail/${voucherId}`;
        return axios.get(url);
    }
    /**
	 * Ghi sổ nhiều
	 * @param {Array} body 
	 * @returns {Promise}
	 * CreatedBy: nvdien (28/09/2021)
	 */
	mentionMany(body) {
		let url = this.baseApiUrl + this.controller + '/mention';
		return axios.put(url, body);
	}
	/**
	 * Bỏ ghi nhiều
	 * @param {Array} body 
	 * @returns {Promise}
	 * CreatedBy: nvdien (28/09/2021)
	 */
	unMentionMany(body) {	
		let url = this.baseApiUrl + this.controller + '/unmention';
		return axios.put(url, body);
	}
}
export default new VoucherAPI();