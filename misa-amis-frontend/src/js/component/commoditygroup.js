import BaseAPI from "../base/baseapi";
import axios from "axios";
class CommodityGroupAPI extends BaseAPI {
    constructor() {
        super();
        this.controller = "CommodityGroups"
    }

    /**
     * Lấy nhóm hàng hóa chính
     * @returns promise
     * CreatedByL nvdien(3/10/2021)
     */
     getMainGroup(){
        let url = this.baseApiUrl + this.controller + "/Main";
        return axios.get(url);
    }
   
    
}
export default new CommodityGroupAPI();