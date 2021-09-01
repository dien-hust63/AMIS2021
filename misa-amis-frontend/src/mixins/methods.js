export default {
    methods: {
        /**
         * Format dữ liệu ngày tháng sang định dạng khác mong muốn
         * seperator = "-" : year-month-day
         * seperator = "/" : day/month/year
         * @param {string} dateString xâu dạng date
         * @param {string} seperator dấu phân cách để chia theo định dạng
         * @returns xâu rỗng hoặc xâu dạng date theo định dạng
         * Created by: nvdien (28/8/2021)
         */

        formatDate(dateString, seperator) {
            let dateObj = new Date(dateString);
            if (Number.isNaN(dateObj.getTime())) {
                return "";
            } else {
                let month = dateObj.getUTCMonth() + 1;
                let day = dateObj.getUTCDate();
                let year = dateObj.getUTCFullYear();
                if (month < 10) {
                    month = "0" + month;
                }
                if (day < 10) {
                    day = "0" + day;
                }
                let newdate = "";
                if (seperator == "-") {
                    newdate = year + seperator + month + seperator + day;
                }
                if (seperator == "/") {
                    newdate = day + seperator + month + seperator + year;
                }
                return newdate;
            }
        },

        /**
         * đưa ra giới tính ứng với từng kí hiệu
         * @param {int} type kí hiệu cho giới tính
         * @returns {string} giới tính
         * author: nvdien(28/8/2021)
         */
        showGenderName(type) {
            switch (type) {
                case 0:
                    return "Nữ";
                case 1:
                    return "Nam";
                case 2:
                    return "Khác";
                default:
                    return "";
            }
        },
        /**
         * so sánh 2 object có giống nhau về nội dung không
         * @param object1 object cần so sánh
         * @param object2 object cần so sánh 
         * CreatedBy: nvdien(1/9/2021)
         */
        deepEqual(object1, object2) {
            const keys1 = Object.keys(object1);
            const keys2 = Object.keys(object2);
            if (keys1.length !== keys2.length) {
                return false;
            }
            for (const key of keys1) {
                const val1 = object1[key];
                const val2 = object2[key];
                const areObjects = this.isObject(val1) && this.isObject(val2);
                if (
                    areObjects && !this.deepEqual(val1, val2) ||
                    !areObjects && val1 !== val2
                ) {
                    return false;
                }
            }
            return true;
        },
        /**
         * Kiểm tra phải object không
         * @param object: object cần kiểm tra
         * CreatedBY: nvdien(1/9/2021)
         */
        isObject(object) {
            return object != null && typeof object === 'object';
        },
        /**
        * Kiểm tra xem object có rỗng không
        * @param obj: object cần kiểm tra
        * CreatedBy: nvdien(30/8/2021)
        */
        checkEmptyObject(obj) {
            return obj && Object.keys(obj).length === 0 && obj.constructor === Object;
        },
    }
}