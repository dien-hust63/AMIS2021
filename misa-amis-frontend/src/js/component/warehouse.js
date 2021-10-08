import BaseAPI from "../base/baseapi";
import axios from "axios";
class WarehouseAPI extends BaseAPI {
    constructor() {
        super();
        this.controller = "Warehouses"
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
export default new WarehouseAPI();