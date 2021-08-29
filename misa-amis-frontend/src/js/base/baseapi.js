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
    async post(body) {
        let api = this.baseApiUrl + this.controller;
        return await axios.post(api, body);
    }

    /**
     * Chỉnh sửa theo Id
     * @param {string} id 
     * @param {object} body thông tin chỉnh sửa
     * @returns promise
     * author: nvdien(25/8/2021)
     */
    async put(id, body) {
        let api = this.baseApiUrl + this.controller + "/" + id;
        return await axios.put(api, body);
    }

    /**
     * Xóa nhiều theo Id
     * @param {Array} listData mảng chứa các id
     * @returns promise get từ call axios api
     * author: nvdien(25/8/2021)
     */
    async deleteMultiple(idList) {
       
        let api = this.baseApiUrl + this.controller + "?entityIds=" + idList;
        return await axios.delete(api);
    }

}