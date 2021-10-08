import axios from "axios";
export default class BaseAPI {
    constructor() {
        this.baseApiUrl = "https://localhost:44350/api/v1/"
        this.controller = null
    }

    /**
     * Lấy tất cả thông tin
     * @returns promise
     * author: nvdien(25/8/2021)
     */
    getAll() {
        let api = this.baseApiUrl + this.controller;
        return axios.get(api);
    }

    /**
     * Lấy theo id
     * @param {string} id 
     * @returns promise
     * author: nvdien(25/8/2021)
     */
    getById(id) {
        let api = this.baseApiUrl + this.controller + "/" + id;
        return axios.get(api);
    }

    /**
     * thêm mới
     * @param {object} body thông tin cần thêm
     * @returns promise
     * author: nvdien(25/8/2021)
     */
    post(body) {
        let api = this.baseApiUrl + this.controller;
        return axios.post(api, body);
    }

    /**
     * Chỉnh sửa theo Id
     * @param {string} id 
     * @param {object} body thông tin chỉnh sửa
     * @returns promise
     * author: nvdien(25/8/2021)
     */
    put(id, body) {
        let api = this.baseApiUrl + this.controller + "/" + id;
        return axios.put(api, body);
    }

    /**
     * Xóa nhiều theo Id
     * @param {Array} listData mảng chứa các id
     * @returns promise get từ call axios api
     * CreatedBy: nvdien (01/09/2021)
     */
    deleteMultiple(body) { 
        let api = this.baseApiUrl + this.controller;
        return axios.delete(api, {data: body});
    }
    /**
     * Xóa theo id
     * @param {string} id  id của đối tượng
     * @returns promise
     * CreatedBy: nvdien(30/8/2021)
     */
    delete(id){
        let api = this.baseApiUrl + this.controller + "/" + id;
        return axios.delete(api);
    }

}