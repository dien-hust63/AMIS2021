export default {
    methods: {
        /**
         * Kiểm tra xem có vượt quá ngày hiện tại
         * @param {string} dateString 
         */
        isFutureDate(dateString) {
            var dateParts = dateString.split("/");
            var givenDate = new Date(+dateParts[2], dateParts[1] - 1, +dateParts[0]);
            let currentDate = new Date();
            if (givenDate > currentDate) {
                return true;
            }
            return false;

        },
        /**
         * Kiểm tra string dạng DD/MM/YYYY đúng định dạng date chưa
         * @param {} dateString string
         * CreatedBy: nvdien(3/9/2021)
         */
        validateDateString(dateString) {
            let pattern = /^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(([1][26]|[2468][048]|[3579][26])00))))$/g;
            return pattern.test(dateString);
        },
        /**
         * Chuyển từ dạng DD/MM/YYYY sang YYYY-MM-DD
         * @param {string} dateString
         * CreatedBy: nvdien(2/9/2021)
         */
        convertDateString(dateString) {
            let dateParts = dateString.split("/");
            return dateString = `${dateParts[2]}-${dateParts[1]}-${dateParts[0]}T00:00:00`;
        },
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
            if (dateString == null) return "";
            if (dateString != null) {
                dateString = dateString.split('T')[0];
            }
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
        /**
         * validate email đúng định dạng
         * @param {String} email xâu email người dùng nhập vào
         * @returns {Boolean} true nếu đúng định dạng
         * CreatedBy: nvdien(3/9/2021)
        */
        validateEmail(email) {
            var re =
                /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            return re.test(email);
        },
        /**
         * format string
         * @returns string
         * CreatedBy: nvdien(3/9/2021)
         */
        formatString(targetString, ...strings) {
            for (let i = 0; i < strings.length; i++) {
                targetString = targetString.replace("{" + i + "}", strings[i]);
            }
            return targetString;
        },
        /**
         * Hàm tính ra element cha chứa lớp được truyền vào
         * @param {Element} childE
         * @param {String} parentClass
         * CreatedBY: nvdien(2/10/2021)
         */
        findParentByClass(childE, parentClass) {
            var parentE = childE;
            if (parentE) {
                // Nếu không chứa class thì tiếp tục vòng lặp
                while (parentE.classList.contains(parentClass) == false) {
                    // Đi ra một element cha
                    parentE = parentE.parentElement;
                    // Khi đã duyệt hết mà không có thì set null và thoát vòng lặp
                    if (parentE.tagName == "BODY") {
                        parentE = null;
                        break;
                    }
                }
            }
            return parentE;
        },
        /**
         * Định dạng tiền
         * @param {string} salary  tiền
         * @returns string tiền được định dạng
         * CreatedBy: nvdien(2/10/2021)
         */
        formatSalary(salary) {
            let salaryString = String(salary).replaceAll(".","");
            var result = '';
            var number 
            if (salaryString != null) {
                for (var i = String(salaryString).length; i > 0; i = i - 3) {
                    if (i > 3) {
                         number = String(salaryString).slice(i - 3, i);
                        result += number.split("").reverse().join("") + ".";
                    } else {
                         number = String(salaryString).slice(0, i);
                        result += number.split("").reverse().join("");
                    }
                }
                return result.split("").reverse().join("");
            } else return '';
        },
        /**
         * convert string định dạng tiền sang số
         * CreatedBy: nvdien(2/10/2021)
         */
        covertStringtoNumber(numberString){
            return parseInt(String(numberString).replaceAll(".",""));
        }
    }
}