import BaseAPI from "../base/baseapi";
import axios from "axios";
class CommodityAPI extends BaseAPI {
    constructor() {
        super();
        this.controller = "Commoditys"
    }
   
    /**
     * thêm mới hàng hóa
     * @param {object} body 
     * @returns promise
     * CreatedBY: nvdien(7/10/2021)
     */
    addCommodity(body){
        let api  = this.baseApiUrl + this.controller + "/add";
        return axios.post(api,body);
    }
    /**
     * Tạo mã mới
     * @returns promise
     * CreatedByL nvdien(3/10/2021)
     */
    getNewCode(){
        let url = this.baseApiUrl + this.controller + "/NewCode";
        return axios.get(url);
    }
}
export default new CommodityAPI();