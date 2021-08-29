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
        }
    }
}